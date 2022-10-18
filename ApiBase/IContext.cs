using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Zuhid.ApiBase;

public interface IContext {
  DatabaseFacade Database { get; }

  DbSet<TEntity> Set<TEntity>() where TEntity : class;

  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

  EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}
