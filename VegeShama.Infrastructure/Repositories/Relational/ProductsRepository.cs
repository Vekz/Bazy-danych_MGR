using Dapper;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Infrastructure.Repositories.Interfaces;
using DB = VegeShama.Common.DatabaseModels.Relational;

namespace VegeShama.Infrastructure.Repositories.Relational
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Product> AddProduct(AddProductModel model)
        {
            var sqlCat = "SELECT * FROM Category c WHERE c.Name = @name";
            var category = _dbConnection.Query<DB.Category>(sqlCat, new { name = model.Category });
            if (category.IsNullOrEmpty())
            {
                var categoryCreation = "INSERT INTO Category (Name) VALUES (@name)";
                _dbConnection.Execute(categoryCreation, new { name = model.Category });
                category = _dbConnection.Query<DB.Category>(sqlCat, new { name = model.Category });
            }

            var product = new DB.Product()
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = category.First().Id
            };

            var sqlInsertProduct = "INSERT INTO Product (Name, Price, CategoryId) VALUES (@Name, @Price, @CategoryId)";
            _dbConnection.Execute(sqlInsertProduct, product);

            var sql = "SELECT p.*, c.* FROM Product p JOIN Category c ON c.Id = p.CategoryId WHERE p.Name = @name AND p.Price = @price";

            var products = await _dbConnection.QueryAsync<DB.Product, DB.Category, DB.Product>(
                sql,
                (prod, cat) =>
                {
                    prod.Category = cat;
                    return prod;
                },
                new { name = model.Name, price = model.Price }
            );
            var productRet = products.FirstOrDefault();
            return new Product(productRet);
        }

        public async Task DeleteProduct(Guid id)
        {
            var sql = "DELETE FROM Product p WHERE p.Id = @id";
            await _dbConnection.ExecuteAsync(sql, new { id });
        }

        public async Task<List<Product>> GetAll()
        {
            var sql = "SELECT p.*, c.* FROM Product p JOIN Category c ON c.Id = p.CategoryId";

            var products = await _dbConnection.QueryAsync<DB.Product, DB.Category, DB.Product>(sql, (prod, cat) =>
            {
                prod.Category = cat;
                return prod;
            });

            return products?.Select(x => new Product(x)).ToList() ?? new List<Product>();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var sql = "SELECT p.*, c.* FROM Product p JOIN Category c ON c.Id = p.CategoryId WHERE p.Id = @id";

            var products = await _dbConnection.QueryAsync<DB.Product, DB.Category, DB.Product>(
                sql,
                (prod, cat) =>
                {
                    prod.Category = cat;
                    return prod;
                },
                new { id }
            );
            var product = products.FirstOrDefault();
            if (product is null)
                return null;

            return new Product(product);
        }

        public async Task<Product> UpdateProduct(Guid id, UpdateProductModel model)
        {
            if (model.Name != null || model.Price != null)
            {
                StringBuilder sqlSB = new StringBuilder("UPDATE Product SET ");
                List<string> updateParts = new List<string>();

                if (model.Name != null)
                    updateParts.Add($"Name = '{model.Name}'");
                if (model.Price != null)
                    updateParts.Add($"Price = {model.Price}");

                var joinedUpdates = string.Join(",", updateParts);
                sqlSB.Append(joinedUpdates);
                sqlSB.Append(" WHERE id = @id");

                await _dbConnection.ExecuteAsync(sqlSB.ToString(), new { id });
            }

            if (model.Category != null)
            {
                var sqlUpdateCategory = "UPDATE c SET Name = @name FROM Category c JOIN Product p ON p.CategoryId = c.Id WHERE p.Id = @id";
                await _dbConnection.ExecuteAsync(sqlUpdateCategory, new { id, name = model.Category });
            }

            return await GetProductById(id);
        }
    }
}
