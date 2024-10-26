using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Serilog;

namespace Ordering.Infrastructure.Persistence;

public class OrderContextSeed(ILogger logger, OrderContext orderContext)
{
    private readonly ILogger _logger = logger;
    private readonly OrderContext _context = orderContext;

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.Error("An error occurred while initialising the database.", ex.Message);
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.Error("An error occurred while initialising the database.", ex.Message);
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.Orders.Any())
        {
            await _context.Orders.AddRangeAsync(
                new Order
                {
                    UserName = "customer1",
                    FirstName = "Tran Thi",
                    LastName = "Thuy Trang",
                    EmailAddress = "trangtran29101705@gmail.com",
                    ShippingAddress = "Hue City",
                    InvoiceAddress = "Viet Nam"   
                });
        }
    }
}
