using GymApp.Models;

namespace GymApp.Repositories.interfaces
{
    public interface ISubscriptionRepository
    {
        Subscription AddSubscription(Subscription subscription);
        Subscription GetSubscription(long id);
    }
}