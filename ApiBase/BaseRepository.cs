using Microsoft.EntityFrameworkCore;
using Zuhid.ApiBase.Models;

namespace Zuhid.ApiBase;

public abstract class BaseRepository<TContext, TModel, TEntity>
  where TContext : IContext
  where TModel : BaseModel
  where TEntity : BaseEntity {
  public abstract IQueryable<TModel> Query { get; }
  protected TContext dbContext;

  public BaseRepository(TContext dbContext) => this.dbContext = dbContext;

  public async Task<List<TModel>> Get(Guid id) => await Query.Where(n => n.Id.Equals(id)).ToListAsync();

  public async Task<bool> Add(TEntity entity) {
    entity.Updated = DateTime.UtcNow;
    dbContext.Set<TEntity>().Add(entity);
    return (await dbContext.SaveChangesAsync()) == 1;
  }

  public async Task<bool> Update(TEntity entity) {
    dbContext.Entry(entity).Property(p => p.Updated).OriginalValue = dbContext.Entry(entity).Property(p => p.Updated).CurrentValue;
    dbContext.Entry(entity).Property(p => p.Updated).CurrentValue = DateTime.UtcNow;
    dbContext.Set<TEntity>().Update(entity);
    return (await dbContext.SaveChangesAsync()) == 1;
  }

  public async Task<bool> Delete(Guid id) {
    var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id.Equals(id));
    if (entity != null) {
      dbContext.Set<TEntity>().Remove(entity);
      return (await dbContext.SaveChangesAsync()) == 1;
    }
    return false;
  }
}
