using Microsoft.EntityFrameworkCore;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Common.Enums;
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

            order.Address = new DB.Address()
            {
                Street = model.Address.Street,
                StreetNo = model.Address.StreetNumber,
                PostCode = model.Address.PostCode,
                City = model.Address.City,
            };

            order.OrderProducts = new List<DB.Order_Product>();
            var products = _dbContext.Product.Where(x => model.ProductIds.Contains(x.Id));
            await products.ForEachAsync(x =>
            {
                var order_product = new DB.Order_Product()
                {
                    Order = order,
                    Product = x,
                };

                order.OrderProducts.Add(order_product);
            });

            order.Payment = new DB.Payment()
            {
                DueDate = DateTime.Now.AddDays(7),
                Method = (byte)PaymentMethod.CreditCard,
                Status = (byte)PaymentStatus.PreAuthorization
            };

            var user = await _dbContext.User.FindAsync(model.UserId);
            order.Customer = user.Customer;
            user.Customer.Orders.Add(order);

            _dbContext.Add(order);
            await _dbContext.SaveChangesAsync();

            return new Order(order);
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            var order = await _dbContext.Order.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (order is null)
                return null;
            return new Order(order);
        }

        public async Task<List<Order>> GetOrdersForUser(Guid id)
        {
            var user = await _dbContext.User.FindAsync(id);
            return user?.Customer.Orders.Select(x => new Order(x)).ToList();
        }
    }
}
