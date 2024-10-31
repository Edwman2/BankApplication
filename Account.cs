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
        - Diskutera: 
            * Hur ni vill hantera kontonummer och valutor.
            * Vem ska hantera "messages to user"? Jag kan göra om Withdraw() och Deposit() till bool istället t.ex.
            * Separata klasserna Balance och Currency. Vad kan det tillföra till andra?
            * 

    */

    /*      Tests passed.
     
            Balance initialBalance = new Balance(0.00m);
            Currency defaultCurrency = new Currency("SEK");
            Account accountTest1 = new Account("AB001", initialBalance, defaultCurrency);
            accountTest1.DisplayInfo();
            accountTest1.Deposit(30m);
            accountTest1.Withdraw(20m);
            accountTest1.Withdraw(20m);
            Console.ReadLine();

     */
    internal class Account
    {
        // egenskaper som kontonummer, saldo och valuta 
        // TODO - Kontotyp?
        public string AccountNumber { get; } // TODO - Maybe change to GUID? public Guid AccNo ... AccNo = Guid.NewGuid();
        public Balance Balance { get; private set; } 
        public Currency Currency { get; } 

        public Account(string accountNumber, Balance initialBalance, Currency currency)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
            Currency = currency;
        }

        // --------------- Methods for Account related functionality ---------------

        
        public void DisplayInfo()
        {
            // Displays Account info
            Console.WriteLine($"Account Number: {AccountNumber,8} \n" +
                $"Total Balance: {Balance.Amount,8} {Currency.AbbreviatedNameOfCurrency}\n");
        }
        
        public void Withdraw(decimal amountToWithdraw)
        {
            // Withdraw the amount
            if (Balance.Amount >= amountToWithdraw)
            {
                Balance.Amount -= amountToWithdraw;
                Console.WriteLine("Money was withdrawn successfully!");
                DisplayInfo();
            }
            else Console.WriteLine("Not enough money in your account!"); 
            
        }
        public void Deposit(decimal amountToDeposit)
        {
            // Deposit the amount
            Balance.Amount += amountToDeposit;
            Console.WriteLine("Money was deposited successfully!");
            DisplayInfo();
        }
    }
}
