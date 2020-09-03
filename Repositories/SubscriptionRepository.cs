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
            string query = "insert into public.subscription (type, expiration_date) values(@Type, @ExpirationDate) returning id";
            using(var connection = db.GetConnection())
            {
                var id = connection.QueryFirst<long>(query, new {Type = (int)subscription.Type, ExpirationDate = subscription.Expiration_Date});
                subscription.Id = id;
                return subscription;
            }
        }
    }
}