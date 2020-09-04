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
            string query = "insert into public.result_history (subscription_id, access, message, date) values(@SubscriptionId, @Access, @Message, @Date) returning id";
            using (var connection = db.GetConnection())
            {
                var id = connection.QueryFirst<int>(query, new {SubscriptionId = resultHistory.Subscription.Id, Access = resultHistory.Access, Message = resultHistory.Message, Date = DateTime.Now});
                resultHistory.Id = id;
            }
            return resultHistory;
        }

        public List<ResultHistory> GetHistory()
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