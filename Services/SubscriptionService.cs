using System;
using System.Collections.Generic;
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
        private IServiceRepository serviceRepository;
        private ITransactionRepository transactionRepository;
        
        public SubscriptionService(ISubscriptionRepository subscriptionRepository,
                                   IAccountRepository accountRepository,
                                   ITransactionRepository transactionRepository,
                                   IServiceRepository serviceRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
            this.serviceRepository = serviceRepository;
        }

        public Subscription AddSubscription(Subscription subscription)
        {
            var sub = subscriptionRepository.AddSubscription(subscription);
            sub.Account = accountRepository.CreateAccount(sub.Id);
            sub.Services = CheckSubscription(sub).ToList();

            return sub;
        }

        private IEnumerable<Service> CheckSubscription(Subscription subscription)
        {
            List<Service> services = new List<Service>();
            if(subscription.Type != TypeSubscription.NONE)
            {
               Service service = serviceRepository.GetServiceByName("GYM");
               service.Price = 0;
               services.Add(service);
            }
            if (subscription.Type == TypeSubscription.CLASSIC_PLUS)
            {
                Service service = serviceRepository.GetServiceByName("SWIMMING_POOL");
                service.Price = 0;
                services.Add(service);
            }
            if(subscription.Type == TypeSubscription.PREMIUM)
            {
                Service service1 = serviceRepository.GetServiceByName("SWIMMING_POOL");
                service1.Price = 0;
                Service service2 = serviceRepository.GetServiceByName("SPA");
                service2.Price = 0;
                services.Add(service1);
                services.Add(service2);
            }

            return services;
        }

        public Subscription GetSubscription(long id)
        {
            var subscription = subscriptionRepository.GetSubscription(id);
            subscription.Account = accountRepository.GetAccountBySubscriptionId(id);
            subscription.Account.Amount = transactionRepository.GetTransactions()
                                                               .Where(transaction => transaction.Account_Id == subscription.Account.Id)
                                                               .Sum(transaction => transaction.Amount);
            subscription.Services.AddRange(CheckSubscription(subscription));

            return subscription;
        }

        public List<Subscription> GetSubscriptions()
        {
            return subscriptionRepository.GetSubscriptions();
        }
    }
}