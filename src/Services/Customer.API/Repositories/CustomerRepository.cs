using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories;

public class CustomerRepository(CustomerContext dbContext) : RepositoryQueryBase<Entities.Customer, int, CustomerContext>(dbContext), ICustomerRepository
{
    public async Task<Entities.Customer> GetCustomerByUserNameAsync(string username) => 
          await FindByCondition(x => x.UserName.Equals(username))
                   .SingleOrDefaultAsync();

    public async Task<IEnumerable<Entities.Customer>> GetCustomersAsync() => await FindAll().ToListAsync();
    
}
