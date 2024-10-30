using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public class CurrencyManager
    {
        private Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>();


        

        //TODO:
        //Adjust bool value for class check later
        //Should currency be object based?
        public void UpdateExchangeRate(string currency, decimal rate, bool isAdmin = false)
        {
            if (isAdmin)
            {
                exchangeRates[currency] = rate;
                return;
            }
            //throw new UnauthorizedAccessException("Only admin can update exchange rates");
            Console.WriteLine("Error: Only admin can update exchange rates");
        }

        public decimal? ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
        {
            if (!exchangeRates.ContainsKey(fromCurrency))
            {
                Console.WriteLine("Error: Exchange rate for " + fromCurrency + " is missing");
                return null; // Standardåtgärd om växelkurser saknas
            }
            if (!exchangeRates.ContainsKey(toCurrency))
            {
                Console.WriteLine("Error: Exchange rate for " + toCurrency + " is missing");
                return null;
            }

            return amount * exchangeRates[toCurrency] / exchangeRates[fromCurrency];
        }
        //Would be called like this;
        //decimal myCurrency = 20;
        //decimal? myNewCurrency = ConvertCurrency(myCurrency, "EUR", "US");

        //if (myNewCurrency.HasValue)
        //{
        //    // Proceed with myNewCurrency
        //}
        //else
        //{
        //    // Handle the error (e.g., log it, notify the user, etc.)
        //}
    }
}

//EX
//public decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
//{
//    if (!exchangeRates.ContainsKey(fromCurrency))
//    {
//        throw new ArgumentException("Exchange rate for " + fromCurrency + " is missing");
//    }
//    if (!exchangeRates.ContainsKey(toCurrency))
//    {
//        throw new ArgumentException("Exchange rate for " + toCurrency + " is missing");
//    }

//    return amount * exchangeRates[toCurrency] / exchangeRates[fromCurrency];
//}

//calling that would look like this:
//decimal myCurrency = 20;

//try
//{
//    decimal myNewCurrency = ConvertCurrency(myCurrency, "EUR", "US");
//    // Proceed with myNewCurrency
//}
//catch (ArgumentException ex)
//{
//    Console.WriteLine("Conversion failed: " + ex.Message);
//    // Handle the error
//}

//-------------
//EX
//public decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
//{
//    if (!exchangeRates.ContainsKey(fromCurrency))
//    {
//        throw new ArgumentException("Exchange rate for " + fromCurrency + " is missing");
//    }
//    if (!exchangeRates.ContainsKey(toCurrency))
//    {
//        throw new ArgumentException("Exchange rate for " + toCurrency + " is missing");
//    }

//    return amount * exchangeRates[toCurrency] / exchangeRates[fromCurrency];
//}