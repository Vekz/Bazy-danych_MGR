namespace VegeShama.Common.DatabaseModels.EFCore
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Order_Product> OrderProducts { get; set; }
    }
}
