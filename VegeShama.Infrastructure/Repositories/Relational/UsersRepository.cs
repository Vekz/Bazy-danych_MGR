using System.Data;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Infrastructure.Repositories.Interfaces;

namespace VegeShama.Infrastructure.Repositories.Relational
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbConnection _dbConnection;

        public UsersRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<User> AddUser(RegisterUserModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid?> GetUserIdByCredentials(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(Guid id, UpdateUserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
