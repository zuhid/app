using Microsoft.EntityFrameworkCore;

namespace Zuhid.Identity.Entities;

[Keyless]
public class LoginEntity {
  public Guid UserId { get; set; }
  public string PasswordHash { get; set; }
  public string LandingPage { get; set; }
  public string Client { get; set; }
  public string Role { get; set; }
}
