using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class Balance
    {
        public decimal Amount { get; set; }
        public Balance(decimal initialAmount)
        {
            Amount = initialAmount;
        }
    }
}
