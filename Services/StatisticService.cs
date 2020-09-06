using System;
using System.Collections.Generic;
using System.Linq;
using GymApp.Models;
using GymApp.Repositories.interfaces;
using GymApp.Services.interfaces;

namespace GymApp.Services
{
    public class StatisticService : IStatisticService
    {
        private ISubscriptionRepository subscriptionRepository;
        public StatisticService(ISubscriptionRepository subscriptionRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
        }
        public List<CountByType> GetByType()
        {
            var subscriptions = subscriptionRepository.GetSubscriptions();
            var count = subscriptions.Count - 1;
            Dictionary<TypeSubscription, int> types = new Dictionary<TypeSubscription, int>();
            List<CountByType> statistics = new List<CountByType>();
            foreach(var type in Enum.GetValues(typeof(TypeSubscription)))
            {
                foreach (var sub in subscriptions)
                {
                    if (types.ContainsKey((TypeSubscription) type))
                    {
                        if (sub.Type.Equals((TypeSubscription) type))
                        {
                            types[(TypeSubscription) type] += 1;
                        }
                    }
                    else
                    {        
                        types.Add((TypeSubscription) type, 0);  
                    }
                }
                CountByType element = new CountByType();
                element.Type = (TypeSubscription) type;
                element.Percent = Math.Round(types[(TypeSubscription) type] * 100.0 / count);
                statistics.Add(element);

            }

            return statistics;
        }

        public List<CountByType> GetByTypeWithDate(DateTime from, DateTime to)
        {
            var subscriptions = subscriptionRepository.GetSubscriptions();
            
            Dictionary<TypeSubscription, int> types = new Dictionary<TypeSubscription, int>();
            List<CountByType> statistics = new List<CountByType>();
            foreach(var type in Enum.GetValues(typeof(TypeSubscription)))
            {
                foreach (var sub in subscriptions)
                {
                    if (types.ContainsKey((TypeSubscription) type))
                    {
                        if (sub.Type.Equals((TypeSubscription) type) && sub.Register_Date != null && from < sub.Register_Date && sub.Register_Date < to)
                        {
                            types[(TypeSubscription) type] += 1;
                        }
                    }
                    else
                    {        
                        types.Add((TypeSubscription) type, 0);  
                    }
                }
            }
            int count = types.Sum(t => t.Value);

            for (int i = 0; i < types.Count; i++)
            {
                CountByType element = new CountByType();
                element.Type = types.ElementAt(i).Key;
                element.Percent = Math.Round(types.ElementAt(i).Value * 100.0 / count);
                statistics.Add(element);
            }

            return statistics;
        }
    }
}