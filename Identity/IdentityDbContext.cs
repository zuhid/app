using Microsoft.EntityFrameworkCore;
using Zuhid.BaseApi;
using Zuhid.Identity.Entities;

namespace Zuhid.Identity;

public class IdentityDbContext : DbContext, IIdentityDbContext {
  public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);

    // Keys
    builder.Entity<UserToClientEntity>().HasKey(entity => new { entity.UserId, entity.ClientId });
    builder.Entity<UserToRoleEntity>().HasKey(entity => new { entity.UserId, entity.RoleId });

    // Used for query only
    builder.Entity<LoginEntity>().ToTable("null", t => t.ExcludeFromMigrations());

    // Data Seeding
    builder.LoadData<ClientEntity>();
    builder.LoadData<RoleEntity>();
    builder.LoadData<PolicyEntity>();
    builder.LoadData<UserEntity>();
    builder.LoadData<UserToClientEntity>();
    builder.LoadData<UserToRoleEntity>();
  }

  public DbSet<ClientEntity> Client { get; set; }
  public DbSet<RoleEntity> Role { get; set; }
  public DbSet<PolicyEntity> Policy { get; set; }
  public DbSet<UserEntity> User { get; set; }
  public DbSet<UserToClientEntity> UserToClient { get; set; }
  public DbSet<UserToRoleEntity> UserToRole { get; set; }
  public DbSet<LoginEntity> Login { get; set; }
}

