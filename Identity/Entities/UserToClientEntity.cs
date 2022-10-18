using Zuhid.BaseApi.Models;

namespace Zuhid.Identity.Entities;

public class UserToClientEntity : BaseEntity {
  public Guid UserId { get; set; }
  public Guid ClientId { get; set; }
  public UserEntity User { get; set; }
  public ClientEntity Client { get; set; }
}
