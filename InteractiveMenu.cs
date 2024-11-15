using System;

namespace BankApplication
{
    public class InteractiveMenu
    {
        /* Trust Executive Bank AB

                User Log In
                Create a new Account
                Admin Log In

            ----------------
            Trust Executive Bank AB

                User Log In

                    Username: Jonas1337
                    Password:
                    Back to Menu

        */

        // ------- Instance call from BankApp
        // InteractiveMenu newLogin = new InteractiveMenu();
        // newLogin.Display();
        // Console.ReadLine();


        private int option = 1;
        private string username = string.Empty;
        private string passwordHidden = string.Empty;
        private Logo Logo = new Logo();

        public void Display()
        {
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n"); // ANSI/ASCII Logo?


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
                    Console.WriteLine($"{(option == 1 ? color : "    ")}User Log In\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? color : "    ")}Create a new Account\u001b[0m");
                    Console.WriteLine($"{(option == 3 ? color : "    ")}Admin Log In\u001b[0m");
                }
                else
                {
                    //evalute (option == 1) if true sets foreground color to green, else white.
                    Console.ForegroundColor = (option == 1) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 1 ? "    " : "    ")}User Log In");

                    Console.ForegroundColor = (option == 2) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 2 ? "    " : "    ")}Create a new Account");

                    Console.ForegroundColor = (option == 3) ? ConsoleColor.Green : ConsoleColor.White;
                    Console.WriteLine($"{(option == 3 ? "    " : "    ")}Admin Log In");

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

        private void UserLogInMenu()
        {
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine("    User Log In\n");

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
                    // username check?
                    // isUsername? else -> create new Account?
                    if (username != string.Empty)
                    {
                        username = string.Empty;
                        UserLogInMenu();
                    }
                    else
                    {
                        Console.CursorLeft = 18;
                        Console.CursorTop = 7;
                        Console.CursorVisible = true;
                        username = Console.ReadLine();
                        option++;
                        UserLogInMenu();
                    }
                    break;
                case 2:
                    // password check?
                    if (username == string.Empty) UserLogInMenu();
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
                    username = string.Empty;
                    Display();
                    break;
            }
            Console.WriteLine($"\n\nUsername: {username}\nPassword: {passwordHidden}");
            Console.ReadLine();
        }
        private void CreateNewAccount()
        {
            // Is this an admin thing or should a new user be able to create an account from here?
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine("    Create a new Account\n");
        }
        private void AdminLogInMenu()
        {
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine("    Admin Log In\n");
        }

        // Check if the terminal supports ANSI escape sequences, Ty chatgpt
        private bool IsAnsiSupported()
        {
            // Detecting Windows with terminal support (e.g., PowerShell 7+ or Windows Terminal)
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // PowerShell 7+ and Windows Terminal support ANSI codes
                return Console.IsOutputRedirected == false && (Environment.GetEnvironmentVariable("TERM") != null);
            }

            // On Unix-based systems (Linux/macOS), ANSI codes are typically supported
            return true;
        }
    }
}
