using System;
using System.Collections.Generic;
using Dapper;
using GymApp.Models;
using GymApp.Repositories.interfaces;

namespace GymApp.Repositories
{
    public class CheckAccessRepository : ICheckAccessRepository
    {
        private DataFactory db;
        public CheckAccessRepository(DataFactory db)
        {
            this.db = db;
        }

        public ResultHistory AddResult(ResultHistory resultHistory)
        {
            string query = "insert into public.result_history (subscription_id, access, message, date, service_id) values(@SubscriptionId, @Access, @Message, @Date, @ServiceId) returning *";
            using (var connection = db.GetConnection())
            {
                var result = connection.QueryFirst<ResultHistory>(query, new {SubscriptionId = resultHistory.Subscription.Id, 
                                                                                               Access = resultHistory.Access, 
                                                                                               Message = resultHistory.Message, 
                                                                                               Date = DateTime.Now,
                                                                                               ServiceId = resultHistory.Service_Id});
                return result;
            }
            
        }

        public List<ResultHistory> GetHistory(long subscriptionId)
        {
            string query = "select * from public.result_history where subscription_id = @Id";
            using (var connection = db.GetConnection())
            {
                var history = connection.Query<ResultHistory>(query, new {Id = subscriptionId}).AsList();
                return history;
            }
        }

        public List<ResultHistory> GetAllHistory()
        {
            string query = "select * from public.result_history";
            using (var connection = db.GetConnection())
            {
                var history = connection.Query<ResultHistory>(query).AsList();
                
                return history;
            }
        }
    }
}