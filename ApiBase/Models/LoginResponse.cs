namespace Zuhid.ApiBase.Models;

public class LoginResponse : IModel {
  /// <summary>
  /// Is Tfa required
  /// </summary>
  public bool RequireTfa { get; set; }

  /// <summary>
  /// Authentication token
  /// </summary>
  public string Token { get; set; }

  /// <summary>
  /// LandingPage
  /// </summary>
  public string LandingPage { get; set; }
}
