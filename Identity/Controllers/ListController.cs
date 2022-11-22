using Microsoft.AspNetCore.Mvc;
using Zuhid.BaseApi;
using Zuhid.Identity.Entities;

namespace Zuhid.Identity.Controllers;

/// <summary>
/// Returns lists
/// </summary>
public class ListController : BaseController {
  private readonly ListRepository repository;

  /// <summary>
  /// Constructor takes AuthContext
  /// </summary>
  public ListController(IdentityDbContext dbContext) => this.repository = new ListRepository(dbContext);

  [HttpGet("Exception")]
  public Task<List<string>> Exception() => throw new Exception("ddddddddddddddd");

  /// <summary>
  /// Gets all the clients
  /// </summary>
  // [Microsoft.AspNetCore.Authorization.AllowAnonymous]
  [HttpGet("Client")]
  public async Task<List<string>> Client() => await repository.GetUniqueList<ClientEntity>();

  [HttpGet("Role")]
  public async Task<List<string>> Role() => await repository.GetUniqueList<RoleEntity>();

}
