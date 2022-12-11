using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;

namespace VegeShama.Domain.Services.Interfaces
{
    public interface IProductsService
    {
        public Task<List<Product>> GetAll();
        public Task<Product> GetProduct(Guid id);
        public Task<Product> AddProduct(AddProductModel model);
        public Task<Product> UpdateProduct(Guid id, UpdateProductModel model);
        public Task DeleteProduct(Guid id);
    }
}
