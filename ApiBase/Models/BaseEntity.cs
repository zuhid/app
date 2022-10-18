using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Zuhid.ApiBase.Models;

public class BaseEntity {
  public Guid Id { get; set; }

  // [JsonIgnore]
  public Guid UpdatedById { get; set; }

  [ConcurrencyCheck]
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  public DateTime Updated { get; set; }
  // public DateTimeOffset Updated { get; set; }
}
