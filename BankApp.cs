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
        Admin _user;
        User _currentUser;
        public BankApp()
        {
            //Setup
            _authManager = new();
            _currencyManager = new();

        }

        public void Run()
        {
            Console.WriteLine("Välkommen till bankappen!"); //TODO ASCI THING ADD
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

            AccountManager userAccounts = new AccountManager();
            userAccounts.AddAccount("A001", "SEK");
            userAccounts.AddSavingsAccount("A002", "USD");
            Console.WriteLine("Account 1 created, Acc no A001");
            Console.WriteLine("Savings Account 2 created, Acc no A002");
            

            var account1 = userAccounts.FindAccount("A001");
            account1.Deposit(1000m);
            Console.WriteLine("1000 SEK has been deposited to Account A001");

            //Loan manager function
            LoanManager Lm1 = new LoanManager();
            Lm1.ApplyForLoan(account1, 4500, account1.Balance, account1.Currency, 0.043m);

            TransactionManager ts = new TransactionManager(userAccounts, _currencyManager);
            ts.HandleUnprocessedTransactions();
            ts.TransactionRequest("A001", "A002", 150);
            
            Console.WriteLine($"150 SEK has been transferred from Account A001 to Account A002");
            Console.WriteLine("---------------");

            Console.ReadLine();
        }
    }
}
