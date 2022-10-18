using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Zuhid.BaseApi;

public static class ModelBuilderExtension {
  public static void LoadData<TEntity>(this ModelBuilder builder) where TEntity : class {
    builder.Entity<TEntity>().HasData(JsonSerializer.Deserialize<List<TEntity>>(File.ReadAllText($"Dataload/{typeof(TEntity).Name}.json")));
  }
}
