using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionSystem.Core.DTOs.Output
{
    public class TransactionDto
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
