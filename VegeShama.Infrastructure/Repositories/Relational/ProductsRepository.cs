using System.Data;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Infrastructure.Repositories.Interfaces;

namespace VegeShama.Infrastructure.Repositories.Relational
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<Product> AddProduct(AddProductModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProduct(Guid id, UpdateProductModel model)
        {
            throw new NotImplementedException();
        }
    }
}
