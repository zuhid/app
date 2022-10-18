using Microsoft.EntityFrameworkCore;
using Zuhid.ApiBase.Models;

namespace Zuhid.ApiBase;

public class ListRepository {
  protected IDbContext dbContext;

  public ListRepository(IDbContext dbContext) => this.dbContext = dbContext;

  public async Task<List<LookupList>> Get<TEntity>() where TEntity : LookupListEntity =>
    await dbContext.Set<TEntity>().Select(entity => new LookupList {
      Id = entity.Id,
      Text = entity.Text
    })
    .OrderBy(n => n.Text)
    .ToListAsync();
}
