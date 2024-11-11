using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class Currency
    {
        public string AbbreviatedNameOfCurrency { get; set; } // SEK, EUR, USD etc.
        public Currency(string nameOfCurrency)
        {
            AbbreviatedNameOfCurrency = nameOfCurrency;
        }
    }
}
