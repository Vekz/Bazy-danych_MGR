using VegeShama.Common.APIModels;

namespace VegeShama.Domain.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<Guid?> Login(LoginModel model);
    }
}
