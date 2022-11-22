using Zuhid.BaseApi.Models;

namespace Zuhid.Identity;

public class AppSetting {
  public AppSetting() { }
  public AppSetting(IConfiguration configuration) {
    configuration.GetSection("AppSettings").Bind(this);
    this.IdentityDbContext = configuration.GetConnectionString("IdentityDbContext");
    this.LogContext = configuration.GetConnectionString("LogContext");
    // this.Name = configuration.GetValue<string>("AppSettings:Name");
    // this.Version = configuration.GetValue<string>("AppSettings:Version");
    // this.CorsOrigin = configuration.GetValue<string>("AppSettings:CorsOrigin");
    // configuration.GetSection("AppSettings:Identity").Bind(this.Identity);
  }

  public string Name { get; set; }
  public string Version { get; set; }
  public string CorsOrigin { get; set; }
  public string IdentityDbContext { get; set; }
  public string LogContext { get; set; }
  public IdentityModel Identity { get; set; } = new IdentityModel();
}
