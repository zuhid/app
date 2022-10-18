using Microsoft.AspNetCore.Identity;
using Zuhid.ApiBase;

namespace Zuhid.Identity.Api.Validators;

public class LoginValidator : IValidator<SignInResult> {
  public List<(string, string)> Validate(SignInResult signInResult) {
    var errorList = new List<(string, string)>();
    if (signInResult.IsLockedOut) {
      errorList.Add(("Error", "Account is locked"));
    } else if (signInResult.IsNotAllowed) {
      errorList.Add(("Error", "Login is not allowed"));
    } else if (signInResult.RequiresTwoFactor) {
      errorList.Add(("Error", "Two factor authentication is required"));
    } else if (!signInResult.Succeeded) {
      errorList.Add(("Error", "Login Failed"));
    }
    return errorList;
  }
}
