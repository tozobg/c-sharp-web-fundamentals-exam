using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionSystem.Models
{
    public sealed class Transfer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Money { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey(nameof(FromAccount))]
        public int FromAccountId { get; set; }
        // Navigation property
        public Account FromAccount { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(ToAccount))]
        public int ToAccountId { get; set; }
        // Navigation property
        public Account ToAccount { get; set; } = null!;
    }
}
