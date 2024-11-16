using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.UI
{
    internal class AdminMenu : Menu
    {
        static private string _welcomeMessage = "";

        private ExchangeRateMenu _exchangeRateMenu;
        private CurrencyManager _currencyManager;
        private UserManager _userManager;

        private string _userName;

        public void Init(CurrencyManager currencyManager, UserManager userManager)
        {
            _exchangeRateMenu = new(currencyManager);
            _userManager = userManager;
            _currencyManager = currencyManager;
            option = 1;
            _welcomeMessage = "    Admin options\n";
        }

        public void ChangeWelcomeMessage(string value)
        {
            _welcomeMessage = value;
        }

        void ResetWelcomeMessage()
        {
            _welcomeMessage = "    Admin options\n";
            _userName = "";
        }

        public override eUIOptions Display()
        {
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine(_welcomeMessage);
            ResetWelcomeMessage();

            ConsoleKeyInfo key;
            bool isSelected = false;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            bool supportANSI = IsAnsiSupported();
            string color = supportANSI ? "\u001b[32m" : "";
            string resetColor = supportANSI ? "\u001b[0m" : "";


            Console.CursorVisible = false;

            while (!isSelected)
            {
                Console.CursorLeft = left;
                Console.CursorTop = top;

                if (supportANSI)
                {
                    Console.WriteLine($"{(option == 1 ? color : "        ")}Create User\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? color : "        ")}Update Curreny Exchange Rate:\u001b[0m");
                    Console.WriteLine($"{(option == 3 ? color : "        ")}Back to Main Menu\u001b[0m");
                }
                else
                {
                    Console.ForegroundColor = (option == 1) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 1 ? "        " : "        ")}Create User");

                    Console.ForegroundColor = (option == 2) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 2 ? "        " : "        ")}Update Currency Exchange Rate");

                    Console.ForegroundColor = (option == 3) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 3 ? "        " : "        ")}Back to Main Menu");

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
                    CreateUserMenu _createAccountMenu = new(_userManager, this);
                    nextState = _createAccountMenu.Display();
                    break;
                case 2:
                    _exchangeRateMenu.Display();
                    nextState = eUIOptions.AdminMenu;
                    break;
                case 3:
                    nextState = eUIOptions.MainMenu;
                    break;
            }
            return nextState;
        }
    }
}