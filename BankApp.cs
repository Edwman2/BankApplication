using BankApplication.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static BankApplication.UI.Menu;
using BankApplication;
using System.Net;

namespace BankApplication
{
    internal class BankApp
    {
        private Admin _admin;

        //Managers
        private CurrencyManager _currencyManager;
        private TransactionManager _transactionManager;
        private AccountManager _accountManager;
        private UserManager _userManager;

        //Menus
        private MainMenu _mainMenu;
        private LoginMenu _loginMenu;
        private AdminMenu _adminMenu;
        private UserMenu _userMenu;

        public BankApp()
        {
            //Setup
            _currencyManager = new();
            _accountManager = new();
            _userManager = new();
            _adminMenu = new();
            _mainMenu = new();

            _transactionManager = new(_currencyManager, _userManager);
            _userMenu = new(_currencyManager, _userManager, _transactionManager);

            _adminMenu.Init(_currencyManager, _userManager);

            _userManager.AddUser(new User("admin", "password123"));
            _transactionManager.HandleUnprocessedTransactions();
        }

        public void Run()
        {
            eUIOptions state = eUIOptions.MainMenu;
            state = _mainMenu.Display();
            bool exit = false;

            while (!exit)
            {
                switch (state)
                {
                    case eUIOptions.NULL:
                        break;
                    case eUIOptions.LoginMenu:
                        if (_userManager.GetUsers().Count <= 1)
                        {
                            state = eUIOptions.MainMenu;
                            _mainMenu.EnableNoUsersExistMessage();
                        }
                        else
                        {
                            _loginMenu = new(_userManager);
                            state = _loginMenu.Display();
                        }
                        break;
                    case eUIOptions.MainMenu:
                        //user = new("", "");//logout here as well, i'll "delete" for now.
                        state = _mainMenu.Display();
                        break;
                    case eUIOptions.AdminMenu:
                        state = _adminMenu.Display();
                        break;
                    case eUIOptions.UserMenu:
                        state = _userMenu.Display();
                        break;
                    case eUIOptions.Exit:
                        exit = true;
                        break;
                }
            }
        }

        //public void Run()
        //{
        //    Console.WriteLine("Välkommen till bankappen!"); //TODO ASCI THING ADD
        //    _user = new Admin("admin", "password123", _authManager); //Auto login as Admin for test.
        //    Console.WriteLine("Auto Selecting Admin as user");
        //    _user.CreateUser("Simon", "si");
        //    Console.WriteLine("Auto created user Simon with password si");
        //    Console.WriteLine("Auto updating currency exchange rate");
        //    _currencyManager.UpdateExchangeRate("SEK", 1, true);
        //    Console.WriteLine("Logging out Admin");
        //    Console.WriteLine("Logging in as Simon");
        //    _currentUser = _authManager._users.First(u => u.Username == "Simon");
        //    Console.WriteLine("Logged in as Simon");
        //    Console.WriteLine("---------------");

        //    // Bank Menu
        //    Console.WriteLine("\n Bank Meny \n");

        //    AccountManager userAccounts = new AccountManager();
        //    userAccounts.AddAccount("A001", "SEK");
        //    userAccounts.AddSavingsAccount("A002", "USD");
        //    Console.WriteLine("Account 1 created, Acc no A001");
        //    Console.WriteLine("Savings Account 2 created, Acc no A002");


        //    var account1 = userAccounts.FindAccount("A001");
        //    account1.Deposit(1000m);
        //    Console.WriteLine("1000 SEK has been deposited to Account A001");

        //    //Loan manager function
        //    LoanManager Lm1 = new LoanManager();
        //    Lm1.ApplyForLoan(account1, 4500, account1.Balance, account1.Currency, 0.043m);

        //    TransactionManager ts = new TransactionManager(userAccounts, _currencyManager);
        //    ts.HandleUnprocessedTransactions();
        //    ts.TransactionRequest("A001", "A002", 150);

        //    Console.WriteLine($"150 SEK has been transferred from Account A001 to Account A002");
        //    Console.WriteLine("---------------");

        //    Console.ReadLine();
        //}
    }
}
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;

//namespace BankApplication
//{
//    internal class BankApp
//    {

//        private AuthenticationManager _authManager;
//        private CurrencyManager _currencyManager;
//        private AccountManager _userAccounts;
//        private LoanManager Lm1;
//        TransactionManager ts;
//        Admin _user;
//        User _currentUser;
//        public BankApp()
//        {
//            //Setup
//            _authManager = new();
//            _currencyManager = new();
//            _userAccounts = new();
//            Lm1 = new();
//            ts = new TransactionManager(_userAccounts, _currencyManager);

//        }

//        public void Run()
//        {
//            // User Manager adds a user, changes exchange rate, and Logs in as user.
//            Console.WriteLine("Welcome to the BankApp!"); //TODO ASCI THING ADD
//            _user = new Admin("admin", "password123", _authManager); //Auto login as Admin for test.
//            Console.WriteLine("Auto Selecting Admin as user");
//            _user.CreateUser("Simon", "si");
//            Console.WriteLine("Auto created user Simon with password si");
//            Console.WriteLine("Auto updating currency exchange rate");
//            _currencyManager.UpdateExchangeRate("SEK", 1, true);
//            Console.WriteLine("Logging out Admin");
//            Console.WriteLine("Logging in as Simon");
//            _currentUser = _authManager._users.First(u => u.Username == "Simon");
//            Console.WriteLine("Logged in as Simon");
//            Console.WriteLine("---------------");

//            // Bank Menu
//            Console.WriteLine("\n Bank Meny \n");

//            // Account manager creates accounts
//            _userAccounts.AddAccount("A001", "SEK");
//            _userAccounts.AddSavingsAccount("A002", "USD");
//            Console.WriteLine("Account 1 created, Acc no A001");
//            Console.WriteLine("Savings Account 2 created, Acc no A002");

//            // Deposits to account 1
//            var account1 = _userAccounts.FindAccount("A001");
//            account1.Deposit(1000m);

//            Console.WriteLine("1000 SEK has been deposited to Account A001");

//            //Loan manager function

//            Lm1.ApplyForLoan(account1, 4500, account1.Balance, account1.Currency, 0.043m);

//            // Transaction Manager
//            ts.HandleUnprocessedTransactions();
//            // Currency Manager acts on the side.
//            ts.TransactionRequest("A001", "A002", 150);

//            Console.WriteLine($"150 SEK has been transferred from Account A001 to Account A002");
//            Console.WriteLine("---------------");

//            Console.ReadLine();
//        }
//    }
//}

