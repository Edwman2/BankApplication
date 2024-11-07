using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class BankApp
    {

        public BankApp()
        {
            //Setup
            TransactionManager ts = new TransactionManager();
            ts.HandleUnprocessedTransactions();




            Account account = new Account("BA0001", 800);
            Account account1 = new Account("BA6753", 600);
            




            ts.AddAccount(account);
            ts.AddAccount(account1);
            ts.FindAccounts("BA0001");
            ts.FindAccounts("BA6753");

            
            ts.TransactionRequest("BA0001", "BA6753", 500);
            
            //ts.ProcessedAccounts("BA0001", "BA6753", 500);
            //ts.ProcessedAccounts("BA6753", "BA0001", 500);
            
            
            
            account.showinfo();
            



            Console.ReadKey();

        }

        public void Run()
        {
            //Does nothing yet.
            
        }
    }
}
