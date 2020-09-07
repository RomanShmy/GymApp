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
        private readonly DataFactory db;

        public TransactionRepository(DataFactory db)
        {
            this.db = db;
        }

        public Transaction AddTransaction(long accountId, Transaction transaction)
        {
            string query = "insert into public.transactions (amount, descriptions, date, account_id) values(@Amount, @Descriptions, @Date, @AccountId) returning id;";
            using (var connection = db.GetConnection())
            {
                var id = connection.QueryFirst<int>(query, new {Amount = transaction.Amount, 
                                                                Descriptions = transaction.Description,
                                                                Date = DateTime.Now,
                                                                AccountId = accountId});
                transaction.Id = id;
                return transaction;
            }
        }

        public List<Transaction> GetTransactions()
        {   
            string query = "select * from public.transactions;";
            using(var connection = db.GetConnection())
            {
                var transactions = connection.Query<Transaction>(query).ToList();
                return transactions;
            }
        }
    }
}