using Zuhid.BaseApi.Models;

namespace Zuhid.Identity.Models;

public class User : BaseModel {
  public string Email { get; set; }
  public string PhoneNumber { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public bool TwoFactorEnabled { get; set; }
  public string Role { get; set; }
  public string Clients { get; set; }
  public string Policies { get; set; }
  public string LandingPage { get; set; }
}
