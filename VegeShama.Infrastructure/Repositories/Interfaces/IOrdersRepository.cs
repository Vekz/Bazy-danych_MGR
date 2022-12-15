using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;

namespace VegeShama.Infrastructure.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        public Task<Order> AddOrder(AddOrderModel model);
        public Task<List<Order>> GetOrdersForUser(Guid id);
        public Task<Order> GetOrderById(Guid id);
    }
}
