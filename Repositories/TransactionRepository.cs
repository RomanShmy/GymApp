using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Dapper.Contrib.Extensions;
using Npgsql;

namespace GymApp
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ConnectionString connectionString;

        public TransactionRepository(ConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Transaction> GetTransactions()
        {
            using(var connection = new NpgsqlConnection(connectionString.Value))
            {
                var transactions = connection.GetAll<Transaction>().ToList();
                return transactions;
            }
        }
    }
}