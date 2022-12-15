using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;

namespace VegeShama.Infrastructure.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        public Task<Product> AddProduct(AddProductModel model);
        public Task DeleteProduct(Guid id);
        public Task<List<Product>> GetAll();
        public Task<Product> GetProductById(Guid id);
        public Task<Product> UpdateProduct(Guid id, UpdateProductModel model);
    }
}
