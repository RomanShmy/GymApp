using Dapper;
using Dapper.Contrib.Extensions;
using GymApp.Models;
using Npgsql;

namespace GymApp
{
    public  class AccountRepository : IAccountRepository
    {
        private readonly ConnectionString connectionString;

        public AccountRepository(ConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public Account GetAccount(long id)
        {
            string query = "select * from public.account where id = @Id;";
            using (var connection = new NpgsqlConnection(connectionString.Value))
            {
                var account = connection.QueryFirst<Account>(query, new {Id = id});
                return account;
            }
        }
    }
}