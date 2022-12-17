using Microsoft.Extensions.DependencyInjection;
using VegeShama.DAL.EFCore;
using VegeShama.DAL.RavenDB;
using VegeShama.DAL.Relational;

namespace VegeShama.Infrastructure.Utils
{
    public static class RegisterInfrastructureInterfaces
    {
        public static IServiceCollection AddRavenDBProvider(this IServiceCollection services)
        {
            services.ConfigureRavenDBContext();
            //Register RavenDB repositories


            return services;
        }

        public static IServiceCollection AddEFCoreProvider(this IServiceCollection services)
        {
            services.ConfigureEFCoreContext();
            //Register relational-object repositories


            return services;
        }

        public static IServiceCollection AddRelationalProvider(this IServiceCollection services)
        {
            services.ConfigureRelationalConnection();
            //Register SQL Relational repositories


            return services;
        }
    }
}
