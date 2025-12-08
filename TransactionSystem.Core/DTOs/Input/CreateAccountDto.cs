using System.ComponentModel.DataAnnotations;

namespace TransactionSystem.Core.DTOs.Input
{
    public class CreateAccountDto
    {
        [Required]
        public required int AccountNumber { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 255 characters.")]
        public required string FullName { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public required decimal Balance { get; set; }
    }
}
