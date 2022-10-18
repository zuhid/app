using Microsoft.EntityFrameworkCore;
using Zuhid.Identity.Api.Entities;
using Zuhid.Identity.Api.Models;

namespace Zuhid.Identity.Api.Repositories;

public class UserRepository {
  protected IdentityDbContext dbContext;

  private IQueryable<User> query => dbContext.User.Select(entity => new User {
    Id = entity.Id,
    Updated = entity.Updated,
    Email = entity.Email,
    PhoneNumber = entity.PhoneNumber,
    FirstName = entity.FirstName,
    LastName = entity.LastName,
    TwoFactorEnabled = entity.TwoFactorEnabled,
    LandingPage = entity.LandingPage,
  });

  public UserRepository(IdentityDbContext dbContext) => this.dbContext = dbContext;

  public async Task<List<User>> Get() => await query.ToListAsync();
  public async Task<List<User>> Get(Guid id) => await query.Where(n => n.Id.Equals(id)).ToListAsync();
  public async Task<UserEntity> Get(string email) => await dbContext.User
    .Where(n => n.Email.Equals(email))
    .FirstOrDefaultAsync();
}
