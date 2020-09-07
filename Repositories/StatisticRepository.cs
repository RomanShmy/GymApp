using System;
using GymApp.Models;
using Dapper;
using System.Collections.Generic;

namespace GymApp.Repositories.interfaces
{
    public class StatisticRepository : IStatisticRepository
    {
        private DataFactory db;
        public StatisticRepository(DataFactory db)
        {
            this.db = db;
        }

        public List<QuantityTimeOfVisit> GetStatisticByService()
        {
            string query = "select s.name as ServiceName, count(h.id) " + 
                            "from public.services as s " + 
                            "left join public.result_history as h " +
                            "on s.id = h.service_id " + 
                            "where h.access = 1 " + 
                            "group by s.id;";

            using(var connection = db.GetConnection())
            {
                var statistic = connection.Query<QuantityTimeOfVisit>(query).AsList();
                return statistic;
            }

        }

        public List<QuantityTimeOfVisit> GetStatisticByServiceDate(DateTime from, DateTime to)
        {
            string query = "select s.name as ServiceName, count(h.id) " + 
                            "from public.services as s " + 
                            "left join public.result_history as h " +
                            "on s.id = h.service_id " + 
                            "where h.access = 1 and @From < h.date and h.date < @To " + 
                            "group by s.id;";

            using(var connection = db.GetConnection())
            {
                var statistic = connection.Query<QuantityTimeOfVisit>(query, new {From = from, To = to}).AsList();
                return statistic;
            }

        }
    }
}