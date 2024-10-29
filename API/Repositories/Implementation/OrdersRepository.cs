using API.Data;
using API.Models.Domain.OrderAggregate;
using API.Repositories.Interfaces;

namespace API.Repositories.Implementation;

public class OrdersRepository(AppDbContext dbContext) : IOrdersRepository
{
    public async Task CreateOrderAsync(Order order)
    {
        await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await dbContext.Orders.FindAsync(id);
    }
}
