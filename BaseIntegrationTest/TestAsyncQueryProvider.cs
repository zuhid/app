using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Zuhid.BaseIntegrationTest;

public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider {
  private readonly IQueryProvider inner;

  public TestAsyncQueryProvider(IQueryProvider inner) => this.inner = inner;

  public IQueryable CreateQuery(Expression expression) => new TestAsyncEnumerable<TEntity>(expression);

  public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new TestAsyncEnumerable<TElement>(expression);

  public object Execute(Expression expression) => inner.Execute(expression);

  public TResult Execute<TResult>(Expression expression) => inner.Execute<TResult>(expression);

  public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default) => Execute<TResult>(expression);
}
