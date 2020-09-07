using System.Linq;
using System.Collections.Generic;
using System;
using GymApp.Models;
using GymApp.Repositories.interfaces;
using GymApp.Services.interfaces;

namespace GymApp.Services
{
    public class CheckAccessService : ICheckAccessService
    {
        private ICheckAccessRepository checkRepository;
        private ISubscriptionService subscriptionService;
        private ITransactionRepository transactionRepository;
        private IServiceRepository serviceRepository;
        const string GYM = "GYM";
        const string SWIMMING_POOL = "SWIMMING_POOL";
        const string SPA = "SPA";

        public CheckAccessService(ICheckAccessRepository checkRepository, 
                                  ISubscriptionService subscriptionService, 
                                  ITransactionRepository transactionRepository,
                                  IServiceRepository serviceRepository)
        {
            this.checkRepository = checkRepository;
            this.subscriptionService = subscriptionService;
            this.transactionRepository = transactionRepository;
            this.serviceRepository = serviceRepository;
        }

        public ResultHistory PostResult(long subscriptionId)
        {
            var subscription = subscriptionService.GetSubscription(subscriptionId);
            ResultHistory result = new ResultHistory();
            result.Subscription = subscription;
            result.Access = Access.NoAccess;
            result.Message = "No access, ";
            if (IsAlowedAcccess(subscription))
            {
                result.Access = Access.OK;
                result.Message = "Access is allowed";
            }
            else
            {
                if(!IsPositiveBalance(subscription))
                {
                    result.Message += "because balance < 0"; 
                }
                if(!IsExpirateSubscription(subscription))
                {
                    result.Message += "because subscription is expirate";
                }
            }

            checkRepository.AddResult(result);

            return result;
        }

        public ResultHistory PostResultService(long subscriptionId, string serviceName)
        {
            var subscription = subscriptionService.GetSubscription(subscriptionId);
            if (!IsAlowedAcccess(subscription))
            {
                return PostResult(subscription.Id);
            }
            
            ResultHistory result = new ResultHistory();
            Transaction transaction = new Transaction();
            var services = serviceRepository.GetServices();
            bool IsContainService = subscription.Services.Exists(service => service.Name.Equals(serviceName));
            if(!IsContainService)
            {
                transaction.Description = serviceName;
                transaction.Amount = services.First(service => service.Name.Equals(serviceName)).Price;
                transactionRepository.AddTransactionWithdrawal(subscription.Account.Id, transaction);
                subscription = subscriptionService.GetSubscription(subscription.Id);
                result.Access = Access.OK;
                result.Message = $"Withdrawal {transaction.Description}";
                result.Subscription = subscription;
                result.Service_Id = services.First(service => service.Name.Equals(serviceName)).Id;
            }
            else 
            {
                result.Access = Access.OK;
                result.Message = "Inclusive in subscription";
                result.Subscription = subscription;
                result.Service_Id = services.First(service => service.Name.Equals(serviceName)).Id;
            }
            checkRepository.AddResult(result);

            return result;
        }


        public List<ResultHistory> GetHistory(long subscriptionId)
        {
            var history = checkRepository.GetHistory(subscriptionId);
            history.ForEach(result => result.Subscription = subscriptionService.GetSubscription(subscriptionId));
            return history;
        }

        private bool IsAlowedAcccess(Subscription subscription)
        {
            return IsPositiveBalance(subscription) && IsExpirateSubscription(subscription);
        }
        private bool IsPositiveBalance(Subscription subscription)
        {
            return subscription.Account.Amount >= 0;
        }

        private bool IsExpirateSubscription(Subscription subscription)
        {
            return subscription.Expiration_Date.Date > DateTime.Now.Date;
        }
    }
}