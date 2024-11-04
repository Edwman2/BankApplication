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