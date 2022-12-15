using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Domain.Services.Interfaces;
using VegeShama.Infrastructure.Repositories.Interfaces;

namespace VegeShama.Domain.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task DeleteUser(Guid id)
            => _usersRepository.DeleteUser(id);

        public Task<User> GetUser(Guid id)
            => _usersRepository.GetUserById(id);

        public Task<User> RegisterUser(RegisterUserModel model)
            => _usersRepository.AddUser(model);

        public Task<User> UpdateUser(Guid id, UpdateUserModel model)
            => _usersRepository.UpdateUser(id, model);
    }
}
