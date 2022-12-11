using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;

namespace VegeShama.Domain.Services.Interfaces
{
    public interface IOrdersService
    {
        public Task<Order> GetOrder(Guid id);
        public Task<Order> AddOrder(AddOrderModel model);
        public Task<List<Order>> GetForUser(Guid id);
    }
}
