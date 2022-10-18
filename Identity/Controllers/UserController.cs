using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zuhid.BaseApi;
using Zuhid.BaseApi.Models;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Mappers;
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
      LandingPage = model.LandingPage
    };
    var result = isAdd ? (await repository.Add(entity)) : (await repository.Update(entity));
    return new UpdateResponse { Updated = entity.Updated };
  }
}

// /// <summary>
// /// Rest Api for Users
// /// </summary>
// public class UserController : BaseController {
//   private readonly UserRepository repository;
//   protected readonly UserMapper mapper = new UserMapper();
//   private readonly ILogger logger;
//   private readonly UserManager<UserEntity> userManager;

//   public UserController(IdentityDbContext context, ILogger<UserController> logger, UserManager<UserEntity> userManager) {
//     this.repository = new UserRepository(context);
//     this.logger = logger;
//     this.userManager = userManager;
//   }

//   [HttpGet("")]
//   public async Task<List<User>> Get() => await repository.Get();

//   [HttpGet("id/{id}")]
//   public async Task<List<User>> Get(Guid id) => await repository.Get(id);

//   [HttpPost()]
//   public async Task<UpdateResponse> Add([FromBody] User model) {
//     var entity = mapper.GetEntity(model);
//     var result = await userManager.CreateAsync(entity); //, model.NewPassword);
//     result.Errors?.ToList().ForEach(item => ModelState.AddModelError(item.Code, item.Description));
//     return new UpdateResponse { Updated = entity.Updated };
//   }

//   [HttpPut()]
//   public async Task<UpdateResponse> Update([FromBody] User model) {
//     var entity = await userManager.FindByEmailAsync(model.Email);
//     entity.PhoneNumber = model.PhoneNumber;
//     entity.FirstName = model.FirstName;
//     entity.LastName = model.LastName;
//     entity.TwoFactorEnabled = model.TwoFactorEnabled;
//     entity.LandingPage = model.LandingPage;
//     entity.LandingPage = model.LandingPage;
//     var result = await userManager.UpdateAsync(entity);
//     result.Errors?.ToList().ForEach(item => ModelState.AddModelError(item.Code, item.Description));
//     return new UpdateResponse { Updated = entity.Updated };
//   }

//   [HttpDelete("id/{id}")]
//   public async Task Delete(Guid id) {
//     var entity = await userManager.FindByIdAsync(id.ToString());
//     var result = await userManager.DeleteAsync(entity);
//     result.Errors?.ToList().ForEach(item => ModelState.AddModelError(item.Code, item.Description));
//   }
// }
