using System.Data;
using System.Reflection;
using Autofac.Core;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using vm.common.api;
using vm.common.api.Services.StartupDateS;
using vm.common.db.Services.HealthService;
using vm.common.Services.ConfigService;

namespace my.template.api;

/// <summary>
/// Provides startup configurations and dependency registrations for the application.
/// </summary>
internal static class Startup
{
  /// <summary>
  /// Registers dependencies with the specified web application builder.
  /// </summary>
  /// <param name="builder">The web application builder to configure.</param>
  internal static void DependencyInjectionRegister(WebApplicationBuilder builder)
  {
    var modules = new IModule[]
    {
      new vm.common.db.DbCommonModule(),
      new MyTemplateModule(),
    };

    builder.Services.AddAutoMapper(typeof(AutomapperProfile));
    vm.common.api.Startup.DependencyInjectionRegister(builder, modules);
  }

  /// <summary>
  /// Registers controllers and health checks with the specified service collection.
  /// </summary>
  /// <param name="services">The service collection to configure.</param>
  internal static void ControllersRegister(IServiceCollection services)
  {
    vm.common.api.Startup.ControllersRegister(services);
    services.AddHealthChecks()
            .AddCheck<IDbHealthService>("database_health_check");
  }

  /// <summary>
  /// Registers the database connection configuration with the service collection.
  /// </summary>
  /// <param name="services">The service collection to configure.</param>
  internal static void DatabaseRegister(IServiceCollection services)
  {
    services.AddScoped<IDbConnection>(sp =>
    {
      var config = sp.GetRequiredService<IConfigService>();
      var databaseProvider = config.GetConfig(vm.common.db.Config.Database.DatabaseProvider);

      return databaseProvider switch
      {
        "MySql" => new MySqlConnection(config.GetConnectionString(vm.common.db.Config.Database.MySqlConnection)),
        _ => throw new InvalidOperationException($"Invalid database provider specified. Database selected: {databaseProvider}."),
      };
    });
  }

  /// <summary>
  /// Configures Swagger API versioning and metadata for the application.
  /// </summary>
  /// <param name="services">The service collection to configure.</param>
  public static void ConfigureSwaggerApiVersioning(IServiceCollection services)
  {
    var info = new OpenApiInfo()
    {
      Title = "my.template.api",
      Description = "Description for my.template.api",
      Contact = new OpenApiContact { Name = "Daniel Viveros", Email = "dviveros90@gmail.com", },
    };
    SwaggerConfigureOptions.SetOpenApiInfo(info);
    var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
    vm.common.api.Startup.ConfigureSwaggerApiVersioning(services, assemblyName);
  }

  /// <summary>
  /// Runs initial settings for the application, such as initializing services.
  /// </summary>
  /// <param name="app">The application builder to configure.</param>
  internal static void ConfigureApp(IApplicationBuilder app)
  {
    using var scope = vm.common.api.Startup.ConfigureApp(app);
    var startup = scope.ServiceProvider.GetRequiredService<IStartupDateService>();

    startup.SetAndGetStartupDate();
  }
}
