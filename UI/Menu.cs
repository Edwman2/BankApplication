using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication;

namespace BankApplication.UI
{
    abstract class Menu
    {
        public enum eUIOptions
        {
            NULL,//somethings wrong is this becomes the state.
            LoginMenu,
            MainMenu,
            AdminMenu,
            UserMenu,
            Exit
        }

        public abstract eUIOptions Display();

        protected int option = 1;
        protected eUIOptions nextState;
        protected static Logo Logo = new Logo();

        protected bool IsAnsiSupported()
        {
            // Detecting Windows with terminal support (e.g., PowerShell 7+ or Windows Terminal)
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // PowerShell 7+ and Windows Terminal support ANSI codes
                return Console.IsOutputRedirected == false && (Environment.GetEnvironmentVariable("TERM") != null);
            }

            // On Unix-based systems (Linux/macOS), ANSI codes are typically supported
            return true;
        }
    }

    class User //temporary
    {
        public string name;
        public string password;
        public User(string name, string password)
        {
            this.name = name;
            this.password = password;
        }
    }
}
