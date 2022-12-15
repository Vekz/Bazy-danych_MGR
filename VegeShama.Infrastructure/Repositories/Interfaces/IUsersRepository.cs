using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;

namespace VegeShama.Infrastructure.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        public Task<Guid?> GetUserIdByCredentials(LoginModel model);
        public Task DeleteUser(Guid id);
        public Task<User> GetUserById(Guid id);
        public Task<User> AddUser(RegisterUserModel model);
        public Task<User> UpdateUser(Guid id, UpdateUserModel model);
    }
}
