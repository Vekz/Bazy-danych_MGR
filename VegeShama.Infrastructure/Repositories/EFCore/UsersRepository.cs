using Microsoft.EntityFrameworkCore;
using VegeShama.Common.APIModels;
using VegeShama.Common.DomainModels;
using VegeShama.Common.Enums;
using VegeShama.DAL.EFCore;
using VegeShama.Infrastructure.Repositories.Interfaces;
using DB = VegeShama.Common.DatabaseModels.EFCore;

namespace VegeShama.Infrastructure.Repositories.EFCore
{
    public class UsersRepository : IUsersRepository
    {
        private readonly EFVegeContext _dbContext;

        public UsersRepository(EFVegeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUser(RegisterUserModel model)
        {
            var user = new DB.User()
            {
                Login = model.Login,
                Password = model.Password,
                Email = model.Email,
                Type = (int)UserType.Customer
            };

            var customer = new DB.Customer()
            {
                Name = model.Name,
                Surname = model.Surname,
                VAT_number = model.VAT_number,
                Orders = new List<DB.Order>()
            };
            user.Customer = customer;

            _dbContext.Add(user);
            _dbContext.Add(customer);
            await _dbContext.SaveChangesAsync();
            return new User(user);
        }

        public async Task DeleteUser(Guid id)
        {
            var userToDelete = _dbContext.User.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.User.Remove(await userToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _dbContext.User.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user is null)
                return null;
            return new User(user);
        }

        public async Task<Guid?> GetUserIdByCredentials(LoginModel model)
            => (await _dbContext.User.Where(x => x.Login == model.Login && x.Password == model.Password).FirstOrDefaultAsync())?.Id;

        public async Task<User> UpdateUser(Guid id, UpdateUserModel model)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
                return null;

            if (model.Email != null)
                user.Email = model.Email;
            if (model.Login != null)
                user.Login = model.Login;
            if (model.Password != null)
                user.Password = model.Password;
            if (model.Name != null)
                user.Customer.Name = model.Name;
            if (model.Surname != null)
                user.Customer.Surname = model.Surname;
            if (model.VAT_number != null)
                user.Customer.VAT_number = model.VAT_number;

            await _dbContext.SaveChangesAsync();
            return new User(user);
        }
    }
}
