using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class BankApp
    {
        CurrencyManager currencyManager;
        
        public BankApp()
        {
            //Setup
            currencyManager = new CurrencyManager();
        }

        public void Run()
        {
            //Does nothing yet.
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
