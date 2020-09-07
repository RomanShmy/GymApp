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
        public CheckAccessService(ICheckAccessRepository checkRepository, ISubscriptionService subscriptionService)
        {
            this.checkRepository = checkRepository;
            this.subscriptionService = subscriptionService;
        }

        public ResultHistory PostResult(long subscriptionId)
        {
            var subscription = subscriptionService.GetSubscription(subscriptionId);
            ResultHistory result = new ResultHistory();
            result.Subscription = subscription;
            result.Access = Access.NoAccess;
            result.Message = "No access, ";
            if (IsPositiveBalance(subscription) && IsExpirateSubscription(subscription))
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

        public List<ResultHistory> GetHistory(long subscriptionId)
        {
            var history = checkRepository.GetHistory(subscriptionId);
            history.ForEach(result => result.Subscription = subscriptionService.GetSubscription(subscriptionId));
            return history;
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