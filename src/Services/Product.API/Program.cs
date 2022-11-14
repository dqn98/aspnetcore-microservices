using Common.Logging;
using Product.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Starting product API up");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfigurations();

    builder.Services.AddInfrastructure();

    var app = builder.Build();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandle exception");
}

finally
{
    Log.Information("Shut down Product API complete");
    Log.CloseAndFlush();
}
