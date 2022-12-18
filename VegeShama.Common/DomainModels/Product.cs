using EFCoreDal = VegeShama.Common.DatabaseModels.EFCore;
using RavenDBDal = VegeShama.Common.DatabaseModels.RavenDB;
using RelationalDal = VegeShama.Common.DatabaseModels.Relational;

namespace VegeShama.Common.DomainModels
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }

        public Product(RavenDBDal.Product product)
        {
            Id = Guid.Parse(product.Id);
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

        public Product(RelationalDal.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Category = product.Category.Name;
        }
    }
}
