using System.Linq;
using GymApp.Models;
using GymApp.Services.interfaces;

namespace GymApp.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository accountRepository;
        private ITransactionRepository transactionRepository;

        public AccountService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
        }
        public Account GetAccount(long id)
        {
            var account = accountRepository.GetAccount(id);
            if (account == null)
            {
                return null;
            }

            var transactions = transactionRepository.GetTransactions().Where(transaction => transaction.AccountId == account.Id).ToList();
            account.Balance = transactions.Sum(transaction => transaction.Amount);

            return account; 
        }

        public Account ReplenishmentBalance(int accountId, Transaction transaction)
        {
            transactionRepository.AddTransaction(accountId, transaction);
            return GetAccount(accountId);
        }
    }
}