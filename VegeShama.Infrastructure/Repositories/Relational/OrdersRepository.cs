using Dapper;
using System.Data;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Common.Enums;
using VegeShama.Infrastructure.Repositories.Interfaces;
using DB = VegeShama.Common.DatabaseModels.Relational;

namespace VegeShama.Infrastructure.Repositories.Relational
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IDbConnection _dbConnection;

        public OrdersRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
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
                PostCode = model.Address.PostCode
            };

            order.Address.Post = new DB.PostCode_DB()
            {
                PostCode = model.Address.PostCode,
                City = model.Address.City
            };

            order.Payment = new DB.Payment()
            {
                DueDate = DateTime.Now.AddDays(7),
                Method = (byte)PaymentMethod.CreditCard,
                Status = (byte)PaymentStatus.PreAuthorization
            };

            await _dbConnection.ExecuteAsync("INSERT INTO PostCode (PostCode, City) VALUES (@PostCode, @City)", order.Address.Post);
            order.CustomerId = await _dbConnection.QuerySingleAsync<Guid>("SELECT Id FROM Customer c WHERE c.UserId = @id", new { id = model.UserId });
            order.PaymentId = await _dbConnection.QuerySingleAsync<Guid>(@"INSERT INTO Payment (DueDate, Method, Status)
                                                                        OUTPUT INSERTED.Id
                                                                        VALUES (@DueDate, @Method, @Status);",
                                                                        order.Payment);
            order.AddressId = await _dbConnection.QuerySingleAsync<Guid>(@"INSERT INTO Address (Street, StreetNo, PostCode)
                                                                        OUTPUT INSERTED.Id
                                                                        VALUES (@Street, @StreetNo, @PostCode);",
                                                                        order.Address);
            order.Id = await _dbConnection.QuerySingleAsync<Guid>(@"INSERT INTO [dbo].[Order] (DueDate, DeliveryDate, CustomerId, PaymentId, AddressId)
                                                                        OUTPUT INSERTED.Id
                                                                        VALUES (@DueDate, @DeliveryDate, @CustomerId, @PaymentId, @AddressId);",
                                                                        order);

            var orderIdProductIdPairList = model.ProductIds.Select(x => new { A = order.Id, B = x });
            await _dbConnection.ExecuteAsync("INSERT INTO Order_Product (OrderId, ProductId) VALUES (@A, @B)", orderIdProductIdPairList);

            return await GetOrderById(order.Id);
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            var userOrderQuery = "SELECT o.*, a.*, p.* FROM [dbo].[Order] o JOIN Address a ON o.AddressId = a.Id JOIN Payment p ON o.PaymentId = p.Id WHERE o.Id = @id";
            var orderDB = await _dbConnection.QueryAsync<DB.Order, DB.Address, DB.Payment, DB.Order>(
                userOrderQuery,
                (ord, addr, paym) =>
                {
                    ord.Address = addr;
                    ord.Payment = paym;

                    return ord;
                },
                new { id }
            );
            var order = orderDB.FirstOrDefault();
            if (order is null)
                return null;

            var orderProductsQuery = "SELECT p.*, c.* FROM Product p JOIN Order_Product op ON op.ProductId = p.Id JOIN Category c ON p.CategoryId = c.Id WHERE op.OrderId = @id";
            var productsDB = await _dbConnection.QueryAsync<DB.Product, DB.Category, DB.Product>(
                orderProductsQuery,
                (prod, cat) =>
                {
                    prod.Category = cat;
                    return prod;
                },
                new { id = order.Id }
            );
            order.Products = productsDB.ToList();

            var postCodeQuery = "SELECT * FROM PostCode pc WHERE pc.PostCode = @pc";
            var postCode = await _dbConnection.QuerySingleAsync<DB.PostCode_DB>(postCodeQuery, new { pc = order.Address.PostCode });
            order.Address.Post = postCode;

            return new Order(order);
        }

        public async Task<List<Order>> GetOrdersForUser(Guid id)
        {
            var userOrderQuery = "SELECT o.*, a.*, p.* FROM [dbo].[Order] o JOIN Customer c ON o.CustomerId = c.Id JOIN Address a ON o.AddressId = a.Id JOIN Payment p ON o.PaymentId = p.Id WHERE c.UserId = @id";
            var orders = await _dbConnection.QueryAsync<DB.Order, DB.Address, DB.Payment, DB.Order>(
                userOrderQuery,
                (ord, addr, paym) =>
                {
                    ord.Address = addr;
                    ord.Payment = paym;

                    return ord;
                },
                new { id }
            );

            foreach (var order in orders)
            {
                var orderProductsQuery = "SELECT p.*, c.* FROM Product p JOIN Order_Product op ON op.ProductId = p.Id JOIN Category c ON p.CategoryId = c.Id WHERE op.OrderId = @id";
                var productsDB = await _dbConnection.QueryAsync<DB.Product, DB.Category, DB.Product>(
                    orderProductsQuery,
                    (prod, cat) =>
                    {
                        prod.Category = cat;
                        return prod;
                    },
                    new { id = order.Id }
                );
                order.Products = productsDB.ToList();

                var postCodeQuery = "SELECT * FROM PostCode pc WHERE pc.PostCode = @pc";
                var postCode = await _dbConnection.QuerySingleAsync<DB.PostCode_DB>(postCodeQuery, new { pc = order.Address.PostCode });
                order.Address.Post = postCode;
            }

            return orders.Select(x => new Order(x)).ToList();
        }
    }
}
