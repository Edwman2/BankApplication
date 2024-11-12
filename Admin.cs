using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class Admin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int FailedLoginAttempts { get; set; }

        AuthenticationManager userManager;

        public Admin(string username, string password, AuthenticationManager userManager)
        {
            Username = username;
            Password = password;
            FailedLoginAttempts = 0;
            this.userManager = userManager;
        }

        public void CreateUser(string name, string password)
        {
            userManager._users.Add(new User(name, password));
        }
    }
}
