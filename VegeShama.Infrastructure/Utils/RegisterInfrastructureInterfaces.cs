using Microsoft.Extensions.DependencyInjection;

namespace VegeShama.Infrastructure.Utils
{
    public static class RegisterInfrastructureInterfaces
    {
        public static IServiceCollection AddMongoDBProvider(this IServiceCollection services)
        {
            //Register MongoDB repositories
            return services;
        }

        public static IServiceCollection AddEFCoreProvider(this IServiceCollection services)
        {
            //Register relational-object repositories
            return services;
        }

        public static IServiceCollection AddRelationalProvider(this IServiceCollection services)
        {
            //Register SQL Relational repositories
            return services;
        }
    }
}
