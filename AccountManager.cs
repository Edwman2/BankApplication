using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class AccountManager
    {
        public List<Account> Accounts { get; set; }

        public AccountManager()
        {
            Accounts = new List<Account>();
        }

        //public void Add(string accountNumber, )
        // WORK IN PROGRESS

        //public void DeleteAccount(string accountNumber)
        //{
        //    var deleteAccount = Accounts.Find(a => a.AccountNumber == accountNumber);
        //}


    }
}
