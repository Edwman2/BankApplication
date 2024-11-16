using System;
using System.Collections.Generic;

namespace BankApplication.UI
{
    internal class UserMenu : Menu
    {
        private TransferFundsMenu _transferFundsMenu;
        private CreateAccountMenu _createAccountMenu;
        private UserManager _userManager;
        private DepositeMoneyMenu _depostieMoney;

        public UserMenu(CurrencyManager currencyManager, UserManager userManager, TransactionManager transactionManager)
        {
            _transferFundsMenu = new(currencyManager, userManager, transactionManager);
            _createAccountMenu = new(currencyManager, userManager);
            _depostieMoney = new(userManager);
            _userManager = userManager;
        }

        public override eUIOptions Display()
        {
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine("    User Menu\n");

            ConsoleKeyInfo key;
            bool isSelected = false;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            bool supportANSI = IsAnsiSupported();
            string color = supportANSI ? "\u001b[32m" : "";
            string resetColor = supportANSI ? "\u001b[0m" : "";

            // Track the current menu option
            int option = 1;
            int totalOptions = 6; // The total number of options

            Console.CursorVisible = false;

            while (!isSelected)
            {
                Console.CursorLeft = left;
                Console.CursorTop = top;

                if (supportANSI)
                {
                    // Display options with ANSI support
                    Console.WriteLine($"{(option == 1 ? color : "    ")}View Active Accounts\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? color : "    ")}Transfer Funds\u001b[0m");
                    Console.WriteLine($"{(option == 3 ? color : "    ")}View Transaction History\u001b[0m");
                    Console.WriteLine($"{(option == 4 ? color : "    ")}Create Account\u001b[0m");
                    Console.WriteLine($"{(option == 5 ? color : "    ")}Create Savings Account\u001b[0m");
                    Console.WriteLine($"{(option == 6 ? color : "    ")}Logout\u001b[0m");
                }
                else
                {
                    // Display options without ANSI support
                    Console.ForegroundColor = (option == 1) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 1 ? "    " : "    ")}View Active Accounts");

                    Console.ForegroundColor = (option == 2) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 2 ? "    " : "    ")}Transfer Funds");

                    Console.ForegroundColor = (option == 3) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 3 ? "    " : "    ")}View Transaction History");

                    Console.ForegroundColor = (option == 4) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 4 ? "    " : "    ")}Create Account");

                    Console.ForegroundColor = (option == 5) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 5 ? "    " : "    ")}Deposit Money");

                    Console.ForegroundColor = (option == 6) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 6 ? "    " : "    ")}Logout");

                    Console.ResetColor();
                }

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == totalOptions) ? 1 : option + 1; // Wrap around to the first option
                        break;

                    case ConsoleKey.UpArrow:
                        option = (option == 1) ? totalOptions : option - 1; // Wrap around to the last option
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true; // Exit the loop when Enter is pressed
                        break;
                }
            }

            // Handle the selected option based on the current option number
            switch (option)
            {
                case 1: // View active accounts
                    _userManager.GetLoggedInUser().AccountManager.DisplayAccounts();
                    Console.WriteLine("Press Enter to Return");
                    Console.ReadLine();
                    Console.Clear();
                    nextState = eUIOptions.UserMenu;
                    break;
                case 2: // Transfer funds
                    nextState = _transferFundsMenu.Display();
                    break;
                case 3: // View transaction history
                    ViewTransactions();
                    nextState = eUIOptions.UserMenu;
                    break;

                case 4: // Create account
                    nextState = _createAccountMenu.Display();
                    break;

                case 5: // Deposite Money
                    nextState = _depostieMoney.Display();
                    break;

                case 6: // Logout
                    nextState = eUIOptions.MainMenu; // Go to main menu
                    break;
            }
            return nextState;
        }

        void ViewTransactions()
        {
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine("    Transaction History\n");

            foreach (var item in _userManager.GetLoggedInUser().AccountManager.GetAccounts())
                _userManager.GetLoggedInUser().AccountManager.ShowTransactionHistory(item.AccountNumber);
            
            Console.WriteLine("\n    Press Enter to Return");
            Console.ReadLine();
            Console.Clear();
        }

    }
}