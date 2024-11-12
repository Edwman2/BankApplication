using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    // Sparkonto-klassen, som ärver från Account och hanterar tillämpning av sparränta
    internal class SavingsAccount : Account
    {
        // Konstruktor för att skapa ett sparkonto, med kontonummer, startbalans och valuta
        public SavingsAccount(string accountNumber, Balance initialBalance, Currency currency) //  AccountType,
            : base(accountNumber, initialBalance, currency) { } //  AccountType, 

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
}
