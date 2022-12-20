using Dapper;
using System.Data;
using System.Text;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Infrastructure.Repositories.Interfaces;
using DB = VegeShama.Common.DatabaseModels.Relational;

namespace VegeShama.Infrastructure.Repositories.Relational
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IOrdersRepository _ordersRepository;

        public UsersRepository(IDbConnection dbConnection, IOrdersRepository ordersRepository)
        {
            _dbConnection = dbConnection;
            _ordersRepository = ordersRepository;
        }

        public async Task<User> AddUser(RegisterUserModel model)
        {
            var user = new DB.User()
            {
                Login = model.Login,
                Password = model.Password,
                Email = model.Email,
                Type = 0,
                Customer = new DB.Customer()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    VAT_number = model.VAT_number,
                    Orders = new List<DB.Order>()
                }
            };

            var insertUserQuery = "INSERT INTO [dbo].[User] (Login, Password, Email, Type) VALUES (@Login, @Password, @Email, @Type)";
            await _dbConnection.ExecuteAsync(insertUserQuery, user);
            var userId = await _dbConnection.QueryAsync<Guid>("SELECT Id FROM [dbo].[User] u WHERE u.Login = @Login AND u.Password = @Password AND u.Email = @Email AND Type = 0", user);

            user.Customer.UserId = userId.First();
            user.Id = userId.First();

            var insertCustomerQuery = "INSERT INTO Customer (Name, Surname, VAT_number, UserId) VALUES (@Name, @Surname, @VAT_number, @UserId)";
            await _dbConnection.ExecuteAsync(insertCustomerQuery, user.Customer);
            var customerWithId = await _dbConnection.QueryAsync<Guid>("SELECT Id FROM Customer c WHERE c.UserId = @id", new { user.Id });
            user.Customer.Id = customerWithId.First();

            return new User(user);
        }

        public async Task DeleteUser(Guid id)
        {
            var sql = "DELETE FROM Customer c WHERE c.UserId = @id";
            await _dbConnection.ExecuteAsync(sql, new { id });
            sql = "DELETE FROM User u WHERE u.Id = @id";
            await _dbConnection.ExecuteAsync(sql, new { id });
        }

        public async Task<User> GetUserById(Guid id)
        {
            var userWithCustomer = await _dbConnection.QueryAsync<DB.User, DB.Customer, DB.User>(
                    "SELECT u.*, c.* FROM [dbo].[User] u JOIN Customer c ON c.UserId = u.Id WHERE u.Id = @id",
                    (user, customer) =>
                    {
                        user.Customer = customer;

                        return user;
                    },
                    new { id }
                );
            var user = userWithCustomer.FirstOrDefault();
            if (user is null)
                return null;

            var userOrders = await _ordersRepository.GetOrdersForUser(id);

            return new User(user, userOrders);
        }

        public async Task<Guid?> GetUserIdByCredentials(LoginModel model)
        {
            var userWithCustomer = await _dbConnection.QueryAsync<DB.User, DB.Customer, DB.User>(
                    "SELECT u.*, c.* FROM [dbo].[User] u JOIN Customer c ON c.UserId = u.Id WHERE u.Login = @login AND u.Password = @password",
                    (user, customer) =>
                    {
                        user.Customer = customer;

                        return user;
                    },
                    new { password = model.Password, login = model.Login }
                );
            var user = userWithCustomer.FirstOrDefault();
            if (user is null)
                return null;

            return user.Id;
        }

        public async Task<User> UpdateUser(Guid id, UpdateUserModel model)
        {
            if (model.Login != null || model.Email != null || model.Password != null)
            {
                StringBuilder sqlSB = new StringBuilder("UPDATE [dbo].[User] SET ");
                List<string> updateParts = new List<string>();

                if (model.Login != null)
                    updateParts.Add($"Login = '{model.Login}'");
                if (model.Email != null)
                    updateParts.Add($"Email = '{model.Email}'");
                if (model.Password != null)
                    updateParts.Add($"Password = '{model.Password}'");

                var joinedUpdates = string.Join(",", updateParts);
                sqlSB.Append(joinedUpdates);
                sqlSB.Append(" WHERE Id = @id");

                await _dbConnection.ExecuteAsync(sqlSB.ToString(), new { id });
            }

            if (model.Name != null || model.Surname != null || model.VAT_number != null)
            {
                StringBuilder sqlSB = new StringBuilder("UPDATE Customer SET ");
                List<string> updateParts = new List<string>();

                if (model.Name != null)
                    updateParts.Add($"Name = '{model.Name}'");
                if (model.Surname != null)
                    updateParts.Add($"Surname = '{model.Surname}'");
                if (model.VAT_number != null)
                    updateParts.Add($"VAT_number = '{model.VAT_number}'");

                var joinedUpdates = string.Join(",", updateParts);
                sqlSB.Append(joinedUpdates);
                sqlSB.Append(" WHERE UserId = @id");

                await _dbConnection.ExecuteAsync(sqlSB.ToString(), new { id });
            }

            return await GetUserById(id);
        }
    }
}
