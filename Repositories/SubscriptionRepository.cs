using System;
using Dapper;
using GymApp.Models;
using GymApp.Repositories.interfaces;

namespace GymApp.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private DataFactory db;

        public SubscriptionRepository(DataFactory db)
        {
            this.db = db;
        }
        public Subscription AddSubscription(Subscription subscription)
        {
            string query = "insert into public.subscription (type, expiration_date) values(@Type, @ExpirationDate) returning *";
            using(var connection = db.GetConnection())
            {
                var subscriptionResult = connection.QueryFirst<Subscription>(query, new {Type = subscription.Type, ExpirationDate = DateTime.Now.AddYears(1).Date});
                
                return subscriptionResult;
            }
        }
    }
}