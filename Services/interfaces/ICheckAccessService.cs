using System.Collections.Generic;
using GymApp.Models;

namespace GymApp.Services.interfaces
{
    public interface ICheckAccessService
    {
        ResultHistory PostResult(long subscriptionId);
        List<ResultHistory> GetHistory(long subscriptionId);
        ResultHistory PostResultService(long subscriptionId, string serviceName);
    }
}