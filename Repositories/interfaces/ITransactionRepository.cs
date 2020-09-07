
using System.Collections.Generic;
using GymApp.Models;

namespace GymApp
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactions();
        Transaction AddTransaction(long accountId, Transaction transaction);
    }
}