namespace VegeShama.Common.DatabaseModels.Relational
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public Guid CustomerId { get; set; }
        public Guid PaymentId { get; set; }
        public Guid AddressId { get; set; }
    }
}
