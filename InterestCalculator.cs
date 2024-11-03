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

    // Sparkonto-klassen, som ärver från Account och hanterar tillämpning av sparränta
    public class SavingsAccount : Account
    {
        // Konstruktor för att skapa ett sparkonto, med kontonummer, startbalans och valuta
        public SavingsAccount(string accountNumber, Balance initialBalance, Currency currency)
            : base(accountNumber, initialBalance, currency) { }

        // Metod för att applicera sparränta på kontot
        public void ApplySavingsInterest(decimal interestRate)
        {
            var interestCalculator = new InterestCalculator();

            // Beräknar ränta baserat på nuvarande saldo
            decimal interest = interestCalculator.CalculateSavingsInterest(Balance.Amount, interestRate);

            // Lägger till beräknad ränta till kontots saldo
            Deposit(interest);
            Console.WriteLine($"Ränta på {interest} {Currency.AbbreviatedNameOfCurrency} har lagts till. Nytt saldo: {Balance.Amount}");
        }
    }

    // Lånekonto-klassen, som ärver från Account och hanterar låneberäkningar
    public class LoanAccount : Account
    {
        // Definierar en konstant multiplikator för maximalt lånebelopp (5 gånger saldot)
        private const decimal MaxLoanMultiplier = 5.0m;

        // Konstruktor för att skapa ett lånekonto med kontonummer, startbalans och valuta
        public LoanAccount(string accountNumber, Balance initialBalance, Currency currency)
            : base(accountNumber, initialBalance, currency) { }

        // Metod för att ansöka om ett lån med ett specifikt belopp och räntesats
        public void ApplyForLoan(decimal loanAmount, decimal interestRate)
        {
            // Beräknar maximalt lånebelopp som är tillåtet (5 gånger nuvarande saldo)
            decimal maxLoanAmount = Balance.Amount * MaxLoanMultiplier;
            if (loanAmount > maxLoanAmount)
            {
                // Meddelar användaren om lånebeloppet överskrider det tillåtna maximibeloppet
                Console.WriteLine($"Låneansökan nekad. Maximalt lånebelopp är {maxLoanAmount} {Currency.AbbreviatedNameOfCurrency}.");
                return;
            }

            var interestCalculator = new InterestCalculator();

            // Beräknar räntan för det önskade lånebeloppet
            decimal interest = interestCalculator.CalculateLoanInterest(loanAmount, interestRate);

            // Lägger till både lånebelopp och beräknad ränta för totalt lånebelopp
            decimal totalLoanAmount = loanAmount + interest;
            Deposit(totalLoanAmount);

            // Meddelar användaren om det beviljade lånet och den nya saldon
            Console.WriteLine($"Lån på {loanAmount} beviljat med ränta på {interest}. Totalt insatt: {totalLoanAmount}. Nytt saldo: {Balance.Amount}");
        }
    }
}