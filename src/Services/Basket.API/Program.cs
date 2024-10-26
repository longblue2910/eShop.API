using Basket.API;
using Basket.API.Extensions;
using Common.Logging;
using Serilog;

Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information($"Start {builder.Environment.ApplicationName} up");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.AddAppConfigurations();
    builder.Services.AddConfigurationSettings(builder.Configuration);

    builder.Services.AddAutoMapper(
        cfg => cfg.AddProfile(new MappingProfile()));

    builder.Services.ConfigureServices();
    builder.Services.ConfigureHttpClinetService();

    builder.Services.ConfigureRedis();
    builder.Services.ConfigureGrpcServices();
    builder.Services.Configure<RouteOptions>(options
        => options.LowercaseQueryStrings = true);

    //Configure Mass Transit
    builder.Services.ConfigureMasstransit();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
            $"{builder.Environment.ApplicationName} v1"));
    }

    app.UseCors("CorsPolicy");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Basket API Complete");
    Log.CloseAndFlush();
}