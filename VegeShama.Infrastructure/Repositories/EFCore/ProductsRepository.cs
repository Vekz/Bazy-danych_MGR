using Microsoft.EntityFrameworkCore;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.DAL.EFCore;
using VegeShama.Infrastructure.Repositories.Interfaces;
using DB = VegeShama.Common.DatabaseModels.EFCore;

namespace VegeShama.Infrastructure.Repositories.EFCore
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly EFVegeContext _dbContext;

        public ProductsRepository(EFVegeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> AddProduct(AddProductModel model)
        {
            var product = new DB.Product()
            {
                Name = model.Name,
                Price = model.Price,
                Category = model.Category,
            };
            _dbContext.Product.Add(product);

            await _dbContext.SaveChangesAsync();
            return new Product(product);
        }

        public async Task DeleteProduct(Guid id)
        {
            var productToDelete = _dbContext.Product.FindAsync(id);

            _dbContext.Product.Remove(await productToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAll()
            => await _dbContext.Product.Select(x => new Product(x)).ToListAsync();

        public async Task<Product> GetProductById(Guid id)
        {
            var product = await _dbContext.Product.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (product is null)
                return null;
            return new Product(product);
        }

        public async Task<Product> UpdateProduct(Guid id, UpdateProductModel model)
        {
            var product = await _dbContext.Product.FindAsync(id);
            if (product is null)
                return null;

            if (model.Name != null)
                product.Name = model.Name;
            if (model.Price.HasValue)
                product.Price = model.Price.Value;
            if (model.Category != null)
                product.Category = model.Category;

            await _dbContext.SaveChangesAsync();
            return new Product(product);
        }
    }
}
