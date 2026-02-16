using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionSystem.Core.DTOs.Output
{
    public class TransactionDetailsDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Money { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty; // "Deposit" or "Withdrawal"
    }
}
