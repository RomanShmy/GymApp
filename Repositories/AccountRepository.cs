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
            using (var connection = new NpgsqlConnection(connectionString.Value))
            {
                var account = connection.Get<Account>(1);
                return account;
            }
        }
    }
}