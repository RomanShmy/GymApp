using System.Collections.Generic;

namespace GymApp.Models
{
    public class Account
    {
        public long Id { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}