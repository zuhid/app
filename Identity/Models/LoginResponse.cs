using Zuhid.BaseApi.Models;

namespace Zuhid.Identity.Models;

public class LoginResponse : IModel {
  public string Token { get; set; }

  public string LandingPage { get; set; }
}
