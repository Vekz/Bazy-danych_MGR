namespace VegeShama.Common.DomainModels
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }

        public Product(MongoDB.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Category = product.Category;
        }

        public Product(EFCore.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Category = product.Category;
        }

        public Product(Relational.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Category = product.Category;
        }
    }
}
