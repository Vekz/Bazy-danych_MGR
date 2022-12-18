using Raven.Client.Documents.Session;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Infrastructure.Repositories.Interfaces;

namespace VegeShama.Infrastructure.Repositories.RavenDB
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IAsyncDocumentSession _docSession;

        public OrdersRepository(IAsyncDocumentSession docSession)
        {
            _docSession = docSession;
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
