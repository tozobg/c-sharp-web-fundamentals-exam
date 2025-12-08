using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionSystem.Models
{
    public class Deposit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Money { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }

        // Navigation property
        public Account Account { get; set; } = null!;
    }
}
