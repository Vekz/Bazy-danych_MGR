namespace VegeShama.Common.DatabaseModels.EFCore
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
        public Address Address { get; set; }
        public List<Product> Products { get; set; }
    }
}
