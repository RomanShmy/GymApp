using System;

namespace GymApp.Models
{
    public class ResultHistory
    {
        public long Id { get; set; }
        public Subscription Subscription { get; set; }
        public Access Access { get; set; }
        public long Service_Id { get; set; }
        public Service Service { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}