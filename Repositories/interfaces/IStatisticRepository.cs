using System;
using System.Collections.Generic;
using GymApp.Models;

namespace GymApp.Repositories.interfaces
{
    public interface IStatisticRepository
    {
        List<QuantityTimeOfVisit> GetStatisticByService();
        List<QuantityTimeOfVisit> GetStatisticByServiceDate(DateTime from, DateTime to);
    }
}