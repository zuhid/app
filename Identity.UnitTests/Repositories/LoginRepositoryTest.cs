using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Repositories;

namespace Zuhid.Identity.UnitTests.Repositories;

[TestClass]
public class LoginRepositoryTest {
  [TestMethod]
  public async Task Get() {
    // Arrange
    var mockContext = new Mock<IIdentityDbContext>();
    new TestRepository().MockContext<UserEntity>(mockContext, context => context.User, new List<UserEntity>{
      new UserEntity{ Id = new Guid(), Email = "email_1@company.com" },
      new UserEntity{ Id = new Guid(), Email = "email_2@company.com" },
      new UserEntity{ Id = new Guid(), Email = "email_3@company.com" },
      new UserEntity{ Id = new Guid() }
    }.AsQueryable());
    var repository = new LoginRepository(mockContext.Object);

    // Assert
    Assert.AreEqual(1, (await repository.Get("email_2@company.com")).Count, "Found a valid email");
    Assert.AreEqual(0, (await repository.Get("email_5@company.com")).Count, "No email found");
    // Assert.AreEqual(1, (await repository.Get("EMAIL_2@COMPANY.COM")).Count, "Found a valid email even wehn case does not match");
    // Assert.AreEqual(0, (await repository.Get(null)).Count, "Null returns no email");
    // Assert.AreEqual(0, (await repository.Get(string.Empty)).Count, "Empty strin returns no email");
  }
}
