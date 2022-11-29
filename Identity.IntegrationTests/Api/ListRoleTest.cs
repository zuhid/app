using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zuhid.BaseIntegrationTest;

namespace Zuhid.Identity.IntegrationTests.Api;

[TestClass]
public class ListRoleTest : BaseTestApi {
  public ListRoleTest() : base("list/role", new Setting()) { }

  [TestMethod]
  public async Task RunTestSuite() {
    await testApiService.TestSuite.RunTestCase("101", "Verify get", Get);
  }

  private async Task Get() {
    // arrange
    var expected = new List<string> { "Administrator", "AlphaUser", "Contact" };

    // act
    await testApiService.Get<string>("Anonymous user cannot access the url", url, HttpStatusCode.Unauthorized, null);
    await testApiService.Login(setting.Email, setting.Password);
    await testApiService.Get("Calling 'Get' with authenticated user returns data", url, HttpStatusCode.OK, expected);
  }
}
