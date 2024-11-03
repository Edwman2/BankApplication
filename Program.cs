using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TransactionManager transaction1 = new TransactionManager(1001, 1002, 500, DateTime.Now, TransactionManager.AddTransactionID(),615);
            TransactionManager transaction2 = new TransactionManager(1003, 1006, 601, DateTime.Now, TransactionManager.AddTransactionID(), 615);
            TransactionManager transaction3 = new TransactionManager(1034, 1002, 5850, DateTime.Now, TransactionManager.AddTransactionID(), 6000);
            transaction1.DisplayTransactionInfo();
            transaction1.AddFee();
            transaction1.CheckBalance();
            transaction2.DisplayTransactionInfo();
            transaction2.AddFee();
            transaction2.CheckBalance();
            transaction3.DisplayTransactionInfo();
            
            transaction3.CheckBalance();

            Console.ReadKey();
        }
    }
}
