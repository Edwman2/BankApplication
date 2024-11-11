using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class TransactionLog
    {
        internal string FromUser { get; set; } // AccNumber
        internal string ToUser { get; set; } // AccNumber
        internal decimal Amount { get; set; } // Transaction Amount
        internal string ErrorMessage { get; set; }
        internal DateTime dateTime = DateTime.Now; // Ska 

        internal TransactionLog(string fromuser, string touser, decimal amount)
        {
            FromUser = fromuser;
            ToUser = touser;
            Amount = amount;
        }
    }
}
