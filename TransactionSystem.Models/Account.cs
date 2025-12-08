using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransactionSystem.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AccountNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; } = null!;

        [Required]
        public decimal Balance { get; set; }

        // Navigation properties
        public ICollection<Deposit> Deposits { get; set; } = new HashSet<Deposit>();
        public ICollection<Withdraw> Withdraws { get; set; } = new HashSet<Withdraw>();
        public ICollection<Transfer> TransfersFrom { get; set; } = new HashSet<Transfer>();
        public ICollection<Transfer> TransfersTo { get; set; } = new HashSet<Transfer>();
    }
}
