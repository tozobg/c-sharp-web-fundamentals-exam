using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionSystem.Core.DTOs.Output
{
    public class AccountDto
    {
        public int AccountNumber { get; set; }
        public string FullName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
