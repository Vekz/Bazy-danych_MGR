using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Common.Enums;
using VegeShama.Infrastructure.Repositories.Interfaces;
using DB = VegeShama.Common.DatabaseModels.RavenDB;

namespace VegeShama.Infrastructure.Repositories.RavenDB
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IAsyncDocumentSession _docSession;

        public OrdersRepository(IAsyncDocumentSession docSession)
        {
            _docSession = docSession;
        }

        public async Task<Order> AddOrder(AddOrderModel model)
        {
            var products = _docSession.Query<DB.Product>().Where(x => x.Id.In(model.ProductIds.Select(x => x.ToString()))).ToListAsync();

            var address = new DB.Address()
            {
                Id = String.Empty,
                Street = model.Address.Street,
                StreetNo = model.Address.StreetNumber,
                PostCode = model.Address.PostCode,
                City = model.Address.City,
            };

            var payment = new DB.Payment()
            {
                Id = String.Empty,
                DueDate = DateTime.Now.AddDays(7),
                Method = (byte)PaymentMethod.CreditCard,
                Status = (byte)PaymentStatus.PreAuthorization
            };

            var order = new DB.Order()
            {
                Id = String.Empty,
                DueDate = DateTime.Now.AddDays(7),
                DeliveryDate = DateTime.Now.AddDays(12),
                Products = await products,
                Address = address,
                Payment = payment
            };

            var user = await _docSession.LoadAsync<DB.User>(model.UserId.ToString());
            user.Orders.Add(order);

            await _docSession.StoreAsync(payment);
            await _docSession.StoreAsync(address);
            await _docSession.StoreAsync(order);
            await _docSession.SaveChangesAsync();

            return new Order(order);
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            var order = await _docSession.LoadAsync<DB.Order>(id.ToString());
            return new Order(order);
        }

        public async Task<List<Order>> GetOrdersForUser(Guid id)
        {
            var user = await _docSession.LoadAsync<DB.User>(id.ToString());
            return user.Orders.Select(x => new Order(x)).ToList();
        }
    }
}
