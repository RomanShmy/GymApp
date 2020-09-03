using System;
namespace GymApp.Models
{
    public class Subscription
    {
        public long Id { get; set; }
        public Account Account { get; set; }
        public TypeSubscription Type { get; set; }
        public DateTime Expiration_Date { get; set; }

    }
}