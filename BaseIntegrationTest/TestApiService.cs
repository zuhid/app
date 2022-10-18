using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using Zuhid.BaseApi.Models;
using Zuhid.Tools;

namespace Zuhid.BaseIntegrationTest;

public class TestApiService {
  public HttpClient HttpClient { get; set; }
  public TestSuite TestSuite { get; set; }

  public TestApiService(HttpClient httpClient, TestSuite testSuite) {
    this.HttpClient = httpClient;
    this.TestSuite = testSuite;
  }

  public async Task Login(string email, string password) {
    var loginModel = new Login { Email = email, Password = password };
    // var response = await HttpClient.PostAsJsonAsync("login/generateTfa", loginModel);
    // var result = (HttpStatusCode.OK == response.StatusCode);
    // var responseModel = await response.Content.ReadAsAsync<LoginResponseModel>();

    // TestSuite.AddTestStep($"Login with the given username and password, then use the new authentication token in the header going forward",
    //    HttpMethod.Post, "login", loginModel,
    //    HttpStatusCode.OK, "Login Token",
    //    response.StatusCode, responseModel,
    //    result);

    // // Login with token
    // loginModel.Token = GetToken(loginModel);
    var response = await HttpClient.PostAsJsonAsync("login", loginModel);
    var result = (HttpStatusCode.OK == response.StatusCode);

    JwtPayload jwtToken = null;
    if (result) {
      var responseModel = await response.Content.ReadAsAsync<LoginResponse>();
      jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(responseModel.Token).Payload;
      HttpClient.DefaultRequestHeaders.Clear();
      HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {responseModel.Token}");
    }

    TestSuite.AddTestStep($"Login with the given username and password, then use the new authentication token in the header going forward",
       HttpMethod.Post, "login", loginModel,
       HttpStatusCode.OK, "Login Token",
       response.StatusCode, jwtToken,
       result);
  }

  private string GetToken(Login login) {
    var assembly = Assembly.GetExecutingAssembly();
    // first line is the subject and second line is the body which is the token
    return File.ReadAllLines(
      Path.Join(assembly.Location.Substring(0, assembly.Location.IndexOf("Api.Test")), "Api.Test", "TestEmail", $"{login.Email}.txt")
    )[1];
  }

  public async Task<List<TModel>> Get<TModel>(string message, string inputUrl, HttpStatusCode expectedStatus, List<TModel> expectedModelList) {
    var response = await HttpClient.GetAsync(inputUrl);
    List<TModel> actualModel = null;
    bool result;
    if (response.IsSuccessStatusCode) {
      actualModel = await response.Content.ReadAsAsync<List<TModel>>();
      result = (expectedStatus == response.StatusCode) && new ObjectHelper().ListEquals(expectedModelList, actualModel);
    } else {
      result = (expectedStatus == response.StatusCode);
    }
    TestSuite.AddTestStep(message, HttpMethod.Get, inputUrl, null, expectedStatus, expectedModelList, response.StatusCode, actualModel, result);
    return actualModel;
  }

  public async Task<DateTime> Post<TModel>(string message, string inputUrl, TModel inputModel, HttpStatusCode expectedStatus) {
    var response = await HttpClient.PostAsJsonAsync(inputUrl, inputModel);
    UpdateResponse updated = null;
    string expectdBody = string.Empty;
    if (response.IsSuccessStatusCode) {
      updated = await response.Content.ReadAsAsync<UpdateResponse>();
      expectdBody = (response.StatusCode == HttpStatusCode.OK) ? "system generated updated field for the record" : null;
    }
    TestSuite.AddTestStep(message, HttpMethod.Post, inputUrl, inputModel, expectedStatus, expectdBody, response.StatusCode, updated, expectedStatus == response.StatusCode);
    return updated?.Updated ?? new DateTime();
  }

  public async Task<DateTime> Put<TModel>(string message, string inputUrl, TModel inputModel, HttpStatusCode expectedStatus, TModel expectedModel = null)
     where TModel : class {
    var response = await HttpClient.PutAsJsonAsync(inputUrl, inputModel);
    bool result;
    object expectedBody = expectedModel;
    object actualBody;
    UpdateResponse updated = null;
    switch (response.StatusCode) {
      case HttpStatusCode.OK:
        updated = await response.Content.ReadAsAsync<UpdateResponse>();
        actualBody = updated;
        expectedBody = "system generated updated field for the record";
        result = expectedStatus == response.StatusCode;
        break;

      case HttpStatusCode.Conflict:
        actualBody = await response.Content.ReadAsAsync<List<TModel>>();
        result = (expectedStatus == response.StatusCode) && (new ObjectHelper().Equals(expectedModel, ((List<TModel>)actualBody).FirstOrDefault()));
        break;

      default:
        actualBody = await response.Content.ReadAsStringAsync();
        result = expectedStatus == response.StatusCode;
        break;
    }
    TestSuite.AddTestStep(message, HttpMethod.Put, inputUrl, inputModel, expectedStatus, expectedBody, response.StatusCode, actualBody, result);
    return updated?.Updated ?? DateTime.MinValue;
  }

  public async Task Delete(string message, string inputUrl, HttpStatusCode expectedStatus) {
    var response = await HttpClient.DeleteAsync(inputUrl);
    var expectedBody = await response.Content.ReadAsStringAsync();
    TestSuite.AddTestStep(message, HttpMethod.Delete, inputUrl, null, expectedStatus, null, response.StatusCode, expectedBody, expectedStatus == response.StatusCode);
  }
}
