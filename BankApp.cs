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
        Admin user;
        User currentUser;
        public BankApp()
        {
            //Setup
            _authManager = new();


        }

        public void Run()
        {
            Console.WriteLine("Välkommen till bankappen!"); //TODO ASCI THING ADD
            user = new Admin("admin", "password123", _authManager); //Auto login as Admin for test.
            Console.WriteLine("Auto Selecting Admin as user");
            user.CreateUser("Simon", "si");
            Console.WriteLine("Auto created user Simon with password si");
            Console.WriteLine("Logging out Admin");
            Console.WriteLine("Logging in as Simon");
            currentUser = _authManager._users.First(u => u.Username == "Simon");
            Console.WriteLine("Logged in as Simon");


            // Bank Menu
            Console.WriteLine("\n Bank Meny \n");

            // User creates account(s):

            AccountManager userAccounts = new AccountManager();
            userAccounts.AddAccount("A001", "SEK");
            userAccounts.AddSavingsAccount("A002", "USD");
            Console.WriteLine("Account 1 created, Acc no A001");
            Console.WriteLine("Savings Account 2 created, Acc no A002");
            

            var account1 = userAccounts.FindAccount("A001");
            account1.Deposit(1000m);
            Console.WriteLine("1000 SEK has been deposited to Account A001");

            Console.ReadLine();


            //    bool running = true;
            //    while (running)
            //    {
            //        Console.WriteLine("Välj ett alternativ:");
            //        Console.WriteLine("1. Logga in");
            //        Console.WriteLine("2. Avsluta");

            //        string input = Console.ReadLine();
            //        switch (input)
            //        {
            //            case "1":
            //                Login();
            //                break;
            //            case "2":
            //                running = false;
            //                break;
            //            default:
            //                Console.WriteLine("Felaktig inmatning, försök igen.");
            //                break;
            //        }
            //    }

            //}
        }
    }
}
