using System.Collections.Generic;
using GymApp.Models;

namespace GymApp.Repositories.interfaces
{
    public interface IServiceRepository
    {
        Service GetService(long id);
        List<Service> GetServices();
    }
}