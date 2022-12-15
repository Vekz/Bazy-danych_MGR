using EFCoreDal = VegeShama.Common.DatabaseModels.EFCore;
using MongoDBDal = VegeShama.Common.DatabaseModels.MongoDB;
using RelationalDal = VegeShama.Common.DatabaseModels.Relational;

namespace VegeShama.Common.DomainModels
{
    public class Order
    {
        public Guid Id { get; }
        public DateOnly DueDate { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public Address Address { get; set; }
        public Payment Payment { get; set; }
        public List<Product> Products { get; set; }

        public Order(MongoDBDal.Order order)
        {
            Id = order.Id;
            DueDate = DateOnly.FromDateTime(order.DueDate);
            DeliveryDate = DateOnly.FromDateTime(order.DeliveryDate.Date);
            Address = new Address(order.Address);
            Payment = new Payment(order.Payment);
            Products = order.Products.Select(x => new Product(x)).ToList();
        }

        public Order(EFCoreDal.Order order)
        {
            Id = order.Id;
            DueDate = DateOnly.FromDateTime(order.DueDate);
            DeliveryDate = DateOnly.FromDateTime(order.DeliveryDate.Date);
            Address = new Address(order.Address);
            Payment = new Payment(order.Payment);
            Products = order.Products.Select(x => new Product(x)).ToList();
        }

        public Order(RelationalDal.Order order, RelationalDal.Address address, RelationalDal.PostCode postCode, RelationalDal.Payment payment, List<Product> products)
        {
            Id = order.Id;
            DueDate = DateOnly.FromDateTime(order.DueDate);
            DeliveryDate = DateOnly.FromDateTime(order.DeliveryDate.Date);
            Address = new Address(address, postCode);
            Payment = new Payment(payment);
            Products = products;
        }
    }
}
