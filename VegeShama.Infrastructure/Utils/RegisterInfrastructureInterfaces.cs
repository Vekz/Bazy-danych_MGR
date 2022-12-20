using Microsoft.Extensions.DependencyInjection;
using VegeShama.DAL.EFCore;
using VegeShama.DAL.RavenDB;
using VegeShama.DAL.Relational;
using VegeShama.Infrastructure.Repositories.Interfaces;
using EFCore = VegeShama.Infrastructure.Repositories.EFCore;
using RavenDB = VegeShama.Infrastructure.Repositories.RavenDB;
using Relational = VegeShama.Infrastructure.Repositories.Relational;

namespace VegeShama.Infrastructure.Utils
{
    public static class RegisterInfrastructureInterfaces
    {
        public static IServiceCollection AddRavenDBProvider(this IServiceCollection services)
        {
            services.ConfigureRavenDBContext();

            //Register RavenDB repositories
            services.AddTransient<IOrdersRepository, RavenDB.OrdersRepository>();
            services.AddTransient<IProductsRepository, RavenDB.ProductsRepository>();
            services.AddTransient<IUsersRepository, RavenDB.UsersRepository>();
            services.AddTransient<ITestRepository, RavenDB.TestRepository>();

            return services;
        }

        public static IServiceCollection AddEFCoreProvider(this IServiceCollection services)
        {
            services.ConfigureEFCoreContext();

            //Register relational-object repositories
            services.AddTransient<IOrdersRepository, EFCore.OrdersRepository>();
            services.AddTransient<IProductsRepository, EFCore.ProductsRepository>();
            services.AddTransient<IUsersRepository, EFCore.UsersRepository>();
            services.AddTransient<ITestRepository, EFCore.TestRepository>();

            return services;
        }

        public static IServiceCollection AddRelationalProvider(this IServiceCollection services)
        {
            services.ConfigureRelationalConnection();

            //Register SQL Relational repositories
            services.AddTransient<IOrdersRepository, Relational.OrdersRepository>();
            services.AddTransient<IProductsRepository, Relational.ProductsRepository>();
            services.AddTransient<IUsersRepository, Relational.UsersRepository>();
            services.AddTransient<ITestRepository, Relational.TestRepository>();

            return services;
        }
    }
}
