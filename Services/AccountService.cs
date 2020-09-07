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
            
            account.Amount = transactionRepository.GetTransactions().Where(transaction => transaction.Account_Id == account.Id).Sum(transaction => transaction.Amount);
            
            return account; 
        }

        public Account ReplenishmentBalance(long accountId, Transaction transaction)
        {
            transactionRepository.AddTransaction(accountId, transaction);
            return GetAccount(accountId);
        }
    }
}