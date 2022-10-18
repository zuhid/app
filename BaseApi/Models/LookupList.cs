using System.ComponentModel.DataAnnotations;

namespace Zuhid.BaseApi.Models;

public class LookupList : BaseModel {
  [Required]
  public string Text { get; set; }
}
