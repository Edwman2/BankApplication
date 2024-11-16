using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankApplication.UI
{
    internal class CreateAccountMenu : Menu
    {
        private CurrencyManager _currencyManager;
        private UserManager _userManager;

        public CreateAccountMenu(CurrencyManager currencyManager, UserManager userManager)
        {
            _currencyManager = currencyManager;
            _userManager = userManager;
        }

        public override eUIOptions Display()
        {
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine("    Choose The Type Of Account You Want To Add\n");

            ConsoleKeyInfo key;
            bool isSelected = false;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            bool supportANSI = IsAnsiSupported();
            string color = supportANSI ? "\u001b[32m" : "";
            string resetColor = supportANSI ? "\u001b[0m" : "";

            Console.CursorVisible = false;
            int option = 1;

            while (!isSelected)
            {
                Console.CursorLeft = left;
                Console.CursorTop = top;

                if (supportANSI)
                {
                    // Display options with ANSI support
                    Console.WriteLine($"{(option == 1 ? color : "    ")}Create Account\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? color : "    ")}Create SacingsAccount\u001b[0m");
                }
                else
                {
                    // Display options without ANSI support
                    Console.ForegroundColor = (option == 1) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 1 ? "    " : "    ")}Create Account");

                    Console.ForegroundColor = (option == 2) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 2 ? "    " : "    ")}Create SavingsAccount");
                    Console.ResetColor();
                }

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == 2) ? 1 : option + 1;
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 1) ? 2 : option - 1;
                        break;
                    case ConsoleKey.Enter:
                        CreateAccount(option == 2);
                        isSelected = true;
                        break;
                }
            }
            nextState = eUIOptions.UserMenu;
            return nextState;
        }

        private void CreateAccount(bool isSavings)
        {
            var temp = _currencyManager.GetExchangeRates();

            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine("    Which Currency Would You Like?\n");

            ConsoleKeyInfo key;
            bool isSelected = false;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            bool supportANSI = IsAnsiSupported();
            string color = supportANSI ? "\u001b[32m" : "";
            string resetColor = supportANSI ? "\u001b[0m" : "";

            Console.CursorVisible = false;
            int option = 1;
            KeyValuePair<string, decimal> selectedItem = new();

            while (!isSelected)
            {
                Console.CursorLeft = left;
                Console.CursorTop = top;

                if (supportANSI)
                {
                    int i = 0;
                    foreach (var item in temp)
                    {
                        if (option == i + 1)
                            Console.WriteLine($"{color}             {item.Key}\u001b[0m");
                        else
                            Console.WriteLine($"              {item.Key}");
                        i++;
                    }
                    Console.ResetColor();

                }
                else
                {
                    int i = 0;
                    foreach (var item in temp)
                    {
                        if (option == i + 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"             {item.Key}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"            {item.Key}");
                        }
                        i++;
                    }
                    Console.ResetColor();
                }

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == temp.Count) ? 1 : option + 1;
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 1) ? temp.Count : option - 1;
                        break;
                    case ConsoleKey.Enter:
                        selectedItem = temp.ElementAt(option - 1);
                        isSelected = true;
                        break;
                }
            }
            if (isSavings)
            {
                _userManager.GetLoggedInUser().AccountManager.AddAccount(selectedItem.Key.ToString());
            }
            else
            {
                _userManager.GetLoggedInUser().AccountManager.AddSavingsAccount(selectedItem.Key.ToString());
            }
        }
    }
}
