namespace VegeShama.Common.DatabaseModels.Relational
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string VAT_number { get; set; }

        public Guid UserId { get; set; }
        public List<Order> Orders { get; set; }
    }
}
