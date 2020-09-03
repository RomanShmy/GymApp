using GymApp.Models;

namespace GymApp
{
    public interface IAccountRepository
    {
        Account GetAccount(long id);
        Account CreateAccount(long subscriptionId);
        
    }
}