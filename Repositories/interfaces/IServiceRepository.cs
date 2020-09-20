using System.Collections.Generic;
using GymApp.Models;

namespace GymApp.Repositories.interfaces
{
    public interface IServiceRepository
    {
        Service GetServiceByName(string serviceName);
        List<Service> GetServices();
         Service GetServiceById(long serviceId);
    }
}