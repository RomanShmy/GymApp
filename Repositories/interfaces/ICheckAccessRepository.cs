using System.Collections.Generic;
using GymApp.Models;

namespace GymApp.Repositories.interfaces
{
    public interface ICheckAccessRepository
    {
        List<ResultHistory> GetHistory();
        ResultHistory AddResult(ResultHistory resultHistory);
    }
}