using Dapper;
using Dapper.Contrib.Extensions;
using GymApp.Models;
using Npgsql;

namespace GymApp
{
    public  class AccountRepository : IAccountRepository
    {
        private readonly DataFactory db;

        public AccountRepository(DataFactory db)
        {
            this.db = db;
        }

        public Account GetAccount(long id)
        {
            string query = "select * from public.account where id = @Id;";
            using (var connection = db.GetConnection())
            {
                var account = connection.QueryFirst<Account>(query, new {Id = id});
                return account;
            }
        }
    }
}