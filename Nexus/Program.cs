using Microsoft.EntityFrameworkCore;
using Nexus.Domain.DataContext;
using Microsoft.OpenApi.Models;
using Nexus.Common.Response;
using Nexus.Application.Interfaces;
using Nexus.Application.Implementation;
using NetCore.AutoRegisterDi;
using System.Reflection;
using Nexus.Common.Helpers;

var builder = WebApplication.CreateBuilder(args);


var assembliesToScan = new[]
                               {
                                     Assembly.GetExecutingAssembly(),
                                     Assembly.GetAssembly(typeof(IUserService))
                                };

builder.Services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
   .Where(x => x.Name.EndsWith("Service"))
  .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
// Add services to the container

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IResponseModel, ResponseModel>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Nexus API",
        Version = "v1"
    });
});

// Register DbContext (only once)
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging());

var app = builder.Build();

// Run migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Context>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nexus API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
