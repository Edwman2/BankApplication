using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class BankApp
    {

        private AuthenticationManager _authManager;
        private CurrencyManager _currencyManager;
        private AccountManager _userAccounts;
        private LoanManager Lm1;
        TransactionManager ts; 
        Admin _user;
        User _currentUser;
        public BankApp()
        {
            //Setup
            _authManager = new();
            _currencyManager = new();
            _userAccounts = new();
            Lm1 = new();
            ts = new TransactionManager(_userAccounts, _currencyManager);

        }

        public void Run()
        {
            // User Manager adds a user, changes exchange rate, and Logs in as user.
            Console.WriteLine("Welcome to the BankApp!"); //TODO ASCI THING ADD
            _user = new Admin("admin", "password123", _authManager); //Auto login as Admin for test.
            Console.WriteLine("Auto Selecting Admin as user");
            _user.CreateUser("Simon", "si");
            Console.WriteLine("Auto created user Simon with password si");
            Console.WriteLine("Auto updating currency exchange rate");
            _currencyManager.UpdateExchangeRate("SEK", 1, true);
            Console.WriteLine("Logging out Admin");
            Console.WriteLine("Logging in as Simon");
            _currentUser = _authManager._users.First(u => u.Username == "Simon");
            Console.WriteLine("Logged in as Simon");
            Console.WriteLine("---------------");

            // Bank Menu
            Console.WriteLine("\n Bank Meny \n");

            // Account manager creates accounts
            _userAccounts.AddAccount("A001", "SEK");
            _userAccounts.AddSavingsAccount("A002", "USD");
            Console.WriteLine("Account 1 created, Acc no A001");
            Console.WriteLine("Savings Account 2 created, Acc no A002");
            
            // Deposits to account 1
            var account1 = _userAccounts.FindAccount("A001");
            account1.Deposit(1000m);

            Console.WriteLine("1000 SEK has been deposited to Account A001");

            //Loan manager function
            
            Lm1.ApplyForLoan(account1, 4500, account1.Balance, account1.Currency, 0.043m);

            // Transaction Manager
            ts.HandleUnprocessedTransactions();
            // Currency Manager acts on the side.
            ts.TransactionRequest("A001", "A002", 150);
            
            Console.WriteLine($"150 SEK has been transferred from Account A001 to Account A002");
            Console.WriteLine("---------------");

            Console.ReadLine();
        }
    }
}
