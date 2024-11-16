using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.UI
{
    internal class TransferFundsMenu : Menu
    {
        global::User _user;
        CurrencyManager _currencyManager;
        UserManager _userManager;

        TransferBetweenUsersMenu _transferBetweenUsersMenu;
        TransferBetweenAccountsMenu _transferBetweenAccountsMenu;

        public TransferFundsMenu(CurrencyManager currencyManager, UserManager userManager, TransactionManager transactionManager)
        {
            _currencyManager = currencyManager;
            _userManager = userManager;
            _transferBetweenUsersMenu = new(_userManager, transactionManager);
            _transferBetweenAccountsMenu = new(_userManager, transactionManager);
        }

        public override eUIOptions Display()
        {
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine("    Transfer Options\n");

            ConsoleKeyInfo key;
            
            bool isSelected = false;
            bool supportANSI = IsAnsiSupported();
            
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            string color = supportANSI ? "\u001b[32m" : "";
            string resetColor = supportANSI ? "\u001b[0m" : "";

            Console.CursorVisible = false;

            while (!isSelected)
            {
                Console.CursorLeft = left;
                Console.CursorTop = top;

                if (supportANSI)
                {
                    Console.WriteLine($"{(option == 1 ? color : "    ")}Transfer Between My Own Accounts\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? color : "    ")}Transfer Between Different Users\u001b[0m");
                    Console.WriteLine($"{(option == 3 ? color : "    ")}Return\u001b[0m");
                }
                else
                {
                    Console.ForegroundColor = (option == 1) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 1 ? "    " : "    ")}Transfer Between My Own Accounts");

                    Console.ForegroundColor = (option == 2) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 2 ? "    " : "    ")}Transfer Between Different Users");

                    Console.ForegroundColor = (option == 3) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 3 ? "    " : "    ")}Return");

                    Console.ResetColor();
                }

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == 3 ? 1 : option + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 1 ? 3 : option - 1);
                        break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }

            switch (option)
            {
                case 1:
                    nextState = _transferBetweenAccountsMenu.Display();
                    break;
                case 2:
                    nextState = _transferBetweenUsersMenu.Display();
                    break;
                case 3:
                    return eUIOptions.UserMenu;
            }

            return nextState;
        }
    }
}