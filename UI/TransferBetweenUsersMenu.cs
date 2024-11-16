using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.UI
{
    internal class TransferBetweenUsersMenu : Menu
    {
        private UserManager _userManager;
        private TransactionManager _transactionManager;

        public TransferBetweenUsersMenu(UserManager userManager, TransactionManager transactionManager)
        {
            _userManager = userManager;
            _transactionManager = transactionManager;
        }

        public override eUIOptions Display()
        {
            List<global::User> temp = _userManager.GetUsers();

            Account userAccount = SelectFromAccount(_userManager.GetLoggedInUser(), "your");
            Account targetAccount = null;

            Console.Clear();
            Console.ResetColor();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine("    Who Would You Like To Transfer To?\n");

            ConsoleKeyInfo key;
            bool isSelected = false;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            bool supportANSI = IsAnsiSupported();
            string color = supportANSI ? "\u001b[32m" : "";
            string resetColor = supportANSI ? "\u001b[0m" : "";

            Console.CursorVisible = false;
            int option = 0;
            global::User selectedItem;

            while (!isSelected)
            {
                Console.CursorLeft = left;
                Console.CursorTop = top;

                // Display the list of users, highlighting the selected option
                int i = 1; // Used for tracking the position in the filtered list
                foreach (var item in temp)
                {
                    // Skip users that do not meet the condition
                    if (item.Username == _userManager.GetLoggedInUser().Username || item.Username == "admin")
                        continue;

                    // Highlight the selected item
                    if (option == i)
                    {
                        if (supportANSI)
                            Console.WriteLine($"{color}             {item.Username}\u001b[0m");
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"             {item.Username}");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        if (supportANSI)
                            Console.WriteLine($"             {item.Username}");
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"             {item.Username}");
                            Console.ResetColor();
                        }
                    }
                    i++; // Increment the filtered list index
                }

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        // Move down in the filtered list
                        if (option < i - 1)
                            option++;
                        else
                            option = 1; // Loop back to the first option
                        break;

                    case ConsoleKey.UpArrow:
                        // Move up in the filtered list
                        if (option > 1)
                            option--;
                        else
                            option = i - 1; // Loop back to the last option
                        break;

                    case ConsoleKey.Enter:
                        // Find the user corresponding to the selected option
                        i = 1; // Reset i for filtering
                        foreach (var item in temp)
                        {
                            if (item.Username == _userManager.GetLoggedInUser().Username || item.Username == "admin")
                                continue;

                            if (option == i)
                            {
                                selectedItem = item;
                                targetAccount = SelectFromAccount(selectedItem, selectedItem.Username);
                                isSelected = true; // Exit the loop after selection
                                break;
                            }
                            i++; // Increment the index for valid users
                        }
                        break;
                }
            }


            decimal amount = 0;
            while (true)
            {
                Console.ResetColor();
                Console.WriteLine("    How much would you like to send?");
                var input = Console.ReadLine();

                if (decimal.TryParse(input, out amount))
                {
                    var account = _userManager.GetLoggedInUser().AccountManager.FindAccount(userAccount.AccountNumber);

                    if (account != null)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("    Invalid input. Please enter a valid amount.");
                }
            }
            Console.WriteLine("\n    Transaction Request has been sent, please wait for up to 15min for it to be proccessed.");
            _transactionManager.TransactionRequest(userAccount.AccountNumber, targetAccount.AccountNumber, amount);
            Console.WriteLine("    Press anywhere to continue...");
            Console.ReadLine();
            return eUIOptions.UserMenu; // Default to returning to User Menu
        }

        private Account SelectFromAccount(global::User user, string owner)
        {
            var temp = user.AccountManager.GetAccounts();

            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine($"    Chose one of {owner} accounts\n");

            ConsoleKeyInfo key;
            bool isSelected = false;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            bool supportANSI = IsAnsiSupported();
            string color = supportANSI ? "\u001b[32m" : "";
            string resetColor = supportANSI ? "\u001b[0m" : "";

            Console.CursorVisible = false;
            int option = 1;
            Account selectedItem;

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
                            Console.WriteLine($"{color}             Accountnumber: {item.AccountNumber} Currency: {item.Currency.AbbreviatedNameOfCurrency} Balance: {item.Balance.Amount}\u001b[0m");
                        else
                            Console.WriteLine($"             Accountnumber: {item.AccountNumber} Currency: {item.Currency.AbbreviatedNameOfCurrency} Balance: {item.Balance.Amount}");
                        i++;
                    }
                }
                else
                {
                    int i = 0;
                    foreach (var item in temp)
                    {
                        if (option == i + 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"             Accountnumber: {item.AccountNumber} Currency: {item.Currency.AbbreviatedNameOfCurrency} Balance: {item.Balance.Amount}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"             Accountnumber: {item.AccountNumber} Currency: {item.Currency.AbbreviatedNameOfCurrency} Balance: {item.Balance.Amount}");
                        }
                        i++;
                    }
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
                        return selectedItem = temp[option - 1];
                }
            }
            return null;
        }
    }
}
