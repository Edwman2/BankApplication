using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace BankApplication
{
    internal class AccountManager
    {
        private List<Account> accounts { get; set; }

        public AccountManager()
        {
            accounts = new List<Account>();
        }
        // Display User's accounts
        public void DisplayAccounts()
        {
            foreach (Account account in accounts)
            {
                account.DisplayInfo();
            }
        }


        public bool AddAccount(string accountNumber, string currency)
        {
            accounts.Add(new Account(accountNumber, new Balance(0.00m), new Currency(currency)));
            return true;
        }

        public bool AddSavingsAccount(string accountNumber, string currency)
        {
            accounts.Add(new SavingsAccount(accountNumber, new Balance(0.00m), new Currency(currency)));
            return true;
        }

        public Account FindAccount(string accountsNumber)
        {
            Account foundAccount = accounts.Find(a => a.AccountNumber == accountsNumber);

            return foundAccount;
        }
        public void LogTransaction(TransactionLog log)
        {
            Account fromAccount = accounts.Find(a => a.AccountNumber == log.FromUser);
            Account toAccount = accounts.Find(a => a.AccountNumber == log.ToUser);
            if (fromAccount != null && toAccount != null)
            {
                fromAccount.TransactionHistory.Add(log);
                toAccount.TransactionHistory.Add(log);
            }
            else
            {
                Console.WriteLine("Feck off!"); // TODO - What is going to happen if one is valid 
                // and the other isn't? Should it be logged anyway?
            }

        }
        // Prints the transaction history for the money account the user has specified.
        // UNTESTED*
        // _userAccounts.ShowTransactionHistory("A003");
        // ChosenAccount blev null.
        public void ShowTransactionHistory(string accountNumber)
        {
            Account ChosenAccount = accounts.Find(a => a.AccountNumber == accountNumber);
            Console.WriteLine($"You have: {ChosenAccount.TransactionHistory.Count} transactions ");
            foreach (TransactionLog transaction in ChosenAccount.TransactionHistory)
            {
                Console.WriteLine(transaction);
            }
        }

        // WORK IN PROGRESS

        //public bool DeleteAccount(string accountNumber)
        //{
            
        //    var deleteAccount = accounts.Find(a => a.AccountNumber == accountNumber);
        //    if (deleteAccount == null) return false;
        //    else if (deleteAccount.)
                
        //        accounts.Remove(deleteAccount);
        //    return false;
        //}
    }
}
