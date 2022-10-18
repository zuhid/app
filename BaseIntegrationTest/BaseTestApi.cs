using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zuhid.BaseIntegrationTest;

public class BaseTestApi {
  protected readonly BaseSetting setting;
  protected readonly TestApiService testApiService;
  protected readonly string roles;
  protected readonly string url;
  protected readonly Random random = new Random();

  public BaseTestApi(string url, BaseSetting setting) {
    this.setting = setting;
    this.url = url;
    testApiService = new TestApiService(setting.HttpClient, new TestSuite(url, setting.TestResultFolder, setting.TfsBasePath));
  }

  [TestCleanup()]
  public void GenerateReport() {
    testApiService.TestSuite.GenerateReport();
  }
}
