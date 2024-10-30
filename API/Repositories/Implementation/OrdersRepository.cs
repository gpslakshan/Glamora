using API.Data;
using API.Models.Domain.OrderAggregate;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        return await dbContext.Orders
                    .Include(x => x.DeliveryMethod)
                    .Include(x => x.OrderItems)
                    .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Order?> GetOrderByPaymentIntentIdAsync(string paymentIntentId)
    {
        return dbContext.Orders
                .Include(x => x.DeliveryMethod)
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.PaymentIntentId == paymentIntentId);
    }

    public async Task<IEnumerable<Order>> GetOrdersForUserAsync(string userEmail)
    {
        return await dbContext.Orders
                    .Where(x => x.BuyerEmail == userEmail)
                    .OrderByDescending(x => x.OrderDate)
                    .Include(x => x.DeliveryMethod)
                    .Include(x => x.OrderItems)
                    .ToListAsync();
    }

    public async Task UpdateOrderAsync(Order order)
    {
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync();
    }
}
