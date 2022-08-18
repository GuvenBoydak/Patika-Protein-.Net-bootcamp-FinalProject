using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject.Business
{
    public static class HangfireInjection
    {
        public static IServiceCollection HangfireServiceInjection(this IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();
            IConfiguration _configuration = provider.GetService<IConfiguration>();

            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(_configuration.GetSection("HangfirePostreSql").Value, new PostgreSqlStorageOptions
            {
                TransactionSynchronisationTimeout = TimeSpan.FromMinutes(5),
                InvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.FromMinutes(5),
            }));

            services.AddHangfireServer();
            return services;
        }
    }
}
