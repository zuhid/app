using System.ComponentModel.DataAnnotations;

namespace Zuhid.BaseApi.Models;

public class LookupList : BaseModel {
  [Required]
  public Guid Id { get; set; }

  [Required]
  public string Text { get; set; }
}
