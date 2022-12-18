namespace VegeShama.Common.DatabaseModels.Relational
{
    public class Order_Product
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
    }
}
