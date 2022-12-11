namespace VegeShama.Common.DomainModels
{
    public class Order
    {
        public Guid Id { get; }
        public DateOnly DueDate { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public User User { get; set; }
        public Address Address { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Product> Products { get; set; }

        public Order(MongoDB.Order order)
        {
            Id = order;
            DueDate = order.DueDate.Date;
            DeliveryDate = order.DeliveryDate.Date;
            User = order.User;
            Address = new Address(order.Address);
            Payments = order.Payments.Select(x => new Payment(x)).ToList();
            Products = order.Products.Select(x => new Product(x)).ToList();
        }

        public Order(EFCore.Order order)
        {
            Id = order;
            DueDate = order.DueDate.Date;
            DeliveryDate = order.DeliveryDate.Date;
            User = order.User;
            Address = new Address(order.Address);
            Payments = order.Payments.Select(x => new Payment(x)).ToList();
            Products = order.Products.Select(x => new Product(x)).ToList();
        }

        public Order(Relational.Order order, Relational.Address address, List<Relational.Payment> payments, List<Relational.Product> products)
        {
            Id = order;
            DueDate = order.DueDate.Date;
            DeliveryDate = order.DeliveryDate.Date;
            User = order.User;

            Address = new Address(address);
            Payments = payments.Select(x => new Payment(x)).ToList();
            Products = products.Select(x => new Product(x)).ToList();
        }
    }
}
