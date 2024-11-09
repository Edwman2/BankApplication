using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class UserManager
    {
    }
}
public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int FailedLoginAttempts { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        FailedLoginAttempts = 0;
    }
}
public class AuthenticationManager
{
    private List<User> _users = new List<User>();

    public AuthenticationManager()
    {
        // Här kan du lägga till användare vid start
        _users.Add(new User("admin", "password123"));
        // ... fler användare
    }

    public bool Authenticate(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return false; // Användare inte hittad
        }

        if (user.Password != password)
        {
            user.FailedLoginAttempts++;
            if (user.FailedLoginAttempts >= 3)
            {
                Console.WriteLine("Kontot är låst.");
                return false;
            }
            return false; // Felaktigt lösenord
        }

        // Inloggning lyckad
        user.FailedLoginAttempts = 0;
        return true;
    }
}
public class BankApp
{
    private AuthenticationManager _authManager = new AuthenticationManager();

    public void Run()
    {
        Console.WriteLine("Välkommen till bankappen!");

        while (true)
        {
            Console.Write("Ange användarnamn: ");
            string username = Console.ReadLine();

            Console.Write("Ange lösenord: ");
            string password = Console.ReadLine();


            if (_authManager.Authenticate(username, password))
            {
                Console.WriteLine("Inloggning lyckad!");
                // Här kan du lägga till funktionalitet för inloggade användare
                break;
            }
            else
            {
                Console.WriteLine("Inloggning misslyckades.");
            }
        }
    }
}