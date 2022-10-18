using Microsoft.AspNetCore.Identity;
using Zuhid.BaseApi;
using Zuhid.Identity.Entities;
using Zuhid.Tools;

namespace Zuhid.Identity;

public class Program {

  public static void Main(string[] args) {

    // services
    var builder = WebApplication.CreateBuilder(args);
    var appSetting = new AppSetting(builder.Configuration);
    builder.Services.AddBaseServices(appSetting.Name, appSetting.Version, appSetting.CorsOrigin, appSetting.Identity);
    builder.Services.AddDatabase<IDbContext, IdentityDbContext>(appSetting.IdentityDbContext);
    // builder.Services.AddIdentity<UserEntity, RoleEntity>().AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders();

    builder.Services.AddScoped<ISecurityService, SecurityService>();
    builder.Services.AddScoped<IEmailService>(option => new TestEmailService(""));
    builder.Services.AddScoped<ISmsService>(option => new TestSmsService(""));

    // app
    var app = builder.Build();
    app.UseBaseApp(appSetting.Name, appSetting.Version, app.Environment);
    app.Run();
  }
}
