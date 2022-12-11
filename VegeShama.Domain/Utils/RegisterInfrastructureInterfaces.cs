using Microsoft.Extensions.DependencyInjection;
using VegeShama.Domain.Enums;

namespace VegeShama.Infrastructure.Utils
{
    public static class RegisterDomainInterfaces
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            //Register domain services
            return services;
        }

        public static IServiceCollection AddDatabaseProvider(this IServiceCollection services, DatabaseProviderEnum databaseProvider) => databaseProvider switch
        {
            DatabaseProviderEnum.MongoDB => services.AddMongoDBProvider(),
            DatabaseProviderEnum.EFCore => services.AddEFCoreProvider(),
            DatabaseProviderEnum.Relational => services.AddRelationalProvider(),
            _ => throw new InvalidOperationException("Tried to initialise application with uknown database type")
        };
    }
}
