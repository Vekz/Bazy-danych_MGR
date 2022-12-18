using Microsoft.EntityFrameworkCore;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.DAL.EFCore;
using VegeShama.Infrastructure.Repositories.Interfaces;
using DB = VegeShama.Common.DatabaseModels.EFCore;

namespace VegeShama.Infrastructure.Repositories.EFCore
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly EFVegeContext _dbContext;

        public OrdersRepository(EFVegeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> AddOrder(AddOrderModel model)
        {
            var order = new DB.Order()
            {
                DueDate = DateTime.Now.AddDays(7),
                DeliveryDate = DateTime.Now.AddDays(12),
            };

            var address = new DB.Address()
            {
                Street = model.Address.Street,
                StreetNo = model.Address.StreetNumber,
                PostCode = model.Address.PostCode,
                City = model.Address.City,
            };
            order.Address = address;

            var products = _dbContext.Product.Where(x => model.ProductIds.Contains(x.Id));
            order.Products.AddRange(products);

            var user = await _dbContext.User.FindAsync(model.UserId);
            order.Customer = user.Customer;
            user.Customer.Orders.Add(order);

            _dbContext.Add(order);
            await _dbContext.SaveChangesAsync();

            return new Order(order);
        }

        public async Task<Order> GetOrderById(Guid id)
            => await _dbContext.Order.Where(x => x.Id == id).Select(x => new Order(x)).FirstOrDefaultAsync();

        public async Task<List<Order>> GetOrdersForUser(Guid id)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Id == id);
            return user?.Customer.Orders.Select(x => new Order(x)).ToList();
        }
    }
}
