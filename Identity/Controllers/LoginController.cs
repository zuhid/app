using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zuhid.BaseApi;
using Zuhid.BaseApi.Models;
using Zuhid.Identity.Repositories;
using Zuhid.Identity.Validators;
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
    var loginEntity = await loginRepository.Get(model.Email);
    if (loginEntity.Count > 0 && securityService.VerifyHashedPassword(loginEntity.FirstOrDefault().PasswordHash, model.Password)) {
      return new LoginResponse {
        Token = tokenService.Build(
          loginEntity.FirstOrDefault().UserId,
          loginEntity.Select(n => n.Client).Distinct().Select(client => new Claim("Client", client)).ToList(),
          loginEntity.Select(role => role.Role).Distinct().ToList()
        ),
        LandingPage = loginEntity.FirstOrDefault().LandingPage
      };
    }
    throw new ApplicationException("Invalid Login");
  }
}

// curl -X 'POST' 'http://localhost:18011/Login/TfaToken' -H 'Content-Type: application/json' -d '"admin@company.com"'
// private async Task<bool> TfaToken([FromBody] Login model) {
//   // var userEntity = await userManager.FindByNameAsync(model.UserName);
//   // if (userEntity != null) {
//   //   var signInResult = await signInManager.CheckPasswordSignInAsync(userEntity, model.Password, false);
//   //   if (signInResult.Succeeded && !string.IsNullOrWhiteSpace(userEntity.PhoneNumber)) {
//   //     var token = await userManager.GenerateChangePhoneNumberTokenAsync(userEntity, userEntity.PhoneNumber);
//   //     if (token != null) {
//   //       return await smsService.Send(userEntity.PhoneNumber, token);
//   //     }
//   //   }
//   // }
//   throw new ApplicationException("Invalid Login");
// }

