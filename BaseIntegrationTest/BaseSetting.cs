namespace Zuhid.BaseIntegrationTest;

public abstract class BaseSetting {
  public string TestResultFolder { get; set; }
  public string TfsBasePath => "https://github.com/zuhid/";
  public string Email => "admin@company.com";
  public string Password => "P@ssw0rd";
  public abstract HttpClient HttpClient { get; }
}
