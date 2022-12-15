using EFCoreDal = VegeShama.Common.DatabaseModels.EFCore;
using MongoDBDal = VegeShama.Common.DatabaseModels.MongoDB;
using RelationalDal = VegeShama.Common.DatabaseModels.Relational;

namespace VegeShama.Common.DomainModels
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }

        public Product(MongoDBDal.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Category = product.Category;
        }

        public Product(EFCoreDal.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Category = product.Category;
        }

        public Product(RelationalDal.Product product, RelationalDal.Category category)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Category = category.Name;
        }
    }
}
