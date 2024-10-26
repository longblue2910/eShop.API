using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence;

public static class CustomerContextSeed
{
    public static IHost SeedCustomerData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var customerContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();
        customerContext.Database.MigrateAsync().GetAwaiter().GetResult();

        CreatCustomer(customerContext, "customer1", "Tran", "Thi Thuy Trang", "thuytrang291099@gmail.com").GetAwaiter().GetResult();
        CreatCustomer(customerContext, "customer2", "Phan", "Tien Long", "tienlong3005@gmail.com").GetAwaiter().GetResult();

        return host;
    }

    private static async Task CreatCustomer(CustomerContext customerContext, string username, string firstname, string lastname, string email)
    {
        var customer = await customerContext.Customers.SingleOrDefaultAsync(x => x.UserName.Equals(username) || 
                                                                                 x.EmailAddress.Equals(email));

        if (customer == null)
        {
            var newCustomer = new Entities.Customer
            {
                UserName = username,
                FirstName = firstname,
                LastName = lastname,
                EmailAddress = email
            };

            await customerContext.Customers.AddAsync(newCustomer);
            await customerContext.SaveChangesAsync();
        }
    }
}
