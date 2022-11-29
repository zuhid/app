using Microsoft.AspNetCore.Mvc;
using Zuhid.BaseApi;
using Zuhid.BaseApi.Models;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Models;
using Zuhid.Identity.Repositories;

namespace Zuhid.Identity.Controllers;

public class UserController : BaseController {

  private readonly UserRepository repository;
  public UserController(IdentityDbContext context) {
    this.repository = new UserRepository(context);
  }

  [HttpGet()]
  public async Task<List<User>> Get() => await repository.Get();

  [HttpGet("id/{id}")]
  public async Task<List<User>> Get(Guid id) => await repository.Get(id);

  [HttpPost]
  public async Task<UpdateResponse> Add([FromBody] User model) => await AddUpdate(true, model);

  [HttpPut]
  public async Task<UpdateResponse> Update([FromBody] User model) => await AddUpdate(false, model);

  [HttpDelete("id/{id}")]
  public async Task<Boolean> Delete(Guid id) => await repository.Delete(id);

  private async Task<UpdateResponse> AddUpdate(bool isAdd, User model) {
    var entity = new UserEntity {
      Id = model.Id,
      Updated = model.Updated,
      Email = model.Email,
      FirstName = model.FirstName,
      LastName = model.LastName,
      PhoneNumber = model.PhoneNumber,
      TwoFactorEnabled = model.TwoFactorEnabled,
      Role = model.Role,
      Clients = model.Clients,
      Policies = model.Policies,
      LandingPage = model.LandingPage
    };
    var result = isAdd ? (await repository.Add(entity)) : (await repository.Update(entity));
    return new UpdateResponse { Updated = entity.Updated };
  }
}
