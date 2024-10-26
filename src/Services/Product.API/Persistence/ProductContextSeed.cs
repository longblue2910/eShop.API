using Product.API.Entities;
using ILogger = Serilog.ILogger;

namespace Product.API.Persistence;

public static class ProductContextSeed
{

    public static async Task SeedProductAsync(ProductContext productContext, ILogger logger)
    {
        if (!productContext.Products.Any())
        {
            productContext.AddRange(CatalogProducts);
            await productContext.SaveChangesAsync();
            logger.Information("Seeded data for Product DB associated with context {DbContextName}", nameof(ProductContext));
        }
    }

    private static IEnumerable<CatalogProduct> CatalogProducts => [
            new()
            {
                No = "Lotus",
                Name = "Esprit",
                Summary = "None",
                Description = "None",
                Price = (decimal)1234562.12
            },
            new()
            {
                No = "Cadillac",
                Name = "CTS",
                Summary = "None",
                Description = "None",
                Price = (decimal)40144562.12
            }
        ];
}
