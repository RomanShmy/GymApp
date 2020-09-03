
using System.Collections.Generic;
using GymApp.Models;

namespace GymApp
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactions();
        Transaction AddTransactionReplenish(long accountId, Transaction transaction);
        Transaction AddTransactionWithdrawal(long accountId, Transaction transaction);
    }
}