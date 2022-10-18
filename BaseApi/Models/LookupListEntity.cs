using System.ComponentModel.DataAnnotations;

namespace Zuhid.BaseApi.Models;

public class LookupListEntity : BaseEntity {
  [Required]
  [MaxLength(100)]
  public string Text { get; set; }
}
