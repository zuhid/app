using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zuhid.BaseApi;
using Zuhid.BaseApi.Models;
using Zuhid.Identity.Repositories;
using Zuhid.Tools;

namespace Zuhid.Identity.Controllers;

/// <summary>
/// Login user
/// </summary>
[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase {
  public readonly ISecurityService securityService;
  private readonly ITokenService tokenService;
  private readonly IEmailService emailService;
  private readonly ISmsService smsService;
  private readonly LoginRepository loginRepository;

  public LoginController(
    IdentityDbContext dbContext,
    ISecurityService securityService,
    ITokenService tokenService,
    IEmailService emailService,
    ISmsService smsService) {
    this.securityService = securityService;
    this.tokenService = tokenService;
    this.emailService = emailService;
    this.smsService = smsService;
    this.loginRepository = new LoginRepository(dbContext);
  }


  // { "email": "admin@company.com", "password": "P@ssw0rd", "rememberMe": true }
  // curl -X 'POST' 'http://localhost:18001/Login' -H 'Content-Type: application/json' -d '{ "email": "admin@company.com", "password": "P@ssw0rd", "rememberMe": true }'
  [AllowAnonymous]
  [HttpPost()]
  public async Task<Object> Login([FromBody] Login model) {
    var loginEntity = (await loginRepository.Get(model.Email)).FirstOrDefault();
    if (loginEntity != null && securityService.VerifyHashedPassword(loginEntity.PasswordHash, model.Password)) {
      var claims = loginEntity.Clients.Split(",").Select(client => new Claim("Client", client)).ToList();
      claims.Add(new Claim("FirstName", loginEntity.FirstName));
      claims.Add(new Claim("LastName", loginEntity.LastName));
      claims.Add(new Claim("LandingPage", loginEntity.LandingPage));

      return new LoginResponse {
        Token = tokenService.Build(
          loginEntity.UserId,
          loginEntity.Role.Split(","),
          claims
        ),
      };
    }
    throw new ApplicationException("Invalid Login");
  }
}
