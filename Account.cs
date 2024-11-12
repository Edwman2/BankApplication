using System;

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
     
            
            AccountManager userAccounts = new AccountManager();
            userAccounts.AddAccount("Tre37", "SEK");
            userAccounts.AddAccount("Två48", "SEK");
            userAccounts.AddAccount("Ett98", "SEK");
            userAccounts.DisplayAccounts();

            Account accountTest1 = userAccounts.FindAccount("Tre37");
            accountTest1.DisplayInfo();
            accountTest1.Deposit(30m); 
            accountTest1.Withdraw(20m); // returns bool
            accountTest1.Withdraw(20m); // returns bool
            accountTest1.DisplayInfo();

            Console.ReadLine();

    Tests RESULT:
Account Number:    Tre37
Total Balance:     0,00 SEK

Account Number:    Två48
Total Balance:     0,00 SEK

Account Number:    Ett98
Total Balance:     0,00 SEK

Account Number:    Tre37
Total Balance:     0,00 SEK

Account Number:    Tre37
Total Balance:    10,00 SEK

    CONCLUSION: Ugly formatting with padding in display info.

     */
    internal class Account
    {

        // egenskaper som kontonummer, saldo och valuta 
        // TODO - Kontotyp? enum?
        public string AccountNumber { get; private set; } // TODO - Maybe change to GUID? public Guid AccNo ... AccNo = Guid.NewGuid();
        public Balance Balance { get; private set; }
        public Currency Currency { get; private set;} 

        public Account(string accountNumberInput, Balance initialBalance, Currency currencyInput)
        {
            AccountNumber = accountNumberInput;
            Balance = initialBalance;
            Currency = currencyInput;
        }
        
        public void DisplayInfo()
        {
            // Displays Account info
            // ,8 is instead of "Account Number:         {AccountNumber} ==
            // == "Pads" the accountnumber to the right by adding spaces to the left.
            Console.WriteLine($"Account Number: {AccountNumber,8} \n" +
                $"Total Balance: {Balance.Amount,8} {Currency.AbbreviatedNameOfCurrency}\n");
        }
        public bool Withdraw(decimal amountToWithdraw)
        {
            // Withdraw the amount from one account
            if (Balance.Amount >= amountToWithdraw)
            {
                Balance.Amount -= amountToWithdraw;
                return true;
            }
            else return false;
        }
        public void Deposit(decimal amountToDeposit)
        {
            // Deposit the amount ToAnother Account
            Balance.Amount += amountToDeposit;
        }
    }
}
