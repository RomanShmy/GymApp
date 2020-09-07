using GymApp.Models;

namespace GymApp.Services.interfaces
{
    public interface ISubscriptionService
    {
        Subscription AddSubscription(Subscription subscription);
        Subscription GetSubscription(long id);
    }
}