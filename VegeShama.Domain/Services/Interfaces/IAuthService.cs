using VegeShama.Common.APIModels;

namespace VegeShama.Domain.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> Login(LoginModel model);
    }
}
