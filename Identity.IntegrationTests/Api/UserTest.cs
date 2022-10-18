using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zuhid.BaseApi.Models;
using Zuhid.BaseIntegrationTest;
using Zuhid.Identity.Models;
using Zuhid.Tools;

namespace Zuhid.Identity.IntegrationTests.Api;

[TestClass]
public class UserTest : BaseTestApi {
  public UserTest() : base("user", new Setting()) { }

  [TestMethod]
  public async Task RunTestSuite() {
    await testApiService.TestSuite.RunTestCase("101", "Verify get", Get);
  }

  private async Task Get() {
    // arrange
    var addModel = new User {
      Id = Guid.NewGuid(),
      FirstName = random.NextWord(),
      LastName = random.NextWord(),
      Email = random.NextWord() + random.NextInt64(99999) + "@test.com",
      PhoneNumber = $"{random.Next(100, 999)}-{random.NextInt64(100, 999)}-{random.NextInt64(1000, 9999)}",
      TwoFactorEnabled = random.Next(2) == 1,
      LandingPage = "auth/search"
    };

    var updateModel = new User {
      Id = addModel.Id,
      FirstName = random.NextWord(),
      LastName = random.NextWord(),
      Email = addModel.Email,
      PhoneNumber = $"{random.Next(100, 999)}-{random.NextInt64(100, 999)}-{random.NextInt64(1000, 9999)}",
      TwoFactorEnabled = random.Next(2) == 1,
      LandingPage = "dashboard"
    };

    // act
    await testApiService.Get<LookupList>("Anonymous user cannot access the url", $"{url}/id/{addModel.Id}", HttpStatusCode.Unauthorized, null);
    await testApiService.Login(setting.Email, setting.Password);
    await testApiService.Get("Verify user does not exists", $"{url}/id/{addModel.Id}", HttpStatusCode.OK, new List<User>());

    // Add user
    await testApiService.Post("Add user", $"{url}", addModel, HttpStatusCode.OK);
    var actualUpdateModel = (await testApiService.Get("Verify user is added", $"{url}/id/{addModel.Id}", HttpStatusCode.OK, new List<User> { addModel })).FirstOrDefault();

    // update user
    updateModel.Updated = actualUpdateModel.Updated;
    await testApiService.Put("Update User", $"{url}", updateModel, HttpStatusCode.OK);
    await testApiService.Get("Verify user is updated", $"{url}/id/{updateModel.Id}", HttpStatusCode.OK, new List<User> { updateModel });

    // delete user
    await testApiService.Delete("Delete User", $"{url}/id/{addModel.Id}", HttpStatusCode.OK);
    await testApiService.Get("Verify user is deleted", $"{url}/id/{addModel.Id}", HttpStatusCode.OK, new List<User>());
  }
}
