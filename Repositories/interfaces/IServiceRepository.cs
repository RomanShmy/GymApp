using System.Collections.Generic;
using GymApp.Models;

namespace GymApp.Repositories.interfaces
{
    public interface IServiceRepository
    {
        Service GetService(string serviceName);
        List<Service> GetServices();
    }
}