using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zuhid.ApiBase;
using Zuhid.ApiBase.Models;
using Zuhid.Identity.Api.Entities;
using Zuhid.Identity.Api.Mappers;
using Zuhid.Identity.Api.Models;
using Zuhid.Identity.Api.Repositories;

namespace Zuhid.Identity.Api.Controllers;

/// <summary>
/// Rest Api for Users
/// </summary>
public class UserController : BaseController {
  private readonly UserRepository repository;
  protected readonly UserMapper mapper = new UserMapper();
  private readonly ILogger logger;
  private readonly UserManager<UserEntity> userManager;

  public UserController(IdentityDbContext context, ILogger<UserController> logger, UserManager<UserEntity> userManager) {
    this.repository = new UserRepository(context);
    this.logger = logger;
    this.userManager = userManager;
  }

  [HttpGet("")]
  public async Task<List<User>> Get() => await repository.Get();

  [HttpGet("id/{id}")]
  public async Task<List<User>> Get(Guid id) => await repository.Get(id);

  [HttpPost()]
  public async Task<UpdatedResponse> Add([FromBody] User model) {
    var entity = mapper.GetEntity(model);
    var result = await userManager.CreateAsync(entity); //, model.NewPassword);
    result.Errors?.ToList().ForEach(item => ModelState.AddModelError(item.Code, item.Description));
    return new UpdatedResponse { Updated = entity.Updated };
  }

  [HttpPut()]
  public async Task<UpdatedResponse> Update([FromBody] User model) {
    var entity = await userManager.FindByEmailAsync(model.Email);
    entity.PhoneNumber = model.PhoneNumber;
    entity.FirstName = model.FirstName;
    entity.LastName = model.LastName;
    entity.TwoFactorEnabled = model.TwoFactorEnabled;
    entity.LandingPage = model.LandingPage;
    entity.LandingPage = model.LandingPage;
    var result = await userManager.UpdateAsync(entity);
    result.Errors?.ToList().ForEach(item => ModelState.AddModelError(item.Code, item.Description));
    // if (!string.IsNullOrWhiteSpace(model.CurrentPassword) && !string.IsNullOrWhiteSpace(model.NewPassword)) {
    //   result = await userManager.ChangePasswordAsync(entity, model.CurrentPassword, model.NewPassword);
    //   result.Errors?.ToList().ForEach(item => ModelState.AddModelError(item.Code, item.Description));
    // }
    return new UpdatedResponse { Updated = entity.Updated };
  }

  [HttpDelete("id/{id}")]
  public async Task Delete(Guid id) {
    var entity = await userManager.FindByIdAsync(id.ToString());
    var result = await userManager.DeleteAsync(entity);
    result.Errors?.ToList().ForEach(item => ModelState.AddModelError(item.Code, item.Description));
  }
}
