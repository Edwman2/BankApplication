﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.UI
{
    internal class TransferBetweenAccountsMenu : Menu
    {
        private UserManager _userManager;
        private TransactionManager _transactionManager;

        public TransferBetweenAccountsMenu(UserManager userManager, TransactionManager transactionManager)
        {
            _userManager = userManager;
            _transactionManager = transactionManager;
        }

        public override eUIOptions Display()
        {
            List<Account> temp = _userManager.GetLoggedInUser().AccountManager.GetAccounts();

            // Select the source account
            Account userAccount = SelectFromAccount(_userManager.GetLoggedInUser(), "your");

            // Select the target account, excluding the source account
            Account targetAccount = SelectFromAccount(_userManager.GetLoggedInUser(), "the target user's", userAccount);

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
            Account selectedItem;

            // Selection loop for the target account (already done above)
            decimal amount = 0;
            while (true)
            {
                Console.ResetColor();
                Console.WriteLine("    How much would you like to send?");
                var input = Console.ReadLine();

                if (decimal.TryParse(input, out amount))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("    Invalid input. Please enter a valid amount.");
                }
            }

            Console.WriteLine("\n    Transaction Request has been sent, please wait for up to 15min for it to be processed.");
            _userManager.GetUsers();
            _transactionManager.TransactionRequest(userAccount.AccountNumber, targetAccount.AccountNumber, amount);

            Console.WriteLine("    Press anywhere to continue...");
            Console.ReadLine();
            return eUIOptions.UserMenu; // Default to returning to User Menu
        }


        private Account SelectFromAccount(global::User user, string owner, Account excludeAccount = null)
        {
            var temp = user.AccountManager.GetAccounts();

            // Exclude the account that has already been selected (if any)
            if (excludeAccount != null)
            {
                temp = temp.Where(acc => acc.AccountNumber != excludeAccount.AccountNumber).ToList();
            }

            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine($"    Choose one of {owner}'s accounts\n");

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
