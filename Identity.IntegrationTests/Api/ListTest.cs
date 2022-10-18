using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zuhid.BaseApi.Models;
using Zuhid.BaseIntegrationTest;

namespace Zuhid.Identity.IntegrationTests.Api;

[TestClass]
public class ListTest : BaseTestApi {
  public ListTest() : base("list/client", new Setting()) {
  }

  [TestMethod]
  public async Task RunTestSuite() {
    await testApiService.TestSuite.RunTestCase("101", "Verify get", Get);
  }

  private async Task Get() {
    // arrange
    var expected = new List<LookupList> {
      new LookupList { Id = new Guid("2a2886c4-b36c-476a-ba14-44f6a4ac8b55"), Text = "Amazon" },
      new LookupList { Id = new Guid("56dba2a8-5e2d-401b-b810-e5e01abcb3c9"), Text = "Google" },
      new LookupList { Id = new Guid("f6f48f79-a8d0-4f8a-8260-106b78c0ed35"), Text = "Microsoft" }
    };

    // act
    await testApiService.Get<LookupList>("Anonymous user cannot access the url", url, HttpStatusCode.Unauthorized, null);
    await testApiService.Login(setting.Email, setting.Password);
    await testApiService.Get("Calling 'Get' with authenticated user returns data", url, HttpStatusCode.OK, expected);
  }
}
