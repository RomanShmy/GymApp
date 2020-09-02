using GymApp.Models;

namespace GymApp.Services.interfaces
{
    public interface IAccountService
    {
        Account GetAccount(long id);
    }
}