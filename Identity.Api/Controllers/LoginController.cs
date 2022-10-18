using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zuhid.ApiBase;
using Zuhid.ApiBase.Models;
using Zuhid.Identity.Api.Repositories;
using Zuhid.Identity.Api.Validators;
using Zuhid.Tools;
// using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Zuhid.Identity.Api.Controllers;

/// <summary>
/// Login user
/// </summary>
[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase {
  private readonly ITokenService tokenService;
  private readonly IEmailService emailService;
  private readonly ISmsService smsService;
  // private readonly IValidator<SignInResult> validator;
  private readonly UserRepository userRepository;
  private readonly UserToClientRepository userToClientRepository;
  private readonly UserToRoleRepository userToRoleRepository;
  public readonly ISecurityService securityService;

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
    // this.validator = new LoginValidator();
    this.userRepository = new UserRepository(dbContext);
    this.userToClientRepository = new UserToClientRepository(dbContext);
    this.userToRoleRepository = new UserToRoleRepository(dbContext);
  }


  // { "email": "admin@company.com", "password": "P@ssw0rd", "rememberMe": true }
  // curl -X 'POST' 'http://localhost:18001/Login' -H 'Content-Type: application/json' -d '{ "email": "admin@company.com", "password": "P@ssw0rd", "rememberMe": true }'
  [AllowAnonymous]
  [HttpPost()]
  public async Task<Object> Login([FromBody] Login model) {
    var userEntity = await userRepository.Get(model.Email);
    if (userEntity != null) {
      var userToClient = await userToClientRepository.Get(userEntity.Id);
      var userToRole = await userToRoleRepository.Get(userEntity.Id);

      if (securityService.VerifyHashedPassword(userEntity.PasswordHash, model.Password)) {
        var claims = userToClient.Select(n => new Claim("Client", n)).ToList();
        var roles = userToRole;
        return new LoginResponse {
          Token = tokenService.Build(userEntity.Id, claims, roles),
          LandingPage = userEntity.LandingPage
        };
      }
    }
    throw new ApplicationException("Invalid Login");
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

  // /// <summary></summary>
  // /// <example> { "userName": "admin@company.com", "password": "P@ssw0rd", "rememberMe": true } </example>
  // /// <param name="model"></param>
  // /// <returns></returns>
  // [AllowAnonymous]
  // [HttpPost()]
  // // curl -X 'POST' 'http://localhost:5001/Login/TfaToken' -H 'Content-Type: application/json' -d '"admin@company.com"'
  // public async Task<LoginResponse> Login([FromBody] Login model) {
  //   var userEntity = await userManager.FindByNameAsync(model.UserName);
  //   if (userEntity != null) {
  //     var signInResult = await signInManager.CheckPasswordSignInAsync(userEntity, model.Password, false);
  //     if (signInResult.Succeeded) {
  //       // if (signInResult.RequiresTwoFactor) {
  //       var isTfaValid = await userManager.VerifyChangePhoneNumberTokenAsync(userEntity, model.Tfa, userEntity.PhoneNumber);
  //       if (isTfaValid) {
  //         var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
  //         if (result.Succeeded && ModelState.IsValid) {
  //           var claims = await userManager.GetClaimsAsync(userEntity);
  //           var roles = await userManager.GetRolesAsync(userEntity);
  //           return new LoginResponse {
  //             RequireTfa = false,
  //             Token = tokenService.Build(userEntity.Id, claims, roles)
  //           };
  //         }
  //       }
  //       // }
  //     }
  //   }
  //   return null;
  // }

  // [AllowAnonymous]
  // [HttpPost()]
  // public async Task<LoginResponse> Login([FromBody] Login model) {
  //   var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
  //   validator.Validate(result)?.ForEach(item => ModelState.AddModelError(item.Item1, item.Item2));

  //   if (result.Succeeded && ModelState.IsValid) {
  //     var userEntity = await userManager.FindByNameAsync(model.UserName);
  //     var claims = await userManager.GetClaimsAsync(userEntity);
  //     var roles = await userManager.GetRolesAsync(userEntity);
  //     return new LoginResponse {
  //       RequireTfa = false,
  //       Token = tokenService.Build(userEntity.Id, claims, roles)
  //     };

  //     // if (await userManager.VerifyChangePhoneNumberTokenAsync(userEntity, model.Token, userEntity.PhoneNumber)) {
  //     //   var claims = await userManager.GetClaimsAsync(userEntity);
  //     //   var roles = await userManager.GetRolesAsync(userEntity);
  //     //   return new LoginResponse {
  //     //     RequireTfa = false,
  //     //     Token = tokenService.Build(userEntity.Id, claims, roles)
  //     //   };
  //     // }
  //   }
  //   return null;
  // }

  // [AllowAnonymous]
  // [HttpPost("GenerateTFA")]
  // public async Task<IActionResult> GenerateTFA([FromBody] LoginModel model) {
  //   if (ModelState.IsValid) {
  //     var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
  //     new LoginValidator().Validate(result)?.ForEach(item => ModelState.AddModelError(item.Item1, item.Item2));

  //     if (result.Succeeded && ModelState.IsValid) {
  //       var userEntity = await userManager.FindByNameAsync(model.UserName);
  //       var token = await userManager.GenerateChangePhoneNumberTokenAsync(userEntity, userEntity.PhoneNumber);

  //       if (await emailService.Send(model.UserName, "Two Factor Authentication Token", token)) {
  //         return Ok();
  //       } else {
  //         ModelState.AddModelError("Error", "Unable to send TFA Token");
  //       }
  //     }
  //   }
  //   return BadRequest(ModelState);
  // }
}
