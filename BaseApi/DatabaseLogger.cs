using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;

namespace Zuhid.BaseApi;

public class DatabaseLogger : ILogger {
  private readonly string category;
  private readonly string connString;

  public DatabaseLogger(string category, string connString) {
    this.category = category;
    this.connString = connString;
  }

  public IDisposable BeginScope<TState>(TState state) => null;

  public bool IsEnabled(LogLevel logLevel) => true;

  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
    var queryString = $@"insert into dbo.Log(Updated,LogLevel,Category,EventId,EventName,State,Exception) values(@Updated,@LogLevel,@Category,@EventId,@EventName,@State,@Exception)";
    using var connection = new SqlConnection(connString);
    using var command = new SqlCommand(queryString, connection);
    command.Parameters.AddWithValue("@Updated", DateTime.UtcNow);
    command.Parameters.AddWithValue("@LogLevel", logLevel.ToString());
    command.Parameters.AddWithValue("@Category", category);
    command.Parameters.AddWithValue("@EventId", eventId.Id);
    command.Parameters.AddWithValue("@EventName", (object)eventId.Name ?? DBNull.Value);
    command.Parameters.AddWithValue("@State", state.ToString());
    command.Parameters.AddWithValue("@Exception", exception != null ? Formatter(exception) : DBNull.Value);
    try {
      command.Connection.Open();
      command.ExecuteNonQuery();
    } catch { } // Ignore exception thrown during loggging
    finally {
      command.Connection.Close();
    }
  }

  private string Formatter(Exception exception) {
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
  }
}
