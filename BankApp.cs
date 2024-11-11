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
