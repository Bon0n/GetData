using GetData.Infra.Data.Context;
using GetData.Infra.Data.Repositories;
using GetDataInfo.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GetData.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            var connectionString = "Server=172.17.0.2;User Id=root;Password=R0omArmY;Database=DataInfo;Initial Catalog=DataInfo";
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<AppDbContext>(options => options
                .UseMySql(connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                );
            services.AddScoped<IDataInfoRepository, DataInfoRepository>();

            return services;
        }
    }
}