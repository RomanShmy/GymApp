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
            

            return sub;
        }

        private IEnumerable<Service> CheckSubscription(Subscription subscription)//брать не по Id
        {
            List<Service> services = new List<Service>();
            if((int)subscription.Type != 0)//enum
            {
               Service service = serviceRepository.GetService(1);
               service.Price = 0;
               services.Add(service);
            }
            if ((int)subscription.Type == 2)
            {
                Service service = serviceRepository.GetService(2);
                service.Price = 0;
                services.Add(service);
            }
            if((int)subscription.Type == 3)
            {
                Service service1 = serviceRepository.GetService(2);
                service1.Price = 0;
                Service service2 = serviceRepository.GetService(3);
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
    }
}