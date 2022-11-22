using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;

namespace Zuhid.BaseApi;

public class DatabaseLogger : ILogger {
  private readonly string categoryName;
  private readonly string connString;

  public DatabaseLogger(string categoryName, string connString) {
    this.categoryName = categoryName;
    this.connString = connString;
  }

  public IDisposable BeginScope<TState>(TState state) => null;

  public bool IsEnabled(LogLevel logLevel) => true; //logLevel >= LogLevel.Information; // categoryName.StartsWith("Api") 

  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
    var queryString = $@"insert into dbo.Log(Updated,LogLevel,EventId,Category,Message) values(@Updated,@LogLevel,@EventId,@Category,@Message)";
    using var connection = new SqlConnection(connString);
    using var command = new SqlCommand(queryString, connection);
    command.Parameters.AddWithValue("@Updated", DateTime.UtcNow);
    command.Parameters.AddWithValue("@LogLevel", logLevel.ToString());
    command.Parameters.AddWithValue("@EventId", eventId.Id);
    command.Parameters.AddWithValue("@Category", eventId.Name ?? categoryName);
    command.Parameters.AddWithValue("@Message", Formatter(state, exception));
    try {
      command.Connection.Open();
      command.ExecuteNonQuery();
    } catch { } // Ignore exception thrown during loggging
    finally {
      command.Connection.Close();
    }
  }

  private string Formatter<TState>(TState state, Exception exception) {
    if (exception != null) {
      var stacktrace = new List<object>();
      var stepList = exception.StackTrace.Split(" at ");
      for (int i = 0; i < stepList.Length; i++) {
        string item = stepList[i].Trim();
        if (!string.IsNullOrWhiteSpace(item)) {
          var index = item.IndexOf(" in ");
          if (index > 0) {
            stacktrace.Add(new { At = item.Substring(0, index).Trim(), In = item.Substring(index + 3).Trim() });
          } else {
            stacktrace.Add(new { At = item });
          }
        }
      }
      return JsonSerializer.Serialize(
        new { exception.Message, stacktrace, exception.Data },
        new JsonSerializerOptions {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
          Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        }
      );
    } else {
      return state.ToString();
    }
  }
}
