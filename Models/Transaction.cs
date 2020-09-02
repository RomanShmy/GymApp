using System;
namespace GymApp.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public long AccountId { get; set; }
    }
}