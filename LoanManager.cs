using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class LoanManager
    {
        // Definierar en konstant multiplikator för maximalt lånebelopp (5 gånger saldot)
        private const decimal MaxLoanMultiplier = 5.0m;

        // Metod för att ansöka om ett lån med ett specifikt belopp och räntesats
        public void ApplyForLoan(Account account, decimal loanAmount, Balance totalBalance, Currency currency, decimal interestRate)
        {
            
            // Beräknar maximalt lånebelopp som är tillåtet (5 gånger nuvarande saldo)
            decimal maxLoanAmount = totalBalance.Amount * MaxLoanMultiplier;
            if (loanAmount > maxLoanAmount)
            {
                // Meddelar användaren om lånebeloppet överskrider det tillåtna maximibeloppet
                Console.WriteLine($"Låneansökan nekad. Maximalt lånebelopp är " +
                    $"{maxLoanAmount} {currency.AbbreviatedNameOfCurrency}. Sätt in eller för över mer pengar.");
                return;
            }

            var interestCalculator = new InterestCalculator();

            // Beräknar räntan för det önskade lånebeloppet
            decimal interest = interestCalculator.CalculateLoanInterest(loanAmount, interestRate);

            // Lägger till både lånebelopp och beräknad ränta för totalt lånebelopp
            decimal totalLoanAmount = loanAmount + interest;

            account.Deposit(totalLoanAmount);
            // Meddelar användaren om det beviljade lånet och den nya saldon
            Console.WriteLine($"Lån på {loanAmount} beviljat med ränta på {interest}. " +
                $"Totalt insatt: {totalLoanAmount}. Nytt saldo: {totalBalance.Amount}");
        }
    }
}
