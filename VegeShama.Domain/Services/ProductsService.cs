using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Domain.Services.Interfaces;
using VegeShama.Infrastructure.Repositories.Interfaces;

namespace VegeShama.Domain.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository ordersRepository)
        {
            _productsRepository = ordersRepository;
        }

        public Task<Product> AddProduct(AddProductModel model)
            => _productsRepository.AddProduct(model);

        public Task DeleteProduct(Guid id)
            => _productsRepository.DeleteProduct(id);

        public Task<List<Product>> GetAll()
            => _productsRepository.GetAll();

        public Task<Product> GetProduct(Guid id)
            => _productsRepository.GetProductById(id);

        public Task<Product> UpdateProduct(Guid id, UpdateProductModel model)
            => _productsRepository.UpdateProduct(id, model);
    }
}
