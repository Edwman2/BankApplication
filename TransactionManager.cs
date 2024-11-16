using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace BankApplication
{
    internal class TransactionManager
    {
        internal Queue<TransactionLog> UnprocessedTransactions { get; set; }
        internal Queue<TransactionLog> NewUnprocessedTransactions { get; set; }
        internal CurrencyManager currencyManager;
        internal UserManager userManager;

        Account SenderAccount;
        Account DepositToAccount;

        internal TransactionManager(CurrencyManager currencyManager, UserManager userManager)
        {
            UnprocessedTransactions = new Queue<TransactionLog>();
            NewUnprocessedTransactions = new Queue<TransactionLog>();
            this.currencyManager = currencyManager;
            this.userManager = userManager;
        }

        internal void TransactionRequest(string AccountnumberOfSender, string AccountNumberOfreciever, decimal amount)
        {
            UnprocessedTransactions.Enqueue(new TransactionLog(AccountnumberOfSender, AccountNumberOfreciever, amount));
        }

        internal async void HandleUnprocessedTransactions()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));//15min delay

                while (UnprocessedTransactions.Count > 0)
                {
                    NewUnprocessedTransactions.Enqueue(UnprocessedTransactions.Dequeue());
                }

                while (NewUnprocessedTransactions.Count > 0)
                {
                    ProcessesAccounts(NewUnprocessedTransactions.Dequeue());
                }
            }
        }

        internal void ProcessesAccounts(TransactionLog log)
        {
            Account SenderAccount = userManager.GetUser("", log.FromUser).AccountManager.FindAccount(log.FromUser);
            Account DepositToAccount = userManager.GetUser("", log.ToUser).AccountManager.FindAccount(log.ToUser);

            log.status = "Failed";
            if (SenderAccount == null)
            {
                log.ErrorMessage = "Your account could not be found";
            }
            else if (DepositToAccount == null)
            {
                log.ErrorMessage = "The account you are trying to transfer money to could not be found";
            }
            else if (SenderAccount.Balance.Amount < log.Amount)
            {
                log.ErrorMessage = "Not enough money in your account";
            }
            else
            {
                SenderAccount.Withdraw(log.Amount);
                
                if (SenderAccount.Currency.AbbreviatedNameOfCurrency != DepositToAccount.Currency.AbbreviatedNameOfCurrency)
                {   
                    decimal originalAmount = log.Amount;
                    decimal? convertedAmount = currencyManager.ConvertCurrency(new Balance(log.Amount), SenderAccount.Currency, DepositToAccount.Currency);

                    if (convertedAmount == null)
                        return;

                    log.Amount = (decimal)convertedAmount;
                    //Console.WriteLine("Both accounts were found and the transaction is in progress.");
                    //Console.WriteLine($"Converted transferred money from {originalAmount} {SenderAccount.Currency.AbbreviatedNameOfCurrency}" +
                    //                  $" to {convertedAmount} {DepositToAccount.Currency.AbbreviatedNameOfCurrency}\nSender: {log.FromUser,1} \nReciever: {log.ToUser,1}");
                }
                else
                {
                    log.Amount = (decimal)currencyManager.ConvertCurrency(new Balance(log.Amount), SenderAccount.Currency, DepositToAccount.Currency);
                }

                SenderAccount.Withdraw(log.Amount);

                if (DepositToAccount is SavingsAccount)
                    ((SavingsAccount)DepositToAccount).ApplySavingsInterest(log.Amount);
                else
                    DepositToAccount.Deposit(log.Amount);

                log.ErrorMessage = "";
                log.status = "Success";
            }
            AddTransactionLog(log);
        }

        public void AddTransactionLog(TransactionLog transaction)
        {
            userManager.GetUser("", transaction.FromUser).AccountManager.LogTransaction(transaction, transaction.FromUser);
            userManager.GetUser("", transaction.ToUser).AccountManager.LogTransaction(transaction, transaction.ToUser);
        }
    }
}
