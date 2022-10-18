namespace Zuhid.BaseApi;

public static class DatabaseLoggerFactory {
  public static ILoggerFactory AddDatabase(this ILoggerFactory factory, string connString) {
    factory.AddProvider(new DatabaseLoggerProvider(connString));
    return factory;
  }
}

