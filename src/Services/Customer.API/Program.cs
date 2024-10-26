using Common.Logging;
using Contracts.Common.Interfaces;
using Customer.API;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services;
using Customer.API.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Starting Customer API up");

try
{
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //Add connectionString
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    builder.Services.AddDbContext<CustomerContext>(
        options => options.UseNpgsql(connectionString));

    builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped(typeof(IRepositoryQueryBase<,,>), typeof(RepositoryQueryBase<,,>))
                .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
                .AddScoped<ICustomerService, CustomerService>();

    var app = builder.Build();

    app.MapGet("/", () => "Welcome to Customer API");

    app.MapGet("/api/customers",
        async (ICustomerService customerService) => await customerService.GetCustomersAsync());

    app.MapGet("/api/customers/{username}",
        async (string username, ICustomerService customerService) =>
        {
            var customer = await customerService.GetCustomerByUserNameAsync(username);
            return customer == null ? Results.NotFound() : Results.Ok(customer);

        });

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.SeedCustomerData()
       .Run();
}
catch (Exception ex)
{

    string type = ex.GetType().Name;

    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Customer API Complete");
    Log.CloseAndFlush();
}

