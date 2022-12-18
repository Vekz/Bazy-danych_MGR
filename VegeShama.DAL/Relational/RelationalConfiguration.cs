using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace VegeShama.DAL.Relational
{
    public static class RelationalConfiguration
    {
        public static IServiceCollection ConfigureRelationalConnection(this IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
            var connStr = config.GetConnectionString("Relational");

            services.AddTransient<IDbConnection>((sp) => new SqlConnection(connStr));

            return services;
        }
    }
}
