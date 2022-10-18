using System.ComponentModel.DataAnnotations;

namespace Zuhid.BaseApi.Models;

public class Login {
  [Required]
  [StringLength(50, MinimumLength = 5)]
  public string Email { get; set; }

  [Required]
  [StringLength(20, MinimumLength = 7)]
  public string Password { get; set; }

  public string Tfa { get; set; }

  public bool RememberMe { get; set; }
}
