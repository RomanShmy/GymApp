using System;
using System.ComponentModel.DataAnnotations;

namespace GymApp.Models
{
    public class Subscription
    {
        public long Id { get; set; }
        public Account Account { get; set; }

        public Coverage Coverage { get; set; }
        public TypeSubscription Type { get; set; }
        public DateTime Expiration_Date { get; set; }

    }
}