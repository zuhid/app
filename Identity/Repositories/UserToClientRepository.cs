using Microsoft.EntityFrameworkCore;

namespace Zuhid.Identity.Repositories;

public class UserToClientRepository {
  protected IdentityDbContext dbContext;

  public UserToClientRepository(IdentityDbContext dbContext) => this.dbContext = dbContext;

  public async Task<List<string>> Get(Guid userId) => await dbContext.UserToClient
    .Include(n => n.Client)
    .Where(n => n.UserId.Equals(userId))
    .Select(n => n.Client.Text)
    .ToListAsync();
}
