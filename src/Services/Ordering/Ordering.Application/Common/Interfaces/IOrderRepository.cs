using Contracts.Common.Interfaces;
using Ordering.Domain.Entities;

namespace Ordering.Application.Common.Interfaces;

public interface IOrderRepository : IRepositoryBaseAsync<Order, long>
{
    Task<IEnumerable<Order>> GetOrdersByUsername(string username);
    Task<Order> CreateOrderAsync(Order orderDto);
    Task<Order> UpdateOrderAsync(Order orderDto);
    Task<Order> GetOrderByDocumentNo(string documentNo);
}
