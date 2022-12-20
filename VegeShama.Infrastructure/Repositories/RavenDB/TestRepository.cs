using Raven.Client.Documents.Session;
using VegeShama.Common.DatabaseModels.RavenDB;
using VegeShama.Common.Enums;
using VegeShama.Infrastructure.Repositories.Interfaces;

namespace VegeShama.Infrastructure.Repositories.RavenDB
{
    public class TestRepository : ITestRepository
    {
        private readonly IDocumentSession _docSession;

        public TestRepository(IDocumentSession docSession)
        {
            _docSession = docSession;
        }

        public void AddComplexEntity()
        {
            var products = new List<Product>();

            var address = new Address()
            {
                Id = String.Empty,
                Street = "Fake",
                StreetNo = "Fake",
                PostCode = "Fake",
                City = "Fake",
            };

            var payment = new Payment()
            {
                Id = String.Empty,
                DueDate = DateTime.Now.AddDays(7),
                Method = (byte)PaymentMethod.CreditCard,
                Status = (byte)PaymentStatus.PreAuthorization
            };

            var order = new Order()
            {
                Id = String.Empty,
                DueDate = DateTime.Now.AddDays(7),
                DeliveryDate = DateTime.Now.AddDays(12),
                Products = products,
                Address = address,
                Payment = payment
            };

            _docSession.Store(payment);
            _docSession.Store(address);
            _docSession.Store(order);
            _docSession.SaveChanges();
        }

        public void AddSimpleEntity()
        {
            var product = new Product()
            {
                Name = "FakeAddName",
                Price = 200,
                Category = "FakeAddCategory",
                Id = String.Empty,
            };

            _docSession.Store(product);
            _docSession.SaveChanges();
        }

        public void GetAllComplexEntities()
            => _docSession.Query<Order>().ToList();

        public void GetAllSimpleEntities()
            => _docSession.Query<Product>().ToList();

        public void GetComplexEntity(Guid id)
            => _docSession.Load<Order>(id.ToString());

        public void GetComplexEntityWithIndex(Guid id)
            => _docSession.Load<Order>(id.ToString());

        public Guid GetFirstComplexEntityId()
            => new Guid(_docSession.Query<Order>().Select(x => x.Id).First());

        public Guid GetFirstSimpleEntityId()
            => new Guid(_docSession.Query<Product>().Select(x => x.Id).First());

        public void GetSimpleEntity(Guid id)
            => _docSession.Load<Product>(id.ToString());

        public int GetEntitiesCount()
            => _docSession.Query<Product>().Count();

        public void GetSimpleEntityWithIndex(Guid id)
            => _docSession.Load<Product>(id.ToString());

        public void UpdateComplexEntity(Guid id)
        {
            var order = _docSession.Load<Order>(id.ToString());

            order.DueDate = DateTime.Now.AddDays(3);
            order.Address.PostCode = "FakePostCodeUpdate";

            _docSession.SaveChanges();
        }

        public void UpdateSimpleEntity(Guid id)
        {
            var product = _docSession.Load<Product>(id.ToString());

            product.Name = "FakeNameUpdate";
            product.Category = "FakeCategoryUpdate";

            _docSession.SaveChanges();
        }

        public void DeleteSimpleEntity(Guid id)
        {
            _docSession.Delete(id.ToString());
            _docSession.SaveChanges();
        }

        public void DeleteComplexEntity(Guid id)
        {
            _docSession.Delete(id.ToString());
            _docSession.SaveChanges();
        }
    }
}
