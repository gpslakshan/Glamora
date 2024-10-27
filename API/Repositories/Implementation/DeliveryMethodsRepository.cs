using API.Data;
using API.Models.Domain;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementation;

public class DeliveryMethodsRepository(AppDbContext context) : IDeliveryMethodsRepository
{
    public async Task<IEnumerable<DeliveryMethod>> GetAllDeliveryMethodsAsync()
    {
        return await context.DeliveryMethods.ToListAsync();
    }

    public async Task<DeliveryMethod?> GetDeliveryMethodByIdAsync(int deliveryMethodId)
    {
        return await context.DeliveryMethods.FindAsync(deliveryMethodId);
    }
}
