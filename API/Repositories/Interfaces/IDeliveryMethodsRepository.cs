using API.Models.Domain;

namespace API.Repositories.Interfaces;

public interface IDeliveryMethodsRepository
{
    Task<IEnumerable<DeliveryMethod>> GetAllDeliveryMethodsAsync();
    Task<DeliveryMethod?> GetDeliveryMethodByIdAsync(int deliveryMethodId);
}
