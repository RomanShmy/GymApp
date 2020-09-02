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
        public Account GetAccount()
        {
            var account = accountRepository.GetAccount();
            if (account == null)
            {
                return null;
            }
            account.Transactions.AddRange(transactionRepository.GetTransactions().Where(transaction => transaction.AccountId == account.Id));
            
            return account; 
        }
    }
}