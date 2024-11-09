using System;
using System.Collections.Generic;
using System.Threading;
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


    /* 
            Tests passed.
     
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
        // *** OLD List with accounts from account *** 
        // List<Account> ValidAccounts;
        // NEW - The list is stored and handled by the Account Manager.
        internal Queue<TransactionLog> UnprocessedTransactions { get; set; }
        internal Queue<TransactionLog> NewUnprocessedTransactions { get; set; }
        internal AccountManager AccountConnector { get; private set; }
        internal List<TransactionLog> Transactionslogged { get; set; }

        internal TransactionManager(AccountManager aM)
        {
            // ****List to store the verified accounts handled by transactionmanager ****
            // NEW - The list is stored and handled by the Account Manager.
            UnprocessedTransactions = new Queue<TransactionLog>();
            NewUnprocessedTransactions = new Queue<TransactionLog>();
            AccountConnector = aM;
            Transactionslogged = new List<TransactionLog>();
        }

        internal void TransactionRequest(string AccountnumberOfSender, string AccountNumberOfreciever, decimal amount)
        {
            /*Thread.Sleep(3000);*/
            UnprocessedTransactions.Enqueue(new TransactionLog(AccountnumberOfSender,AccountNumberOfreciever,amount));
        }
        
        internal async void HandleUnprocessedTransactions()
        {
            while (true)
            {
                await Task.Delay(5000); //15 minuters delay
                // Thread.Sleep(5000) ?
                while (UnprocessedTransactions.Count > 0)
                {
                    NewUnprocessedTransactions.Enqueue(UnprocessedTransactions.Dequeue());
                }

                Console.WriteLine("Handling transactions " + NewUnprocessedTransactions.Count);
                
                while (NewUnprocessedTransactions.Count > 0)
                {
                    ProcessesAccounts(NewUnprocessedTransactions.Dequeue());
                }

                ShowTransactionInfo();
            }  
        }

        internal void ProcessesAccounts(TransactionLog log)
        {
            
            Account SenderAccount =  AccountConnector.FindAccount(log.FromUser); // Using accountmanager 
            Account DepositToAccount = AccountConnector.FindAccount(log.ToUser);

            if (SenderAccount == null)
            {
                Console.WriteLine("Your account can not be found");
            }
            else if (DepositToAccount == null)
            {
                Console.WriteLine("The account you are trying to transfer money to can not be found");
            }
            else if (SenderAccount.Balance.Amount < log.Amount) 
            {
                Console.WriteLine("Not enough money in your account");
            }
            else
            {
                Console.WriteLine($"both accounts were found and the transaction is in progress. {log.Amount}" +
                                             $" SEK is on it's way.\nSender: {log.FromUser,1} \nReciever: {log.ToUser,1}");
                TransactionLog transaction = new TransactionLog(SenderAccount.AccountNumber, DepositToAccount.AccountNumber, log.Amount);

                SenderAccount.Withdraw(log.Amount);
                DepositToAccount.Deposit(log.Amount);

                AddTransactionLog(transaction);
            }
        }
        

        public void AddTransactionLog(TransactionLog transaction)
        {
            Transactionslogged.Add(transaction);
        }

        public void ShowTransactionInfo()
        {
            foreach (var transaction in Transactionslogged)
            {
                Console.WriteLine($"{transaction.FromUser},{transaction.ToUser}, {transaction.dateTime}, {transaction.Amount}");
            }
        }
    }
}
