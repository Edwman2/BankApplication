using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class TransactionLog
    {
        internal decimal Amount { get; set; } // Transaction Amount
        internal string FromUser { get; set; } // AccNumber
        internal string ToUser { get; set; } // AccNumber
        internal string ErrorMessage { get; set; }
        internal DateTime dateTimeRequested = DateTime.Now; // Ska 
        internal DateTime dateTimeCompleted = DateTime.Now; // Ska 
        internal string status;

        internal TransactionLog(string fromuser, string touser, decimal amount)
        {
            FromUser = fromuser;
            ToUser = touser;
            Amount = amount;
            status = "Pending";
        }
    }
}
