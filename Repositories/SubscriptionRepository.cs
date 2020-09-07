using System;
using System.Collections.Generic;
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

        public List<Subscription> GetSubscriptions()
        {
            string query = "select * from public.subscription";
            using(var connection = db.GetConnection())
            {
                var subscriptions = connection.Query<Subscription>(query).AsList();
                
                return subscriptions;
            }
        }
        public Subscription AddSubscription(Subscription subscription)
        {
            string query = "insert into public.subscription (type, coverage, expiration_date, register_date) values(@Type, @Coverage,@ExpirationDate,@RegisterDate) returning *";
            using(var connection = db.GetConnection())
            {
                var subscriptionResult = connection.QueryFirst<Subscription>(query, new {Type = subscription.Type,
                                                                                         Coverage = subscription.Coverage, 
                                                                                         ExpirationDate = DateTime.Now.AddMonths(subscription.CountMonth).Date,
                                                                                         RegisterDate = DateTime.Now});
                return subscriptionResult;
            }
        }

        public Subscription GetSubscription(long id)
        {
            string query = "select * from public.subscription where id = @Id;";
            using(var connection = db.GetConnection())
            {
                var subscriptionResult = connection.QueryFirst<Subscription>(query, new {Id = id});
                
                return subscriptionResult;
            }
        }
    }
}