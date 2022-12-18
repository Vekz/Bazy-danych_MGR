namespace VegeShama.Common.DatabaseModels.Relational
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
