using System;
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

        public Transaction AddTransaction(long accountId, Transaction transaction)
        {
            string query = "insert into public.transactions (amount, descriptions, date, accountId) values(@Amount, @Descriptions, @Date, @AccountId) return id;";
            using (var connection = new NpgsqlConnection(connectionString.Value))
            {
                var id = connection.QueryFirst<int>(query, new {Amount = transaction.Amount, 
                                                                                  Description = transaction.Description,
                                                                                  Date = DateTime.Now,
                                                                                  AccountId = accountId});
                transaction.Id = id;
                return transaction;
            }
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