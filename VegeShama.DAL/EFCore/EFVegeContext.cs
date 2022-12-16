using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegeShama.Common.DatabaseModels.EFCore;

namespace VegeShama.DAL.EFCore
{
    public class EFVegeContext : DbContext
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }

        public EFVegeContext(DbContextOptions<EFVegeContext> options)
        : base(options)
        {
        }
    }
}
