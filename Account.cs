using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
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

        // Properties:
        // TODO - Account type?
        //public enum AccountType
        //{
        //    Debit,
        //    Saving,
        //    Loan
        //}
        public List<TransactionLog> TransactionHistory { get; set; }
        public string AccountNumber { get; private set; } // TODO - Maybe change to GUID? public Guid AccNo ... AccNo = Guid.NewGuid();
        public Balance Balance { get; private set; }
        public Currency Currency { get; } 

        public Account(string accountNumberInput, Balance initialBalance, Currency currencyInput)
        {
            TransactionHistory = new List<TransactionLog>();
            AccountNumber = accountNumberInput;
            Balance = initialBalance;
            Currency = currencyInput;
        }

        
        public bool Withdraw(decimal amountToWithdraw)
        {
            // Withdraw the amount from one account
            if (amountToWithdraw > Balance.Amount) 
                return false;

            Balance.Amount -= amountToWithdraw;
            return true;
        }

        public void DisplayInfo()
        {
            // Displays Account info
            Console.WriteLine($"Account Number: {AccountNumber,8} \n" +
                $"Total Balance: {Balance,8} {Currency.AbbreviatedNameOfCurrency}\n");

        }

        public void Deposit(decimal amountToDeposit)
        {
            // Deposit the amount ToAnother Account
            Balance.Amount += amountToDeposit;
        }

    }

   
}
