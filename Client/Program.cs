using Zuhid.BaseApi;
using Zuhid.Tools;

namespace Zuhid.Client;

public class Program {

  public static void Main(string[] args) {
    // services
    var builder = WebApplication.CreateBuilder(args);
    var appSetting = new AppSetting(builder.Configuration);
    builder.Services.AddBaseServices(appSetting.Name, appSetting.Version, appSetting.CorsOrigin, appSetting.Identity);
    // builder.Services.AddDatabase<ClientContext, ClientContext>(appSetting.ClientContext);
    builder.Logging.AddProvider(new DatabaseLoggerProvider(appSetting.LogContext));

    // app
    var app = builder.Build();
    app.UseBaseApp(appSetting.Name, appSetting.Version, app.Environment);
    app.Run();
  }
}
