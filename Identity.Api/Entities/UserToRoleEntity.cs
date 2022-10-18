using Zuhid.ApiBase.Models;

namespace Zuhid.Identity.Api.Entities;

public class UserToRoleEntity : BaseEntity {
  public Guid UserId { get; set; }
  public Guid RoleId { get; set; }
  public UserEntity User { get; set; }
  public RoleEntity Role { get; set; }
}
