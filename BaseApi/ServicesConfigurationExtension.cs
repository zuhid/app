using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Zuhid.BaseApi.Models;

namespace Zuhid.BaseApi;

public static class ServicesConfigurationExtension {
  public static void AddBaseServices(this IServiceCollection services, string title, string version, string corsOrigin, IdentityModel identityModel) {
    var CORS_ORIGIN = "CorsOrigins";

    services
      .AddAuthentication()
      .AddJwtBearer("Bearer", option => new JwtTokenService(identityModel).Configure(option));

    services.AddCors(options => {
      options.AddPolicy(CORS_ORIGIN, builder => {
        builder.WithOrigins(corsOrigin)
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials();
      });
    });
    services
      .AddControllers(options => {
        options.Filters.Add(new AuthorizeFilter());
        options.Filters.Add(typeof(ActionFilter));
        options.Filters.Add(typeof(ExceptionFilter));
        // options.Filters.Add(new ProducesAttribute("application/json", "application/xml", "text/csv"));
        // options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
        options.OutputFormatters.Add(new CsvFormatter());
      })
      .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
    services.AddSwaggerGen(options => AddSwagger(options, title, version));
    services.AddScoped<ITokenService>(option => new JwtTokenService(identityModel)); // Add identity service
  }

  public static void AddDatabase<ITContext, TContext>(this IServiceCollection services, string connectionString)
    where ITContext : class
    where TContext : DbContext, ITContext {
    services.AddDbContext<TContext>(options => options
      .UseSqlServer(connectionString)
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // don't track entities
      .EnableSensitiveDataLogging() // log sql param values
    );
    services.AddScoped<ITContext, TContext>();
  }

  private static void AddSwagger(SwaggerGenOptions options, string title, string version) {
    options.SwaggerDoc($"v{version}", new OpenApiInfo {
      Title = $"{title} Api",
      Version = version
    });
    var xmlFile = $"Zuhid.{title}.xml";
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) {
      options.IncludeXmlComments(xmlPath);
    }

    // AddSecurityDefinition and AddSecurityRequirement needed to allow users to pass in jwt token
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
      In = ParameterLocation.Header,
      Description = "Please insert JWT with Bearer into field",
      Name = "Authorization",
      Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {{
        new OpenApiSecurityScheme{
          Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
          }
        },
        new string[] { }
        }});
    options.CustomSchemaIds(x => x.Name.Replace("Model", "")); // Display the model without the word "Model"
    options.EnableAnnotations();
  }
}
