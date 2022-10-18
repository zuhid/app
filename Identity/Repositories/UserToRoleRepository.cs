using Microsoft.EntityFrameworkCore;

namespace Zuhid.Identity.Repositories;

public class UserToRoleRepository {
  protected IdentityDbContext dbContext;

  public UserToRoleRepository(IdentityDbContext dbContext) => this.dbContext = dbContext;

  public async Task<List<string>> Get(Guid userId) => await dbContext.UserToRole
    .Include(n => n.Role)
    .Where(n => n.UserId.Equals(userId))
    .Select(n => n.Role.Text)
    .ToListAsync();
}
