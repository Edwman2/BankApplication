using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{

    internal class TransactionManager
    {
        public int WithdrawFrom { get; set; }
        public int DepositTo { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate = DateTime.Now;
        public int TransactionID { get; set; }
        public decimal TransactionFee { get; set; }


        internal TransactionManager(int withdrawFrom, int depositTo, decimal amount, DateTime date, int transactionID, decimal balance)
        {
            WithdrawFrom = withdrawFrom;
            DepositTo = depositTo;
            Amount = amount;
            TransactionDate = date;
            TransactionID = transactionID;
            TransactionFee = 15;
            Balance = balance;
        }


        
        internal static int AddTransactionID()
        {
            return new Random().Next(100, 1000000);
        }
        internal void AddFee()
        {
            if (Amount > 5000)
            {
                Amount = Amount;
            }
            else
            {
                Amount = Amount + TransactionFee;
                Console.WriteLine("Transationfee has been added: " + TransactionFee +"kr");
            }

        }
        internal void CheckBalance()
        {
            if (Amount > Balance)
            {
                Console.WriteLine("you have insufficient balance");
            }
            else
            {
                Console.WriteLine("Transaction successful! You have transfered :" + Amount + "kr");
            }
        }

        internal void DisplayTransactionInfo()
        {
            Console.WriteLine("Transaction ID: " + TransactionID);
            Console.WriteLine("Withdraw From: " + WithdrawFrom);
            Console.WriteLine("Deposit To: " + DepositTo);
            Console.WriteLine("Amount: " + Amount);
            Console.WriteLine("Date: " + TransactionDate);
            Console.WriteLine("Balance: " + Balance);
            
        }

    }   

    
}
