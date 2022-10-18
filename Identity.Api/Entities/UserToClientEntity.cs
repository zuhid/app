using Zuhid.ApiBase.Models;

namespace Zuhid.Identity.Api.Entities;

public class UserToClientEntity : BaseEntity {
  public Guid UserId { get; set; }
  public Guid ClientId { get; set; }
  public UserEntity User { get; set; }
  public ClientEntity Client { get; set; }
}
