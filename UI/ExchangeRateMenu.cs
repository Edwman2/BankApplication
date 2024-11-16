using System;
using System.Collections.Generic;
using System.Linq;

namespace BankApplication.UI
{
    internal class ExchangeRateMenu : Menu
    {
        private CurrencyManager _currencyManager;

        public ExchangeRateMenu(CurrencyManager currencyManager)
        {
            _currencyManager = currencyManager;
        }

        public override eUIOptions Display()
        {
            Console.Clear();
            Console.WriteLine($"{Logo.LogoSMALL}\n");
            Console.WriteLine("    Chose One Of Supported Currency\n");

            ConsoleKeyInfo key;
            bool isSelected = false;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            bool supportANSI = IsAnsiSupported();
            string color = supportANSI ? "\u001b[32m" : "";
            string resetColor = supportANSI ? "\u001b[0m" : "";

            Console.CursorVisible = false;
            var temp = _currencyManager.GetExchangeRates();
            KeyValuePair<string, decimal> selectedItem = new();
            int option = 1;

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
                            Console.WriteLine($"{color}             {item.Key} {item.Value}\u001b[0m");
                        else
                            Console.WriteLine($"             {item.Key} {item.Value}");
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
                            Console.WriteLine($"             {item.Key} {item.Value}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"             {item.Key} {item.Value}");
                        }
                        i++;
                    }
                    Console.ResetColor(); // Reset color after loop
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
            Console.WriteLine("    Input new value");
            decimal newValue = 0;
            bool isValid = false;

            while (!isValid)
            {
                string input = Console.ReadLine();
                isValid = decimal.TryParse(input, out newValue);

                if (!isValid)
                    Console.WriteLine("Invalid input. Please enter a valid numeric value for the new exchange rate:");
            }

            _currencyManager.UpdateExchangeRate(selectedItem.Key, newValue, true);
            return nextState;
        }
    }
}