using Microsoft.EntityFrameworkCore;
using Zuhid.ApiBase;
using Zuhid.Identity.Api.Entities;

namespace Zuhid.Identity.Api;

public class IdentityDbContext : DbContext, IDbContext {
  public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);

    // Keys
    builder.Entity<UserToClientEntity>().HasKey(entity => new { entity.UserId, entity.ClientId });
    builder.Entity<UserToRoleEntity>().HasKey(entity => new { entity.UserId, entity.RoleId });

    // // Data Seeding
    builder.LoadData<ClientEntity>();
    builder.LoadData<RoleEntity>();
    builder.LoadData<UserEntity>();
    builder.LoadData<UserToClientEntity>();
    builder.LoadData<UserToRoleEntity>();
  }

  public DbSet<ClientEntity> Client { get; set; }
  public DbSet<RoleEntity> Role { get; set; }
  public DbSet<UserEntity> User { get; set; }
  public DbSet<UserToClientEntity> UserToClient { get; set; }
  public DbSet<UserToRoleEntity> UserToRole { get; set; }
}

