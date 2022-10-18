namespace Zuhid.ApiBase;

public static class WebApplicationExtension {
  public static void UseBaseApp(this WebApplication app, string name, string version, IWebHostEnvironment env) {
    var CORS_ORIGIN = "CorsOrigins";
    if (env.IsDevelopment()) {
      app.UseDeveloperExceptionPage();
    }
    // app.AddProvider(new DatabaseLoggerProvider(app.Configuration.GetConnectionString("Log")));
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v{version}/swagger.json", $"{name} v{version}"));
    app.UseCors(CORS_ORIGIN);
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints => {
      // on the base page, show a simple message
      endpoints.MapGet("/", async context => await context.Response.WriteAsync("<html><body style='padding:100px 0;text-align:center;font-size:xxx-large;'>Api is running<br/><br/><a href='/swagger'>View Swagger</a></body></html>"));
      endpoints.MapControllers();
    });
  }
}
