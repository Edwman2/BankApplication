using BankApplication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankApplication
{
}
public class User
{
    internal AccountManager AccountManager;
    public string Username { get; set; }
    public string Password { get; set; }
    public bool LockedAccount { get; set; }
    public int FailedLoginAttempts { get; set; }


    public User(string username, string password)
    {
        Username = username;
        Password = password;
        LockedAccount = false;
        FailedLoginAttempts = 3;
        AccountManager = new();
        AccountManager.AddAccount("SEK");
        AccountManager.AddAccount("USD");
        Console.WriteLine("");
    }
}
public class UserManager
{
    private List<User> _users;
    private User loggedInUser;

    public UserManager()
    {
        _users = new();
        loggedInUser = new("","");
    }
    
    public List<User> GetUsers()
    {
        return _users;
    }

    public void UpdateLoggedInUser(User user)
    {
        loggedInUser = user;
    }

    public User GetLoggedInUser()
    {
        return loggedInUser;
    }

    public User GetUser(string userName = "", string accountNumber = "")
    {
        if (!string.IsNullOrEmpty(userName))
        {
            return _users.Find(a => a.Username == userName);
        }
        else if (!string.IsNullOrEmpty(accountNumber))
        {
            return _users.FirstOrDefault(user => user.AccountManager.GetAccounts()
                .Any(account => account.AccountNumber == accountNumber));
        }

        return null;
    }

    public void DeleteUser(string username)
    {
        var user = _users.Find(u => u.Username == username);
        if (user != null)
            _users.Remove(user); 
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public bool Authenticate(string username, string password)
    {
        var user = _users.Find(u => u.Username == username);

        if (user == null)
            return false;

        if (user.Password != password)
        {
            return false;
            //user.FailedLoginAttempts++;
            //if (user.FailedLoginAttempts >= 3)
            //{
            //    return false;
            //}
            //return false;
        }

        user.FailedLoginAttempts = 3;
        return true;
    }

    public bool PasswordValid(string password)
    {
        //Would we want some restriction on passwords?
        //Keeping it just returning true to show the functionallity could exist here.
        return true;
    }

    public bool UsernameValid(string username)
    {
        var user = _users.Find(u => u.Username == username);

        if (user != null) //if user ain't null means we have someone with that name.
            return false;

        return true;
    }
}

//public class BankApp
//{
//    private AuthenticationManager _authManager = new AuthenticationManager();

//    public void Run()
//    {
//        Console.WriteLine("Välkommen till bankappen!");

//        while (true)
//        {
//            Console.Write("Ange användarnamn: ");
//            string username = Console.ReadLine();

//            Console.Write("Ange lösenord: ");
//            string password = Console.ReadLine();


//            if (_authManager.Authenticate(username, password))
//            {
//                Console.WriteLine("Inloggning lyckad!");
//                // Här kan du lägga till funktionalitet för inloggade användare
//                break;
//            }
//            else
//            {
//                Console.WriteLine("Inloggning misslyckades.");
//            }
//        }
//    }
//}