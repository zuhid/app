using System.Reflection;
using Microsoft.AspNetCore.Mvc.Testing;
using Zuhid.BaseIntegrationTest;
using Zuhid.Identity;

namespace Zuhid.Identity.IntegrationTests;

public class Setting : BaseSetting {
  public Setting() {
    var location = Assembly.GetExecutingAssembly().Location;
    TestResultFolder = Path.Combine(location.Substring(0, location.IndexOf("bin")), "TestCases");
  }

  public bool UseTestServer => false;

  public override HttpClient HttpClient => UseTestServer
    ? new WebApplicationFactory<Program>().CreateClient()
    : new HttpClient() { BaseAddress = new Uri("http://localhost:18001/") };
}
