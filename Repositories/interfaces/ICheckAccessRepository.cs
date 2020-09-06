using System.Collections.Generic;
using GymApp.Models;

namespace GymApp.Repositories.interfaces
{
    public interface ICheckAccessRepository
    {
        List<ResultHistory> GetHistory(long subscriptionId);
        ResultHistory AddResult(ResultHistory resultHistory);
        List<ResultHistory> GetAllHistory();
    }
}