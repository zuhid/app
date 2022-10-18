using System.ComponentModel.DataAnnotations;

namespace Zuhid.ApiBase.Models;

public class LookupListEntity : BaseEntity {
  [Required]
  [MaxLength(100)]
  public string Text { get; set; }
}
