using Raven.Client.Documents.Session;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Infrastructure.Repositories.Interfaces;

namespace VegeShama.Infrastructure.Repositories.RavenDB
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IAsyncDocumentSession _docSession;

        public UsersRepository(IAsyncDocumentSession docSession)
        {
            _docSession = docSession;
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
