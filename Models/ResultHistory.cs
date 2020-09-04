using System;

namespace GymApp.Models
{
    public class ResultHistory
    {
        public long Id { get; set; }
        public Subscription Subscription { get; set; }
        public Access Access { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}