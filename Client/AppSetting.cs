using Zuhid.BaseApi.Models;

namespace Zuhid.Client;

public class AppSetting {
  public AppSetting() { }
  public AppSetting(IConfiguration configuration) {
    configuration.GetSection("AppSettings").Bind(this);
    this.ClientContext = configuration.GetConnectionString("ClientContext");
    this.LogContext = configuration.GetConnectionString("LogContext");
  }

  public string Name { get; set; }
  public string Version { get; set; }
  public string CorsOrigin { get; set; }
  public string ClientContext { get; set; }
  public string LogContext { get; set; }
  public IdentityModel Identity { get; set; } = new IdentityModel();
}
