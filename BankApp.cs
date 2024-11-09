using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        }

        public void Run()
        {
            //Does nothing yet.
            /* ---------------- Cuurency manager calls to methods? ---------------
            var temp = currencyManager.GetExchangeRates();

            //loop through temp and write out the exchange rates and key.
            foreach (var item in temp)
            {
                Console.WriteLine("Key: " + item.Key.AbbreviatedNameOfCurrency + " Value: " + item.Value);
            }

            Console.ReadLine();
            */
        }
    }
}
