using System.Collections.Generic;
using GymApp.Models;

namespace GymApp.Services.interfaces
{
    public interface ICheckAccessService
    {
        ResultHistory CheckBalanceExpirationDateAndLogEntry(long subscriptionId);
        List<ResultHistory> GetHistory(long subscriptionId);
        ResultHistory CheckInclusiveServiceAndLogEntry(long subscriptionId, string serviceName);
    }
}