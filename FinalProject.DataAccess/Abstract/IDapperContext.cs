using Npgsql;

namespace FinalProject.DataAccess
{
    public interface IDapperContext
    {
        public NpgsqlConnection GetConnection();

        public void Execute(Action<NpgsqlConnection> @event);
    }
}
