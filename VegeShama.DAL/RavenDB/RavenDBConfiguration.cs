using Microsoft.Extensions.DependencyInjection;
using Raven.DependencyInjection;

namespace VegeShama.DAL.RavenDB
{
    public static class RavenDBConfiguration
    {
        public static IServiceCollection ConfigureRavenDBContext(this IServiceCollection services)
        {
            services.AddRavenDbDocStore(o =>
            {
                //Setup mapping names for collections
                o.BeforeInitializeDocStore = docStore =>
                {
                    docStore.Conventions.FindCollectionName = type => type.Name;
                };
            });
            services.AddRavenDbAsyncSession();
            services.AddRavenDbSession();

            return services;
        }
    }
}
