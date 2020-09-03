using GymApp.Models;
using GymApp.Repositories.interfaces;
using GymApp.Services.interfaces;

namespace GymApp.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private ISubscriptionRepository subscriptionRepository;
        private IAccountRepository accountRepository;
        
        public SubscriptionService(ISubscriptionRepository subscriptionRepository,
                                   IAccountRepository accountRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.accountRepository = accountRepository;
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

            return subscription;
        }
    }
}