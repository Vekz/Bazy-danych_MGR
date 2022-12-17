using Microsoft.Extensions.DependencyInjection;
using Raven.DependencyInjection;

namespace VegeShama.DAL.RavenDB
{
    public static class RavenDBConfiguration
    {
        public static IServiceCollection ConfigureRavenDBContext(this IServiceCollection services)
        {
            services.AddRavenDbDocStore();
            services.AddRavenDbAsyncSession();

            return services;
        }
    }
}
