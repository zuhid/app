using Microsoft.EntityFrameworkCore;

namespace Zuhid.Identity.Entities;

[Keyless]
public class LoginEntity {
  public Guid UserId { get; set; }
  public string PasswordHash { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string LandingPage { get; set; }
  public string Role { get; set; }
  public string Clients { get; set; }
  public string Policies { get; set; }
}
