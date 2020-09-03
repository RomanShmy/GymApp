using System.Data;
using Npgsql;

namespace GymApp
{
    public class DataFactory
    {
        private string connectionString;
        public  DataFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}