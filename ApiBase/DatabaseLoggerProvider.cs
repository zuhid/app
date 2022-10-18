namespace Zuhid.ApiBase;

public class DatabaseLoggerProvider : ILoggerProvider {
  private readonly string connString;

  public DatabaseLoggerProvider(string connString) => this.connString = connString;

  public ILogger CreateLogger(string categoryName) => new DatabaseLogger(categoryName, connString);

  public void Dispose() { }
}
