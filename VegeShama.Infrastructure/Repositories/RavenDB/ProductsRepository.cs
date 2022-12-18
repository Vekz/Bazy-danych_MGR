using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Infrastructure.Repositories.Interfaces;
using DB = VegeShama.Common.DatabaseModels.RavenDB;


namespace VegeShama.Infrastructure.Repositories.RavenDB
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IAsyncDocumentSession _docSession;

        public ProductsRepository(IAsyncDocumentSession docSession)
        {
            _docSession = docSession;
        }

        public async Task<Product> AddProduct(AddProductModel model)
        {
            var product = new DB.Product()
            {
                Name = model.Name,
                Price = model.Price,
                Category = model.Category,
                Id = String.Empty,
            };

            await _docSession.StoreAsync(product);
            await _docSession.SaveChangesAsync();
            return new Product(product);
        }

        public async Task DeleteProduct(Guid id)
        {
            var product = await _docSession.LoadAsync<DB.Product>(id.ToString());
            _docSession.Delete(product);
            await _docSession.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _docSession.Query<DB.Product>().ToListAsync();
            return products.Select(x => new Product(x)).ToList();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var product = await _docSession.LoadAsync<DB.Product>(id.ToString());
            return new Product(product);
        }

        public async Task<Product> UpdateProduct(Guid id, UpdateProductModel model)
        {
            var product = await _docSession.LoadAsync<DB.Product>(id.ToString());
            if (product is null)
                return null;

            if (model.Name != null)
                product.Name = model.Name;
            if (model.Price.HasValue)
                product.Price = model.Price.Value;
            if (model.Category != null)
                product.Category = model.Category;

            await _docSession.SaveChangesAsync();
            return new Product(product);
        }
    }
}
