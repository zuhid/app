using Zuhid.ApiBase.Models;

namespace Zuhid.Identity.Api.Entities;

public class UserEntity : BaseEntity {
  public string Email { get; set; }
  public string PasswordHash { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string PhoneNumber { get; internal set; }
  public bool TwoFactorEnabled { get; internal set; }
  public string LandingPage { get; set; }
}
