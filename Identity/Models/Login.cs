using System.ComponentModel.DataAnnotations;
using Zuhid.BaseApi.Models;

namespace Zuhid.Identity.Models;

public class Login : IModel {
  [Required]
  [StringLength(50, MinimumLength = 5)]
  public string UserName { get; set; }

  [Required]
  [StringLength(20, MinimumLength = 7)]
  public string Password { get; set; }

  public string Tfa { get; set; }

  public bool RememberMe { get; set; }
}
