using GymApp.Models;

namespace GymApp.Services.interfaces
{
    public interface IAccountService
    {
        Account GetAccount(long id);
        Account ReplenishmentBalance(long accountId, Transaction transaction);
        Account WithdrawalBalance(long accountId, Transaction transaction);
    }
}