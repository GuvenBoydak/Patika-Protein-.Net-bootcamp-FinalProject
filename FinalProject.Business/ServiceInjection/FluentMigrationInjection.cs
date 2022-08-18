using FinalProject.DataAccess;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace FinalProject.Business
{
    public static class FluentMigrationInjection
    {
        public static IServiceCollection FluentMigrationInjectionService(this IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();
            IConfiguration configuration = provider.GetService<IConfiguration>();

            services.AddFluentMigratorCore()
            // Configure the runner
            .ConfigureRunner(
            buil => buil
            // Use PosgreSql
            .AddPostgres11_0()
            // connection string
            .WithGlobalConnectionString(configuration.GetConnectionString("PosgreSql"))
            // Specify the assembly with the migrations
            .WithMigrationsIn(typeof(InitialMigration).Assembly));

            return services;
        }


        public static WebApplication FluentMigrationAplicationBuilder(this WebApplication app)
        {
            try
            {
                using (IServiceScope scope = app.Services.CreateScope())
                {
                    //Instantiate the runner
                    IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                    //Execute the migrations
                    runner.MigrateUp();
                    Console.WriteLine("Migration has successfully executed.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return app;
        }
    }


    
}
