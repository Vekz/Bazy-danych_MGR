using VegeShama.Common.Enums;
using EFCoreDal = VegeShama.Common.DatabaseModels.EFCore;
using RavenDBDal = VegeShama.Common.DatabaseModels.RavenDB;
using RelationalDal = VegeShama.Common.DatabaseModels.Relational;

namespace VegeShama.Common.DomainModels
{
    public class User
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string VAT_number { get; set; }
        public UserType Type { get; }
        public List<Order> Orders { get; set; }

        public User(RavenDBDal.User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            VAT_number = user.VAT_number;
            Type = (UserType)user.Type;
            Orders = user.Orders.Select(x => new Order(x)).ToList();
        }

        public User(EFCoreDal.User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            VAT_number = user.VAT_number;
            Type = (UserType)user.Type;
            Orders = user.Customer.Orders.Select(x => new Order(x)).ToList();
        }

        public User(RelationalDal.User user, List<Order> orders)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            VAT_number = user.VAT_number;
            Type = (UserType)user.Type;

            Orders = orders;
        }
    }
}
