using Common.Logging;
using Customer.API.Controllers;
using Customer.API.Extensions;
using Customer.API.Persistence;
using Customer.API.Services.Interfaces;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

Log.Information($"Start {builder.Environment.ApplicationName} up");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfigurations();

    //Add services to the container
    builder.Services.AddInfrastructure(builder.Configuration);
    var app = builder.Build();

    app.MapCustomerAPI();
    app.UseInfrastructure();

    app.MigrateDatabase<CustomerContext>((context, _) 
        =>
    { 
    });
    app.SeedCustomerData();
    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}

finally
{
    Log.Information("Shut down Product API complete");
    Log.CloseAndFlush();
}
