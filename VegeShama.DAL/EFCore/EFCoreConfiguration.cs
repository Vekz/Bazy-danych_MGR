using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VegeShama.DAL.EFCore
{
    public static class EFCoreConfiguration
    {
        public static IServiceCollection ConfigureEFCoreContext(this IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
            var connStr = config.GetConnectionString("EFCore");

            services.AddDbContext<EFVegeContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connStr));

            return services;
        }
    }
}
