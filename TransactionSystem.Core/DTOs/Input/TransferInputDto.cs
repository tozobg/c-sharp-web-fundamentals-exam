using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionSystem.Core.DTOs.Input
{
    public class TransferInputDto
    {
        [Required]
        public required int FromAccountNumber { get; set; }

        [Required]
        public required int ToAccountNumber { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Money must be greater than 0.")]
        public required decimal Money { get; set; }
    }
}
