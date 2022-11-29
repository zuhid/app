using Microsoft.EntityFrameworkCore;
using Zuhid.Identity.Entities;

namespace Zuhid.Identity.Repositories;

public class LoginRepository {
  protected IIdentityDbContext dbContext;

  public LoginRepository(IIdentityDbContext dbContext) => this.dbContext = dbContext;

  public async Task<List<LoginEntity>> Get(string email) => await dbContext.User
    .Where(n => n.Email == email)
    // .Where(n => n.Email.ToLower().Equals((email ?? "").ToLower()))
    .Select(n => new LoginEntity {
      UserId = n.Id,
      PasswordHash = n.PasswordHash,
      FirstName = n.FirstName,
      LastName = n.LastName,
      LandingPage = n.LandingPage,
      Role = n.Role,
      Clients = n.Clients,
      Policies = n.Policies,
    }).ToListAsync();

  //     return await dbContext.Login.FromSqlRaw(@$"
  // SELECT 
  //     Users.Id AS UserId
  //   , Users.PasswordHash
  //   , Users.LandingPage
  //   , Role.Text AS Role
  //   , Client.Text AS Clients
  //   , Policy.Text AS Policies
  // FROM dbo.[User] Users
  //   INNER JOIN dbo.UserToRole UserToRole ON Users.Id = UserToRole.UserId
  //   INNER JOIN dbo.Role Role ON UserToRole.RoleId = Role.Id
  //   INNER JOIN dbo.UserToClient UserToClient ON Users.Id = UserToClient.UserId
  //   INNER JOIN dbo.Client Client ON UserToClient.ClientId = Client.Id
  //   INNER JOIN dbo.UserToPolicy UserToPolicy ON Users.Id = UserToPolicy.UserId
  //   INNER JOIN dbo.Policy Policy ON UserToPolicy.PolicyId = Policy.Id
  // WHERE Users.Email = @email
  //     ", new SqlParameter("@email", email)
  //     ).ToListAsync();

}
