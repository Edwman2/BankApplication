using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class CurrencyManager
    {

        private Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>();


        public CurrencyManager()
        {
            exchangeRates.Add("SEK", 1);    // SEK to SEK, no change
            exchangeRates.Add("USD", 0.1m); // 1 SEK = 0.1 USD (approx)
            exchangeRates.Add("EUR", 0.09m); // 1 SEK = 0.09 EUR (approx)
            exchangeRates.Add("JPY", 12.0m); // 1 SEK = 12 JPY (approx)
        }

        public void UpdateExchangeRate(string currency, decimal rate, bool isAdmin = false)
        {
            if (isAdmin)
            {
                exchangeRates[currency] = rate;
                return;
            }

            Console.WriteLine("Error: Only admin can update exchange rates");
        }

        public IReadOnlyDictionary<string, decimal> GetExchangeRates()
        {
            return exchangeRates;
        }

        public decimal? ConvertCurrency(Balance amount, Currency fromCurrency, Currency toCurrency)
        {
            if (!exchangeRates.ContainsKey(fromCurrency.AbbreviatedNameOfCurrency))
            {
                Console.WriteLine("Error: Exchange rate for " + fromCurrency.AbbreviatedNameOfCurrency+ " is missing");
                return null; 
            }
            if (!exchangeRates.ContainsKey(toCurrency.AbbreviatedNameOfCurrency))
            {
                Console.WriteLine("Error: Exchange rate for " + toCurrency.AbbreviatedNameOfCurrency + " is missing");
                return null;
            }

            return amount.Amount * exchangeRates[toCurrency.AbbreviatedNameOfCurrency] / exchangeRates[fromCurrency.AbbreviatedNameOfCurrency];
        }
    }
}