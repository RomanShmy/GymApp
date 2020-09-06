using System;
using System.Collections.Generic;
using GymApp.Models;

namespace GymApp.Services.interfaces
{
    public interface IStatisticService
    {
       List<CountByType> GetByType(); 
       List<CountByType> GetByTypeWithDate(DateTime from, DateTime to);
    }
}