using VegeShama.Common.APIModels;
using VegeShama.Domain.Services.Interfaces;
using VegeShama.Infrastructure.Repositories.Interfaces;

namespace VegeShama.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _usersRepository;

        public AuthService(IUsersRepository userRepository)
        {
            _usersRepository = userRepository;
        }

        public async Task<Guid?> Login(LoginModel model)
            => await _usersRepository.GetUserIdByCredentials(model);
    }
}
