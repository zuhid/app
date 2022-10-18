using Zuhid.BaseApi.Models;

namespace Zuhid.Identity.Entities;

public class UserEntity : BaseEntity {
  public string Email { get; set; }
  public string PasswordHash { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string PhoneNumber { get; set; }
  public bool TwoFactorEnabled { get; set; }
  public string LandingPage { get; set; }
}
