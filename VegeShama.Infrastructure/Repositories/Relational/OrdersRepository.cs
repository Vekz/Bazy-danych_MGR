using System.Data;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Infrastructure.Repositories.Interfaces;

namespace VegeShama.Infrastructure.Repositories.Relational
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IDbConnection _dbConnection;

        public OrdersRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<Order> AddOrder(AddOrderModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrdersForUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
