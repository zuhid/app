using Microsoft.EntityFrameworkCore;
using Zuhid.BaseApi;
using Zuhid.Identity.Entities;

namespace Zuhid.Identity;

public interface IIdentityDbContext : IDbContext {
  DbSet<ClientEntity> Client { get; set; }
  DbSet<RoleEntity> Role { get; set; }
  DbSet<PolicyEntity> Policy { get; set; }
  DbSet<UserEntity> User { get; set; }
  DbSet<UserToClientEntity> UserToClient { get; set; }
  DbSet<UserToRoleEntity> UserToRole { get; set; }
  DbSet<LoginEntity> Login { get; set; }
}
