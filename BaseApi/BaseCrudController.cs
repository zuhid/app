using Microsoft.AspNetCore.Mvc;
using Zuhid.BaseApi.Models;

namespace Zuhid.BaseApi;

public abstract class BaseCrudController<TContext, TRepository, TModel, TEntity> : ControllerBase
  where TContext : IDbContext
  where TRepository : BaseRepository<TContext, TModel, TEntity>
  where TModel : BaseModel
  where TEntity : BaseEntity {
  protected TRepository repository;

  [HttpGet("id/{id}")]
  public async Task<IActionResult> Get(Guid id) => Ok(await repository.Get(id));

  [HttpPost]
  public async Task<IActionResult> Add([FromBody] TEntity entity) => await AddUpdate(true, entity);

  [HttpPut]
  public async Task<IActionResult> Update([FromBody] TEntity entity) => await AddUpdate(false, entity);

  [HttpDelete("id/{id}")]
  public async Task<IActionResult> Delete(Guid id) => (await repository.Delete(id)) ? Ok() : NotFound();

  private async Task<IActionResult> AddUpdate(bool isAdd, TEntity entity) {
    if (entity == null)
      return BadRequest("model cannot be null");
    if (!ModelState.IsValid)
      return BadRequest(ModelState);
    // call add or update based on the param passed in
    var result = isAdd ? (await repository.Add(entity)) : (await repository.Update(entity));
    // if succesfull, then return the Updated date tiem value 
    return result ? Ok(new { entity.Updated }) : NotFound();
  }
}
