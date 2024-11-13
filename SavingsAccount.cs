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
        decimal interestRate = 0.05m;
        // Konstruktor för att skapa ett sparkonto, med kontonummer, startbalans och valuta
        public SavingsAccount(string accountNumber, Balance initialBalance, Currency currency) //  AccountType,
            : base(accountNumber, initialBalance, currency) { } //  AccountType, 

        // TODO - private List<TransactionLog> TransactionHistory { get; set; } LÖS DET

        // Metod för att applicera sparränta på kontot
        public void ApplySavingsInterest(decimal deposit)
        {
            Deposit(deposit);

            // Beräknar ränta baserat på nuvarande saldo
            var interestCalculator = new InterestCalculator();
            decimal interest = interestCalculator.CalculateSavingsInterest(Balance.Amount, interestRate);

            Deposit(interest);
            // Lägger till beräknad ränta till kontots saldo
            Console.WriteLine($"Interest of {interest} {Currency.AbbreviatedNameOfCurrency} has been added. New balance: {Balance.Amount}");
        }
    }
}
