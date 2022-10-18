using Microsoft.EntityFrameworkCore;
using Zuhid.BaseApi;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Models;

namespace Zuhid.Identity.Repositories;

public class UserRepository : BaseRepository<IdentityDbContext, User, UserEntity> {

  public override IQueryable<User> Query => dbContext.User.Select(entity => new User {
    Id = entity.Id,
    Updated = entity.Updated,
    Email = entity.Email,
    PhoneNumber = entity.PhoneNumber,
    FirstName = entity.FirstName,
    LastName = entity.LastName,
    TwoFactorEnabled = entity.TwoFactorEnabled,
    LandingPage = entity.LandingPage,
  });

  public UserRepository(IdentityDbContext dbContext) : base(dbContext) { }

  public async Task<List<User>> Get() => await Query.ToListAsync();
}
