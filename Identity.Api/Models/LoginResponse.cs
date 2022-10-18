using Zuhid.ApiBase.Models;

namespace Zuhid.Identity.Api.Models;

public class LoginResponse : IModel {
  public string Token { get; set; }

  public string LandingPage { get; set; }
}
