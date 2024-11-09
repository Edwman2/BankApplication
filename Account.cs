using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        // TODO - Kontotyp?
        //public enum AccountType
        //{
        //    Debit,
        //    Saving,
        //    Loan
        //}

        public string accountNumber { get; private set; } // TODO - Maybe change to GUID? public Guid AccNo ... AccNo = Guid.NewGuid();
        protected Balance balance { get; private set; }
        protected Currency currency { get; private set;} 

        public Account(string accountNumberInput, Balance initialBalance, Currency currencyInput)
        {
            accountNumber = accountNumberInput;
            balance = initialBalance;
            currency = currencyInput;
        }

        List<TransactionLog> transactionslogged = new List<TransactionLog>();

        public void AddTransactionLog(TransactionLog transaction)
        {
            transactionslogged.Add(transaction);
            // Displays Account info
            // ,8 is instead of "Account Number:         {AccountNumber} == "Pads" the accountnumber to the right by adding spaces to the left.
            Console.WriteLine($"Account Number: {accountNumber,8} \n" +
                $"Total Balance: {balance.Amount,8} {currency.AbbreviatedNameOfCurrency}\n");
        }

        public void showinfo()
        {
            foreach(var transaction in transactionslogged)
            {
                Console.WriteLine($"{transaction.FromUser},{transaction.ToUser}, {transaction.dateTime}, {transaction.Amount}");
            }
        }


        public bool Withdraw(decimal amountToWithdraw)
        {
            // Withdraw the amount from one account
            if (balance.Amount >= amountToWithdraw)
            {
                balance.Amount -= amountToWithdraw;
                return true;
            }
            else return false;
        }



        public void Deposit(decimal amountToDeposit)
        {
            // Deposit the amount ToAnother Account
            balance.Amount += amountToDeposit;
        }




    }

   


    




}
