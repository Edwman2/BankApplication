using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public static class UserInteractiveMenu
    {
        private static int option = 1;
        
        public static void Display()
        {
            Console.Clear();
            Console.WriteLine("Trust Executive Bank AB\n");
            
            
            ConsoleKeyInfo key;
            bool isSelected = false;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            string color = "    \u001b[32m";
            Console.CursorVisible = false;


            while (!isSelected)
            {
                Console.CursorLeft = left;
                Console.CursorTop = top;
                
                Console.WriteLine($"{(option == 1 ? color : "    ")}Log In\u001b[0m");
                Console.WriteLine($"{(option == 2 ? color : "    ")}Create a new Account\u001b[0m");
                Console.WriteLine($"{(option == 3 ? color : "    ")}Admin Log In\u001b[0m");

                key = Console.ReadKey(true);
                
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == 3 ? 1 : option + 1 );
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
                    UserLogInMenu();
                    break;
                case 2:
                    CreateNewAccount();
                    break;
                case 3:
                    AdminLogInMenu();
                    break;
            }
        }
        private static void UserLogInMenu()
        {
            Console.Clear();
            Console.WriteLine("Trust Executive Bank AB\n");
            Console.WriteLine("    Log In\n");

            ConsoleKeyInfo key;
            bool isSelected = false;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            string color = "        \u001b[32m";
            Console.CursorVisible = false;
            string username = string.Empty;
            string passwordHidden = string.Empty;


            while (!isSelected)
            {
                Console.CursorLeft = left;
                Console.CursorTop = top;

                Console.WriteLine($"{(option == 1 ? color : "        ")}Username: {username}\u001b[0m");
                Console.WriteLine($"{(option == 2 ? color : "        ")}Password: {passwordHidden}\u001b[0m");
                Console.WriteLine($"{(option == 3 ? color : "        ")}Back to Menu\u001b[0m");

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
                    // username check?
                    break;
                case 2:
                    // password check?
                    Console.CursorLeft = 18;
                    Console.CursorTop = 5;
                    Console.CursorVisible = true;
                    ConsoleKey key2;
                    do
                    {
                        var keyInfo = Console.ReadKey(true);
                        key2 = keyInfo.Key;

                        if (key2 != ConsoleKey.Enter)
                        {
                            passwordHidden += keyInfo.KeyChar;
                            Console.Write("*"); // Display a placeholder character
                        }
                    } while (key2 != ConsoleKey.Enter);
                    break;
                case 3:
                    Display();
                    break;
            }

        }
        private static void CreateNewAccount()
        {
            Console.Clear();
            Console.WriteLine("Trust Executive Bank AB\n");
            Console.WriteLine("    Create a new Account\n");
        }
        private static void AdminLogInMenu()
        {
            Console.Clear();
            Console.WriteLine("Trust Executive Bank AB\n");
            Console.WriteLine("    Log In\n");
        }

    }
}
