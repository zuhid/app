using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.EndToEndTests.Page;

namespace Web.EndToEndTests.Tests;

[TestClass]
public class LoginTest {

  [TestMethod]
  public void TestMethod1() {
    using (var webDriver = Settings.WebDriver) {
      webDriver.Navigate().GoToUrl(Settings.BaseUrl);
      var loginPage = new LoginPage(webDriver);
      loginPage.Set("admin@company.com", "P@ssw0rd", true);
      loginPage.Verify("admin@company.com", "P@ssw0rd", true);
      Thread.Sleep(1000); // give the ui some time
      loginPage.Submit();
      Thread.Sleep(1000); // give the ui some time
    }
  }
}
