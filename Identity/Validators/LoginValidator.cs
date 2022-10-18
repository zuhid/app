using Microsoft.AspNetCore.Identity;
using Zuhid.BaseApi;
using Zuhid.BaseApi.Models;

namespace Zuhid.Identity.Validators;

public class LoginValidator : IValidator<Login> {
  public List<(string, string)> Validate(Login login) {
    var errorList = new List<(string, string)>();
    // if (signInResult.IsLockedOut) {
    //   errorList.Add(("Error", "Account is locked"));
    // } else if (signInResult.IsNotAllowed) {
    //   errorList.Add(("Error", "Login is not allowed"));
    // } else if (signInResult.RequiresTwoFactor) {
    //   errorList.Add(("Error", "Two factor authentication is required"));
    // } else if (!signInResult.Succeeded) {
    //   errorList.Add(("Error", "Login Failed"));
    // }
    return errorList;
  }
}
