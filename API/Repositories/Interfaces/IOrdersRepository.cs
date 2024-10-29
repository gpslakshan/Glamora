using API.Models.Domain.OrderAggregate;

namespace API.Repositories.Interfaces;

public interface IOrdersRepository
{
    Task CreateOrderAsync(Order order);
    Task<Order?> GetOrderByIdAsync(int id);
}
