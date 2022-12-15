using Microsoft.Extensions.DependencyInjection;
using VegeShama.Common.Enums;
using VegeShama.Domain.Services;
using VegeShama.Domain.Services.Interfaces;
using VegeShama.Infrastructure.Utils;

namespace VegeShama.Domain.Utils
{
    public static class RegisterDomainInterfaces
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            //Register domain services
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IUsersService, UsersService>();

            return services;
        }

        public static IServiceCollection AddDatabaseProvider(this IServiceCollection services, DatabaseProviderEnum databaseProvider) => databaseProvider switch
        {
            DatabaseProviderEnum.RavenDB => services.AddRavenDBProvider(),
            DatabaseProviderEnum.EFCore => services.AddEFCoreProvider(),
            DatabaseProviderEnum.Relational => services.AddRelationalProvider(),
            _ => throw new InvalidOperationException("Tried to initialise application with uknown database type")
        };

    }
}
