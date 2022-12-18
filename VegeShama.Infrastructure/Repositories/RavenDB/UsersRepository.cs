using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Infrastructure.Repositories.Interfaces;
using DB = VegeShama.Common.DatabaseModels.RavenDB;

namespace VegeShama.Infrastructure.Repositories.RavenDB
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IAsyncDocumentSession _docSession;

        public UsersRepository(IAsyncDocumentSession docSession)
        {
            _docSession = docSession;
        }

        public async Task<User> AddUser(RegisterUserModel model)
        {
            var user = new DB.User()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                VAT_number = model.VAT_number,
                Login = model.Login,
                Password = model.Password,
                Type = 0,
                Orders = new List<DB.Order>(),
                Id = String.Empty,
            };

            await _docSession.StoreAsync(user);
            await _docSession.SaveChangesAsync();
            return new User(user);
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _docSession.LoadAsync<DB.User>(id.ToString());
            _docSession.Delete(user);
            await _docSession.SaveChangesAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _docSession.LoadAsync<DB.User>(id.ToString());
            return new User(user);
        }

        public async Task<Guid?> GetUserIdByCredentials(LoginModel model)
            => Guid.Parse(await _docSession.Query<DB.User>().Where(x => x.Login == model.Login && x.Password == model.Password).Select(x => x.Id).FirstAsync());

        public async Task<User> UpdateUser(Guid id, UpdateUserModel model)
        {
            var user = await _docSession.LoadAsync<DB.User>(id.ToString());
            if (user is null)
                return null;

            if (model.Email != null)
                user.Email = model.Email;
            if (model.Login != null)
                user.Login = model.Login;
            if (model.Password != null)
                user.Password = model.Password;
            if (model.Name != null)
                user.Name = model.Name;
            if (model.Surname != null)
                user.Surname = model.Surname;
            if (model.VAT_number != null)
                user.VAT_number = model.VAT_number;

            await _docSession.SaveChangesAsync();
            return new User(user);
        }
    }
}
