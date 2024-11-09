using System;
using System.Security.Principal;

namespace BankApplication
{
    internal class BankApp
    {
        public CurrencyManager currencyManager;
        public AccountManager UserAccounts = new();
        public TransactionManager ts; 
        
        public BankApp()
        {
            //Setup
            ts = new TransactionManager(UserAccounts);
            ts.HandleUnprocessedTransactions();

            UserAccounts.AddAccount("BA0001", "SEK"); // Till Account Manager
            UserAccounts.AddAccount("BA6753", "SEK");

            Account accountTest1 = UserAccounts.FindAccount("BA0001");
            accountTest1.Deposit(800m);
            Account accountTest2 = UserAccounts.FindAccount("BA6753");
            accountTest2.Deposit(600m);
            

            ts.TransactionRequest("BA0001", "BA6753", 500);
            ts.TransactionRequest("BA6753", "BA0001", 500);
            ts.TransactionRequest("BA0001", "BA6753", 500);
            ts.TransactionRequest("BA0001", "BA6753", 1500);
            ts.TransactionRequest("BA000", "BA6753", 500);
            ts.TransactionRequest("BA0001", "BA675", 500);

            /* Output: 
                    Handling transactions 6
                    both accounts were found and the transaction is in progress. 500 SEK is on it's way.
                    Sender: BA0001
                    Reciever: BA6753
                    both accounts were found and the transaction is in progress. 500 SEK is on it's way.
                    Sender: BA6753
                    Reciever: BA0001
                    both accounts were found and the transaction is in progress. 500 SEK is on it's way.
                    Sender: BA0001
                    Reciever: BA6753
                    Not enough money in your account
                    Your account can not be found
                    The account you are trying to transfer money to can not be found
                    BA0001,BA6753, 2024-11-09 22:44:53, 500
                    BA6753,BA0001, 2024-11-09 22:44:54, 500
                    BA0001,BA6753, 2024-11-09 22:44:55, 500
             */

            Console.ReadKey();

        }

        public void Run()
        {
            //Does nothing yet.
            //----------------Cuurency manager calls to methods? ---------------
           var temp = currencyManager.GetExchangeRates();

            //loop through temp and write out the exchange rates and key.
            foreach (var item in temp)
            {
                Console.WriteLine("Key: " + item.Key.AbbreviatedNameOfCurrency + " Value: " + item.Value);
            }

            Console.ReadLine();

        }
    }
}
