using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Domain.Services.Interfaces;
using VegeShama.Infrastructure.Repositories.Interfaces;

namespace VegeShama.Domain.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Order> AddOrder(AddOrderModel model)
            => await _ordersRepository.AddOrder(model);

        public async Task<List<Order>> GetForUser(Guid id)
            => await _ordersRepository.GetOrdersForUser(id);

        public async Task<Order> GetOrder(Guid id)
            => await _ordersRepository.GetOrderById(id);
    }
}
