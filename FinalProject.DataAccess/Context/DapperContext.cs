using Microsoft.Extensions.Configuration;
using Npgsql;

namespace FinalProject.DataAccess
{
    public class DapperContext : IDapperContext
    {
        IConfiguration configuration;

        public DapperContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        //Execute metoduna parametre olarak Geriye deger döndürmeyen delegate olan Action a <NpgsqlConnection> tipindeki baglantı nesnesini veriyoruz.
        public void Execute(Action<NpgsqlConnection> @event)
        {
            using (NpgsqlConnection connection = GetConnection())
            {
                connection.Open();
                @event(connection);
            }
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(configuration.GetConnectionString("PosgreSql"));
        }
    }


}
