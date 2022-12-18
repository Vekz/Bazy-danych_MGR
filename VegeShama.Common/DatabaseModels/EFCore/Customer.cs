namespace VegeShama.Common.DatabaseModels.EFCore
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string VAT_number { get; set; }

        public virtual List<Order> Orders { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
