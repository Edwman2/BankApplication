using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class InterestCalculator
    {
        // Beräknar sparränta baserat på balans och räntesats
        public decimal CalculateSavingsInterest(decimal balance, decimal interestRate)
        {
            return balance * interestRate;
        }

        // Beräknar låneränta baserat på lånebelopp och räntesats
        public decimal CalculateLoanInterest(decimal loanAmount, decimal interestRate)
        {
            return loanAmount * interestRate;
        }
    }

}