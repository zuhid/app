using Microsoft.AspNetCore.Mvc;
using Zuhid.BaseApi;
using Zuhid.BaseApi.Models;
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

  /// <summary>
  /// Gets all the clients
  /// </summary>
  // [Microsoft.AspNetCore.Authorization.AllowAnonymous]
  [HttpGet("Client")]
  public async Task<List<LookupList>> Client() => await repository.Get<ClientEntity>();
}