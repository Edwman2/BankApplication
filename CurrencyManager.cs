using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class CurrencyManager
    {
        private Dictionary<Currency, decimal> exchangeRates = new Dictionary<Currency, decimal>();


        public CurrencyManager()
        {
            exchangeRates.Add(new Currency("SEK"), 1);
            exchangeRates.Add(new Currency("USD"), 0.12m);
            exchangeRates.Add(new Currency("EUR"), 0.10m);
            exchangeRates.Add(new Currency("JPY"), 13.0m);
        }

        public void UpdateExchangeRate(Currency currency, decimal rate, bool isAdmin = false)
        {
            if (isAdmin)
            {
                exchangeRates[currency] = rate;
                return;
            }

            Console.WriteLine("Error: Only admin can update exchange rates");
        }

        public IReadOnlyDictionary<Currency, decimal> GetExchangeRates()
        {
            return exchangeRates;
        }

        public decimal? ConvertCurrency(Balance amount, Currency fromCurrency, Currency toCurrency)
        {
            if (!exchangeRates.ContainsKey(fromCurrency))
            {
                Console.WriteLine("Error: Exchange rate for " + fromCurrency + " is missing");
                return null; 
            }
            if (!exchangeRates.ContainsKey(toCurrency))
            {
                Console.WriteLine("Error: Exchange rate for " + toCurrency + " is missing");
                return null;
            }

            return amount.Amount * exchangeRates[toCurrency] / exchangeRates[fromCurrency];
        }
    }
}