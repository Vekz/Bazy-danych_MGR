using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;

namespace VegeShama.Domain.Services.Interfaces
{
    public interface IUsersService
    {
        public async Task<User> GetUser(Guid id);
        public async Task<User> RegisterUser(RegisterUserModel model);
        public async Task<User> UpdateUser(Guid id, UpdateUserModel model);
        public async Task DeleteUser(Guid id);
    }
}
