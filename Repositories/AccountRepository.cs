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

        public Account GetAccount()
        {
            string query = "select * from public.account where id = 1;";
            using (var connection = new NpgsqlConnection(connectionString.Value))
            {
                var account = connection.QueryFirst<Account>(query);
                return account;
            }
        }
    }
}