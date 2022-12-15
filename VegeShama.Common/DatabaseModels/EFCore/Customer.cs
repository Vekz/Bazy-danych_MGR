namespace VegeShama.Common.DatabaseModels.EFCore
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string VAT_number { get; set; }

        public List<Order> Orders { get; set; }
        public User User { get; set; }
    }
}
