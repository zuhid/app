namespace Zuhid.BaseApi.Models;

public class LoginResponse : IModel {
  /// <summary>
  /// Authentication token
  /// </summary>
  public string Token { get; set; }
}
