using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    /*
     Steg 2: Kontohantering och Saldofunktion (Simon Eke)

    För att användare ska kunna se sina konton och saldo, skapa en Account-klass som innehåller egenskaper som kontonummer, saldo och valuta. 
    Lägg till metoder för att sätta in och ta ut pengar.
    Rekommendationer och tips:
        - Skapa en lista över konton för varje användare.
        - Implementera metoder för insättning och uttag, men tänk på att uttag bara ska vara möjliga om saldot tillåter det.
        - Diskutera hur ni vill hantera kontonummer och valutor.

    */
    internal class Account
    {
        // egenskaper som kontonummer, saldo och valuta
        public string AccountNumber { get; set; } // TODO - Maybe change to GUID? public Guid AccNo ... AccNo = Guid.NewGuid();
        public decimal Balance { get; set; } // Kanske behandla Balance som ett objekt
        public string Currency { get; set; } // Också behandla Currency som ett objekt

        public Account(string accountNumber, decimal initialBalance, string currency)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
            Currency = currency;
        }

        // Methods for Account related functionality
        public void DisplayInfo()
        {
            // Displays Account info
            Console.WriteLine($"Account Number: {AccountNumber.PadLeft(6)} \n" +
                $"Total Balance: {Balance.ToString().PadLeft(8)} {Currency}\n");
        }
        public void Withdraw(string accountNumber, decimal amount)
        {
            // Withdraw the amount
        }
        public void Deposit(string accountNumber, decimal amount)
        {
            // Deposit the amount
        }
    }
}
