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
        public Service GetServiceByName(string serviceName)
        {
            string query = "select * from public.services where name = @ServiceName";
            using(var connection = db.GetConnection())
            {
                var service = connection.QueryFirst<Service>(query, new {ServiceName = serviceName});
                return service;
            }
        }

        public Service GetServiceById(long serviceId)
        {
            string query = "select * from public.services where id = @Id";
            using(var connection = db.GetConnection())
            {
                var service = connection.QueryFirst<Service>(query, new {Id = serviceId});
                return service;
            }
        }
        
    }
}