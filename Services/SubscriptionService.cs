using System.Linq;
using GymApp.Models;
using GymApp.Repositories.interfaces;
using GymApp.Services.interfaces;

namespace GymApp.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private ISubscriptionRepository subscriptionRepository;
        private IAccountRepository accountRepository;
        private ITransactionRepository transactionRepository;
        
        public SubscriptionService(ISubscriptionRepository subscriptionRepository,
                                   IAccountRepository accountRepository,
                                   ITransactionRepository transactionRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
        }

        public Subscription AddSubscription(Subscription subscription)
        {
            var sub = subscriptionRepository.AddSubscription(subscription);
            sub.Account = accountRepository.CreateAccount(sub.Id);

            return sub;
        }

        public Subscription GetSubscription(long id)
        {
            var subscription = subscriptionRepository.GetSubscription(id);
            subscription.Account = accountRepository.GetAccountBySubscriptionId(id);
            subscription.Account.Amount = transactionRepository.GetTransactions()
                                                               .Where(transaction => transaction.Account_Id == subscription.Account.Id)
                                                               .Sum(transaction => transaction.Amount);

            return subscription;
        }
    }
}