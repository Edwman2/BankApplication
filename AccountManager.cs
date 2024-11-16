using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace BankApplication
{
    internal class AccountManager
    {
        static HashSet<int> accountNumbers = new HashSet<int>(); //used to give accounts unique ID's
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

        public List<Account> GetAccounts()
        {
            return accounts;
        }

        public bool AddAccount(string currency)
        {
            accounts.Add(new Account(GenerateAccountNumber().ToString(), new Balance(0.00m), new Currency(currency)));
            return true;
        }

        private int GenerateAccountNumber()
        {
            int nmr = 0;
            Random rnd = new();
            do
            {
                nmr = rnd.Next();

            } while (accountNumbers.Contains(nmr));

            return nmr;
        }

        public bool AddSavingsAccount(string currency)
        {
            accounts.Add(new SavingsAccount(GenerateAccountNumber().ToString(), new Balance(0.00m), new Currency(currency)));
            return true;
        }

        // Finds a specified account in the User's list
        public Account FindAccount(string accountsNumber)
        {
            Account foundAccount = accounts.Find(a => a.AccountNumber == accountsNumber);

            return foundAccount;
        }
        public void LogTransaction(TransactionLog log, string owner)
        {
            //Account fromAccount = accounts.Find(a => a.AccountNumber == log.FromUser);
            //Account toAccount = accounts.Find(a => a.AccountNumber == log.ToUser);

            //if (fromAccount != null && toAccount != null)
            //{
            //    fromAccount.TransactionHistory.Add(log);
            //    toAccount.TransactionHistory.Add(log);
            //}
            //else
            //{
            //    Console.WriteLine("Feck off!"); // TODO - What is going to happen if one is valid 
            //    // and the other isn't? Should it be logged anyway?
            //}

            Account acc = accounts.Find(a => a.AccountNumber == owner);
            acc?.TransactionHistory.Add(log);

        }
        // Prints the transaction history for the money account the user has specified.
        public void ShowTransactionHistory(string accountNumber)
        {
            Account ChosenAccount = accounts.Find(a => a.AccountNumber == accountNumber);
            foreach (TransactionLog transaction in ChosenAccount.TransactionHistory)
            {
                Console.WriteLine(
                    $"| From: {transaction.FromUser} " +
                    $"| To: {transaction.ToUser} " +
                    $"| Anmount: {transaction.Amount}");
                Console.WriteLine("");
                Console.WriteLine(
                    $"| Requested: {transaction.dateTimeRequested} " +
                    $"| Completed: {transaction.dateTimeCompleted} ");
                Console.WriteLine("");


                Console.Write("| Status:");
                if (transaction.status == "Failed")
                    Console.ForegroundColor = ConsoleColor.Red;  
                else if (transaction.status == "Success")
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.White;
                Console.Write(transaction.status);
                Console.ResetColor();
                Console.WriteLine($"| Error Message: {transaction.ErrorMessage}");

                // Reset color back to default after printing status
                Console.ResetColor();
                Console.WriteLine("--------------------------------------");
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
