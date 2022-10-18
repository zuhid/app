using Zuhid.ApiBase.Models;

namespace Zuhid.Identity.Api.Models;

public class User : IModel {
  public Guid Id { get; set; }
  public DateTime Updated { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public string PhoneNumber { get; set; }
  public bool TwoFactorEnabled { get; set; }
  public string LandingPage { get; set; }
}
