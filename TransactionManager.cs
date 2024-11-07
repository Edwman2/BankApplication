using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;


namespace BankApplication
{
    //public class Transaction
    //{
    //    public string FromAccount { get; }
    //    public string ToAccount { get; }
    //    public decimal Amount { get; }
    //    public DateTime Timestamp { get; }

    //    public Transaction(string fromAccount, string toAccount, decimal amount)
    //    {
    //        FromAccount = fromAccount;
    //        ToAccount = toAccount;
    //        Amount = amount;
    //        Timestamp = DateTime.Now;
    //    }
    //}

    //public class TransactionManager
    //{
    //    private List<Transaction> transactionLog = new List<Transaction>();

    //    public void ScheduleTransaction(string fromAccount, string toAccount, decimal amount)
    //    {
    //        // Skapa en transaktion och lägg till i loggen
    //        var transaction = new Transaction(fromAccount, toAccount, amount);
    //        transactionLog.Add(transaction);
    //    }

    //    public async Task<string> "namn" ProcessScheduledTransactions()
    //    {
    //        // Logik för att genomföra transaktioner var 15:e minut
    //    }
    //}

   




    internal class TransactionManager
    {
        // List with accounts from account
        List<Account> ValidAccounts;
        Queue<Task> UnproccessedTransactions;


        internal TransactionManager()
        {
            //List to store the verified accounts handled by transactionmanager
            ValidAccounts = new List<Account>();
            UnproccessedTransactions = new Queue<Task>();

        }

        //adds account to the list
        internal void AddAccount(Account account)
        {
            ValidAccounts.Add(account);
        }

        // Method to match given Accountnumber to accountnumbers stored in the list.
        internal Account FindAccounts(string AccountNumber)
        {

            foreach (var valid in ValidAccounts)
            {
                if (valid.AccountNumber == AccountNumber)
                {
                    return valid;
                }

            }
            return null;
        }


        internal void TransactionRequest(string AccountnumberOfSender, string AccountNumberOfreciever, decimal amount)
        {
            UnproccessedTransactions.Enqueue(ProcessesAccounts(AccountnumberOfSender,AccountNumberOfreciever,amount));
        }

        internal async Task HandleUnprocessedTransactions()
        {
            while(true)
            {
                await Task.Delay(1);
                Console.WriteLine("Handling transaction " + UnproccessedTransactions.Count);
                foreach(var item in UnproccessedTransactions)
                {

                    
                    item.Start();
                    UnproccessedTransactions.Dequeue();
                }
            }
            
            
        }

        internal async Task<bool> ProcessesAccounts(string AccountnumberOfSender, string AccountNumberOfreciever, decimal amount)
        {
            var SenderAccount = FindAccounts(AccountnumberOfSender);
            var DepositToAccount = FindAccounts(AccountNumberOfreciever);

            if (SenderAccount == null)
            {
                /*await Task.Delay(10000);*/ Console.WriteLine("This account can not be found");
                return false;
            }
            if (DepositToAccount == null)
            {
                /*await Task.Delay(10000);*/ Console.WriteLine("The recieveraccount can not be found");
                return false;
            }
            if (SenderAccount.Balance < amount)
            {
                Console.WriteLine("Not enough money in your account");
                return false;
            }
            /*await Task.Delay(10000);*/ Console.WriteLine($"both accounts were found and the transaction is in progress. {amount} SEK is on it's way.\nSender: {AccountnumberOfSender,1} \nReciever: {AccountNumberOfreciever,1}");
            TransactionLog transaction = new TransactionLog(SenderAccount.AccountNumber, DepositToAccount.AccountNumber, amount);

            SenderAccount.Withdraw(amount);
            DepositToAccount.Deposit(amount);

            SenderAccount.AddTransactionLog(transaction);
            DepositToAccount.AddTransactionLog(transaction);
            return true;
            
        }




















    }
    internal class TransactionLog
    {
        internal string FromUser { get; set; }
        internal string ToUser { get; set; }
        internal decimal Amount { get; set; }
        internal DateTime dateTime = DateTime.Now;

        internal TransactionLog(string fromuser, string touser, decimal amount)
        {
            FromUser = fromuser;
            ToUser = touser;
            Amount = amount;

        }



        




    }
}
