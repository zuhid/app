using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Zuhid.Identity.Entities;

namespace Zuhid.Identity.Repositories;

public class LoginRepository {
  protected IdentityDbContext dbContext;

  public LoginRepository(IdentityDbContext dbContext) => this.dbContext = dbContext;

  public async Task<List<LoginEntity>> Get(string email) {
    return await dbContext.Login.FromSqlRaw(@$"
SELECT 
    Users.Id AS UserId
  , Users.PasswordHash
  , Users.LandingPage
  , Client.Text AS Client
  , Role.Text AS Role
FROM dbo.[User] Users
  INNER JOIN dbo.UserToClient UserToClient ON Users.Id = UserToClient.UserId
  INNER JOIN dbo.Client Client ON UserToClient.ClientId = Client.Id
  INNER JOIN dbo.UserToRole UserToRole ON Users.Id = UserToRole.UserId
  INNER JOIN dbo.Role Role ON UserToRole.RoleId = Role.Id
WHERE Users.Email = @email
    ", new SqlParameter("@email", email)
    ).ToListAsync();
  }
}
