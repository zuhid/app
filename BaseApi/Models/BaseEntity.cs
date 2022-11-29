using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zuhid.BaseApi.Models;

public class BaseEntity {
  [Required]
  public Guid Id { get; set; }

  // [JsonIgnore]
  public Guid UpdatedById { get; set; }

  [ConcurrencyCheck]
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  public DateTime Updated { get; set; }
  // public DateTimeOffset Updated { get; set; }
}
