using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using GymApp.Models;
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
            string query = "select * from public.transactions;";
            using(var connection = new NpgsqlConnection(connectionString.Value))
            {
                var transactions = connection.Query<Transaction>(query).ToList();
                return transactions;
            }
        }
    }
}