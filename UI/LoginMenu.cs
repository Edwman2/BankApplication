using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.UI
{
    internal class LoginMenu : Menu
    {
        private bool _failedToLogin;
        private int _AttemptsLeft;
        private global::UserManager _userManager;
        private string _username;

        public LoginMenu(UserManager userManager)
        {
            _failedToLogin = false;
            _username = "";
            _userManager = userManager;
        }

        public override eUIOptions Display()
        {
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");

            if (_failedToLogin)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"    Wrong password or username. Attempts left: {_userManager.GetUsers().Find(u => u.Username == _username).FailedLoginAttempts}\n");
                _failedToLogin = false;
                Console.ResetColor();
            }
            else
                Console.WriteLine("    User Log In\n");

            ConsoleKeyInfo key;
            bool isSelected = false;
            bool supportANSI = IsAnsiSupported();

            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            string color = supportANSI ? "\u001b[32m" : "";
            string resetColor = supportANSI ? "\u001b[0m" : "";
            string username = _username;
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
                    Console.CursorLeft = 18;
                    Console.CursorTop = 7;
                    Console.CursorVisible = true;
                    username = Console.ReadLine();

                    _username = username;
                    if (_userManager.GetUsers().Find(u => u.Username == username).LockedAccount)
                    {
                        Console.WriteLine("You have failed to login 3 times, your account has been locked.");
                        Console.WriteLine("Press anywhere to continue...");
                        Console.ReadLine();
                        nextState = eUIOptions.MainMenu;
                        return nextState;
                    }
                    option++;
                    nextState = Display();
                    break;
                case 2://password check
                    if (username == string.Empty)//prevent input on password if username is not put in.
                        nextState = Display();
                    else
                    {
                        Console.CursorLeft = 18;
                        Console.CursorTop = 8;
                        Console.CursorVisible = true;
                        passwordHidden = string.Empty;

                        while (true)
                        {
                            key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.Enter) //Enter password
                            {
                                _username = username;
                                if (_userManager.Authenticate(username, passwordHidden))
                                {
                                    //Login Success
                                    _userManager.UpdateLoggedInUser(_userManager.GetUsers().Find(u => u.Username == username));
                                    return eUIOptions.UserMenu;
                                }
                                else
                                {
                                    _failedToLogin = true;
                                    _userManager.GetUsers().Find(u => u.Username == username).FailedLoginAttempts--;
                                    if (_userManager.GetUsers().Find(u => u.Username == username).FailedLoginAttempts <= 0)
                                    {
                                        _userManager.GetUsers().Find(u => u.Username == username).LockedAccount = true;
                                        if (_userManager.GetUsers().Find(u => u.Username == username).LockedAccount)
                                        {
                                            Console.WriteLine("You have failed to login 3 times, your account has been locked.");
                                            Console.WriteLine("Press anywhere to continue...");
                                            Console.ReadLine();
                                            nextState = eUIOptions.MainMenu;
                                            return nextState;
                                        }
                                    }
                                    nextState = Display();
                                }
                                break;
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
                        }
                    }
                    break;
                case 3:
                    _userManager.UpdateLoggedInUser(null);
                    nextState = eUIOptions.MainMenu;
                    break;
            }
            return nextState;
        }
    }
}
