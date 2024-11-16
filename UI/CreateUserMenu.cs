using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.UI
{
    internal class CreateUserMenu : Menu
    {
        private UserManager _userManager;
        private AdminMenu _adminMenu;

        string _userName;
        string _password;

        bool _invalidUsername;
        bool _invalidPassword;
        bool _accountCreated;

        public CreateUserMenu(UserManager userManager, AdminMenu menu)
        {
            _invalidUsername = false;
            _invalidPassword = false;

            _adminMenu = menu;
            _userManager = userManager;

            _invalidUsername = false;
            _invalidPassword = false;
            _userName = string.Empty;
            _password = string.Empty;
        }

        public override eUIOptions Display()
        {
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");

            if (_invalidUsername)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"    Username is already in use\n");
                _invalidUsername = false;
                Console.ResetColor();
            }
            else if (_invalidPassword) //backlog says it should be unique as well? don't think it's necessary though.
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"    Password is already in use\n");
                _invalidPassword = false;
                Console.ResetColor();
            }
            else
                Console.WriteLine("    Create New User\n");

            ConsoleKeyInfo key;
            bool isSelected = false;
            bool supportANSI = IsAnsiSupported();

            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            string color = supportANSI ? "\u001b[32m" : "";
            string resetColor = supportANSI ? "\u001b[0m" : "";
            string username = _userName;
            string passwordHidden = "";

            Console.CursorVisible = false;

            while (!isSelected)
            {
                Console.CursorLeft = left;
                Console.CursorTop = top;

                if (supportANSI)
                {
                    Console.WriteLine($"{(option == 1 ? color : "        ")}Username: {username}\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? color : "        ")}Password: {passwordHidden}\u001b[0m");
                    Console.WriteLine($"{(option == 3 ? color : "        ")}Back to Menu\u001b[0m");
                }
                else
                {
                    Console.ForegroundColor = (option == 1) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 1 ? "        " : "        ")}Username: {username}");

                    Console.ForegroundColor = (option == 2) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 2 ? "        " : "        ")}Password: {passwordHidden}");

                    Console.ForegroundColor = (option == 3) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 3 ? "        " : "        ")}Back to Menu");

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
                    //if (username != string.Empty)
                    //{
                    //    username = string.Empty;
                    //    Display();
                    //}
                    //else
                    //{
                    Console.CursorLeft = 18;
                    Console.CursorTop = 7;
                    Console.CursorVisible = true;
                    username = Console.ReadLine();

                    if (_userManager.UsernameValid(username))//make sure it unique, required by backlog.
                    {
                        _userName = username;
                        option++;
                        Display();//starts the password check, recursive

                        //problm here is the current logic is recursive so once the username is confirmed it goes after the password,
                        //put when it's done with the password it returns here, quick fix is checking if user values arn't empty.
                        //So this if() will be triggered once it has returned to the first time being called, and then it should have 
                        //a username and a password
                        if (_userName != string.Empty && _password != string.Empty)
                        {
                            _userManager.AddUser(new global::User(_userName, _password));

                            Console.WriteLine("New user created!");
                            _adminMenu.ChangeWelcomeMessage("    Account Was Created!\n");
                            nextState = eUIOptions.AdminMenu;
                            break;
                        }
                    }
                    _invalidUsername = true;
                    Display();
                    //}
                    break;
                case 2://password check
                       //if (username == string.Empty)//prevent input on password if username is not put in.
                       //    Display();
                       //else
                       //{
                    Console.CursorLeft = 18;
                    Console.CursorTop = 8;
                    Console.CursorVisible = true;
                    passwordHidden = string.Empty;

                    while (true)
                    {
                        key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter) //Enter password
                        {
                            if (_userManager.PasswordValid(passwordHidden))
                            {
                                _password = passwordHidden;
                                return eUIOptions.AdminMenu;
                            }
                            else
                            {
                                _invalidPassword = true;
                                Display();
                            }
                        }
                        else if (key.Key == ConsoleKey.Backspace && passwordHidden.Length > 0) //Handle backspace
                        {
                            passwordHidden = passwordHidden.Substring(0, passwordHidden.Length - 1); //Remove last character overrite it with space
                            Console.CursorLeft--;
                            Console.Write(" ");
                            Console.CursorLeft--;
                            continue;
                        }
                        passwordHidden += key.KeyChar;
                        Console.Write("*");
                        //}
                    }
                    break;
                case 3:
                    nextState = eUIOptions.MainMenu;
                    break;
            }
            return nextState;
        }
    }
}
