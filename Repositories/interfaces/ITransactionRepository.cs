using System.Transactions;
using System.Collections.Generic;
namespace GymApp
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactions();
    }
}