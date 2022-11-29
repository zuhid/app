using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;
using Zuhid.BaseApi.Models;
// using Zuhid.BaseIntegrationTest;
using Zuhid.BaseUnitTest;

namespace Zuhid.Identity.UnitTests.Repositories;

public class TestRepository {
  public void MockContext<TEntity>(Mock<IIdentityDbContext> mockContext, Expression<Func<IIdentityDbContext, DbSet<TEntity>>> expression, IQueryable<TEntity> repoData) // , Exception ex)
    where TEntity : BaseEntity, new() {
    var mockSet = new Mock<DbSet<TEntity>>();
    // mockSet.As<IAsyncEnumerable<TEntity>>().Setup(m => m.GetAsyncEnumerator(CancellationToken.None)).Returns(new TestAsyncEnumerator<TEntity>(repoData.GetEnumerator()));
    // if (ex == null) {
    mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<TEntity>(repoData.Provider));
    // } else { mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Throws(ex); }
    mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(repoData.Expression);
    // mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(repoData.ElementType);
    mockContext.Setup(expression).Returns(mockSet.Object);
  }
}
