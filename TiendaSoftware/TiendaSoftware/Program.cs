using Microsoft.AspNetCore.Identity;
using TiendaSoftware;
using TiendaSoftware.DataBase;
using TiendaSoftware.DataBase.Entities;



var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.configureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<TiendaSoftwareContext>();
        var userManager = services.GetRequiredService<UserManager<UserEntity>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await TiendaSoftwareSeeder.LoadDataAsync(context, loggerFactory, userManager, roleManager);
    }
    catch (Exception e)
    {
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(e, "Error al ejevutar el seed de datos");

        }
    }
}

app.Run();
