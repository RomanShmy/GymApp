using System.Collections.Generic;

namespace GymApp.Models
{
    public class Account
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public long Subscription_Id { get; set; }
    }
}