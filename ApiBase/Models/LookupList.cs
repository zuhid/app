using System.ComponentModel.DataAnnotations;

namespace Zuhid.ApiBase.Models;

public class LookupList : BaseModel {
  [Required]
  public string Text { get; set; }
}
