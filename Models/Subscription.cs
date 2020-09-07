using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace GymApp.Models
{
    public class Subscription
    {
        public long Id { get; set; }
        public Account Account { get; set; }
        public List<Service> Services { get; set; } = new List<Service>();
        public Coverage Coverage { get; set; }
        public TypeSubscription Type { get; set; }
        public int CountMonth { get; set; }
        public DateTime Register_Date {get; set;}
        public DateTime Expiration_Date { get; set; }

    }
}