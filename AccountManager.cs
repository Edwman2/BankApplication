using System.Collections.Generic;

namespace BankApplication
{
    internal class AccountManager
    {
        private List<Account> accounts { get; set; }

        public AccountManager()
        {
            accounts = new List<Account>();
        }
        // Display User's accounts
        public void DisplayAccounts()
        {
            foreach (Account account in accounts)
            {
                account.DisplayInfo();
            }
        }

        public bool AddAccount(string accountNumber, string currency)
        {
            accounts.Add(new Account(accountNumber, new Balance(0.00m), new Currency(currency)));
            return true;
        }

        public bool AddSavingsAccount(string accountNumber, string currency)
        {
            accounts.Add(new SavingsAccount(accountNumber, new Balance(0.00m), new Currency(currency)));
            return true;
        }

        public Account FindAccount(string accountsNumber)
        {
            Account foundAccount = accounts.Find(a => a.AccountNumber == accountsNumber);

            return foundAccount;
        }

        // WORK IN PROGRESS

        //public bool DeleteAccount(string accountNumber)
        //{
            
        //    var deleteAccount = accounts.Find(a => a.AccountNumber == accountNumber);
        //    if (deleteAccount == null) return false;
        //    else if (deleteAccount.)
                
        //        accounts.Remove(deleteAccount);
        //    return false;
        //}
    }
}
