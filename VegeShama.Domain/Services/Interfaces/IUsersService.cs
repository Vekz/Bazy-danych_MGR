using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;

namespace VegeShama.Domain.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<User> GetUser(Guid id);
        public Task<User> RegisterUser(RegisterUserModel model);
        public Task<User> UpdateUser(Guid id, UpdateUserModel model);
        public Task DeleteUser(Guid id);
    }
}
