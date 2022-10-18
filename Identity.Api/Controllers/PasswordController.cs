using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zuhid.ApiBase;
using Zuhid.Identity.Api.Entities;
using Zuhid.Tools;

namespace Zuhid.Identity.Api.Controllers;

/// <summary>
/// Password user
/// </summary>
public class PasswordController : BaseController {
  private readonly UserManager<UserEntity> userManager;
  private readonly IEmailService emailService;

  /// <summary>
  /// constructor
  /// </summary>
  /// <param name="userManager">manage user</param>
  /// <param name="emailService">email service</param>
  public PasswordController(UserManager<UserEntity> userManager, IEmailService emailService) {
    this.userManager = userManager;
    this.emailService = emailService;
  }

  /// <summary>
  /// constructor
  /// </summary>
  /// curl -X 'POST' 'http://localhost:5001/Password/ResetToken' -H 'Content-Type: application/json' -d '"admin@company.com"'
  [AllowAnonymous]
  [HttpPost("ResetToken")]
  public async Task<bool> ResetToken([FromBody] string userName) {
    var userEntity = await userManager.FindByNameAsync(userName);
    if (userEntity != null) {
      var token = await userManager.GeneratePasswordResetTokenAsync(userEntity);
      if (token != null) {
        return await emailService.Send(userEntity.Email, "Reset Passowrd Token", token);
      }
    }
    return false;
  }

  /// <summary>
  /// constructor
  /// </summary>
  /// curl -X 'POST' 'http://localhost:5001/Password/TfaToken' -H 'Content-Type: application/json' -d '"admin@company.com"'
  [AllowAnonymous]
  [HttpPost("TfaToken")]
  public async Task<bool> TfaToken([FromBody] string userName) {
    var userEntity = await userManager.FindByNameAsync(userName);
    if (userEntity != null) {
      var token = await userManager.GenerateChangePhoneNumberTokenAsync(userEntity, userEntity?.PhoneNumber);
      if (token != null) {
        return await emailService.Send(userEntity.Email, "Two factor Authentication Token", token);
      }
    }
    return false;
  }
}
