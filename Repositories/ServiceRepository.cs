using System.Collections.Generic;
using Dapper;
using GymApp.Models;
using GymApp.Repositories.interfaces;

namespace GymApp.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private DataFactory db;

        public ServiceRepository(DataFactory db)
        {
            this.db = db;
        }

        public List<Service> GetServices()
        {
            string query = "select * from public.services";
            using(var connection = db.GetConnection())
            {
                var service = connection.Query<Service>(query).AsList();
                return service;
            }
        }
        public Service GetService(long id)
        {
            string query = "select * from public.services where id = @Id";
            using(var connection = db.GetConnection())
            {
                var service = connection.QueryFirst<Service>(query, new {Id = id});
                return service;
            }
        }
    }
}