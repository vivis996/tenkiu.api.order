using tenkiu.api.order;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
vm.common.api.Startup.SettingsConfiguration(builder.Services, builder.Environment);
Startup.DependencyInjectionRegister(builder);
Startup.ControllersRegister(builder.Services);
Startup.DatabaseRegister(builder.Services);

// Configure API versioning
vm.common.api.Startup.ConfigureApiVersioning(builder.Services);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

if (builder.Environment.IsDevelopment())
{
  Startup.ConfigureSwaggerApiVersioning(builder.Services);
}

var app = builder.Build();

Startup.ConfigureApp(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();

  app.UseSwaggerUI(options =>
  {
    options.EnableTryItOutByDefault();
    
    // Build a swagger endpoint for each discovered API version
    foreach (var description in app.DescribeApiVersions())
    {
      options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                              description.GroupName.ToUpperInvariant());
    }
  });
}

app.UseHttpsRedirection();

app.UseCors(x => x
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
app.UseAuthorization();


app.MapControllers();
app.MapHealthChecks("/api/order/health");

app.Run();
