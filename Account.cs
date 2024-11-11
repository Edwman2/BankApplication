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
        //public enum AccountType
        //{
        //    Debit,
        //    Saving,
        //    Loan
        //}
        public string AccountNumber { get; } // TODO - Maybe change to GUID? public Guid AccNo ... AccNo = Guid.NewGuid();
        public decimal Balance { get;  set; } 

        public decimal Amount { get; set; }
        public Currency Currency { get; } 

        public Account(string accountNumber, decimal initialBalance /*Currency currency*/)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
            
            //Currency = currency;
        }

        List<TransactionLog> transactionslogged = new List<TransactionLog>();


        

        public void AddTransactionLog(TransactionLog transaction)
        {
            transactionslogged.Add(transaction);
        }

        public void showinfo()
        {
            foreach(var transaction in transactionslogged)
            {

                /*await Task.Delay(11100);*/ Console.WriteLine($"{transaction.FromUser},{transaction.ToUser} {transaction.dateTime}, {transaction.Amount}");
            }
        }



        






        public void Withdraw(decimal amountToWithdraw)
        {
            if (Balance >= amountToWithdraw)
            {
                Balance -= amountToWithdraw;
                Console.WriteLine("Money was withdrawn successfully!");
            }
            else

                Console.WriteLine("Not enough money in your account");

        }



        public void Deposit(decimal amountToDeposit)
        {
            // Deposit the amount
            Balance += amountToDeposit;
            /*await Task.Delay(11000);*/ Console.WriteLine("Money was deposited successfully!");
            //DisplayInfo();
        }





    }

    // --------------- Methods for Account related functionality ---------------


    //public void DisplayInfo()
    //{
    //    // Displays Account info
    //    Console.WriteLine($"Account Number: {AccountNumber,8} \n" +
    //        $"Total Balance: {Balance,8} {Currency.AbbreviatedNameOfCurrency}\n");
    //}


    




}
