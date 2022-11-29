namespace Zuhid.BaseApi;

public class DatabaseLoggerProvider : ILoggerProvider {
  private readonly string connString;

  public DatabaseLoggerProvider(string connString) => this.connString = connString;

  public ILogger CreateLogger(string category) => new DatabaseLogger(category, connString);

  public void Dispose() { }
}
