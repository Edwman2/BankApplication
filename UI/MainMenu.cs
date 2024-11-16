using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.UI
{
    internal class MainMenu : Menu
    {
        bool _noUsersExist = false;
        public override eUIOptions Display()
        {
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            if (_noUsersExist)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("    No user accounts exist, please login as an Admin to create Users.\n");
                Console.ResetColor();
                _noUsersExist = false;
            }
            else
                Console.WriteLine("    Main Menu\n");

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
                Console.CursorLeft = left; // Sets the cursor position here -> the 'cw'(text) will be overwritten.
                Console.CursorTop = top;

                if (supportANSI)
                {
                    Console.WriteLine($"{(option == 1 ? color : "    ")}User Login\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? color : "    ")}Admin Login\u001b[0m");
                    Console.WriteLine($"{(option == 3 ? color : "    ")}Exit\u001b[0m");
                }
                else
                {
                    //evalute (option == 1) if true sets foreground color to green, else white.
                    Console.ForegroundColor = (option == 1) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 1 ? "    " : "    ")}User Login");

                    Console.ForegroundColor = (option == 2) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 2 ? "    " : "    ")}Admin Login");

                    Console.ForegroundColor = (option == 3) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 3 ? "    " : "    ")}Exit");

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
                    nextState = eUIOptions.LoginMenu;
                    break;
                case 2:
                    nextState = eUIOptions.AdminMenu;
                    break;
                case 3:
                    nextState = eUIOptions.Exit;
                    break;
                default:
                    nextState = eUIOptions.NULL;
                    break;
            }

            return nextState;
        }

        public void EnableNoUsersExistMessage()
        {
            _noUsersExist = true;
        }
    }
}