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


    /*          ------ Removed from BankApp.cs ----------
                TransactionManager ts = new TransactionManager();
                ts.HandleUnprocessedTransactions();




                Account account = new Account("BA0001", 800);
                Account account1 = new Account("BA6753", 600);





                ts.AddAccount(account);
                ts.AddAccount(account1);
                ts.FindAccounts("BA0001");
                ts.FindAccounts("BA6753");


                ts.TransactionRequest("BA0001", "BA6753", 500);

                //ts.ProcessedAccounts("BA0001", "BA6753", 500);
                //ts.ProcessedAccounts("BA6753", "BA0001", 500);



                account.showinfo();


                Console.ReadKey();

                */



    internal class TransactionManager
    {
        // List with accounts from account
        List<Account> ValidAccounts;
        Queue<TransactionLog> UnprocessedTransactions;
        Queue<TransactionLog> NewUnprocessedTransactions;

        internal TransactionManager()
        {
            //List to store the verified accounts handled by transactionmanager
            ValidAccounts = new List<Account>();
            UnprocessedTransactions = new Queue<TransactionLog>();
            NewUnprocessedTransactions = new Queue<TransactionLog>();

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
            UnprocessedTransactions.Enqueue(new TransactionLog(AccountnumberOfSender,AccountNumberOfreciever,amount));
        }
        
        internal async void HandleUnprocessedTransactions()
        {
            
            while (true)
            {
                await Task.Delay(5000); //15 minuters delay
                while (UnprocessedTransactions.Count > 0)
                {
                    NewUnprocessedTransactions.Enqueue(UnprocessedTransactions.Dequeue());
                }


                Console.WriteLine("Handling transactions " + NewUnprocessedTransactions.Count);
                
                while (NewUnprocessedTransactions.Count > 0)
                {
                    ProcessesAccounts(NewUnprocessedTransactions.Dequeue());
                }
            }
            
            
        }

        internal void ProcessesAccounts(TransactionLog log)
        {
            var SenderAccount = FindAccounts(log.FromUser);
            var DepositToAccount = FindAccounts(log.ToUser);

            if (SenderAccount == null)
            {
                /*await Task.Delay(10000);*/ Console.WriteLine("This account can not be found");
                
            }
            if (DepositToAccount == null)
            {
                /*await Task.Delay(10000);*/ Console.WriteLine("The recieveraccount can not be found");
                
            }
            if (SenderAccount.Balance.Amount < log.Amount) // Solved error by changing prop Balance to public
            {
                Console.WriteLine("Not enough money in your account");
                
            }
            /*await Task.Delay(10000);*/ Console.WriteLine($"both accounts were found and the transaction is in progress. {log.Amount}" +
                                             $" SEK is on it's way.\nSender: {log.FromUser,1} \nReciever: {log.ToUser,1}");
            TransactionLog transaction = new TransactionLog(SenderAccount.AccountNumber, DepositToAccount.AccountNumber, log.Amount);

            SenderAccount.Withdraw(log.Amount);
            DepositToAccount.Deposit(log.Amount);

            SenderAccount.AddTransactionLog(transaction);
            DepositToAccount.AddTransactionLog(transaction);
           
            
        }




















    }
    internal class TransactionLog
    {
        internal string FromUser { get; set; }
        internal string ToUser { get; set; }
        internal decimal Amount { get; set; }
        internal string ErrorMessage { get; set; }
        internal DateTime dateTime = DateTime.Now;

        internal TransactionLog(string fromuser, string touser, decimal amount)
        {
            FromUser = fromuser;
            ToUser = touser;
            Amount = amount;

        }



        




    }
}
