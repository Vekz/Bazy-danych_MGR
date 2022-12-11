using VegeShama.Common.Enums;

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

        public User(MongoDB.User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            VAT_number = user.VAT_number;
            Type = (UserType)user.Type;
            Orders = user.Orders.Select(x => new Order(x)).ToList();
        }

        public User(EFCore.User user, List<EFCore.Order> orders)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            VAT_number = user.VAT_number;
            Type = (UserType)user.Type;
            Orders = user.orders.Select(x => new Order(x)).ToList();
        }

        public User(Relational.User user, List<Order> orders)
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
