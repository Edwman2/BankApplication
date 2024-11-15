using System;

namespace BankApplication
{
    internal class BankApp
    {

        public BankApp()
        {
            //Setup
            InteractiveMenu newLogin = new InteractiveMenu();
            newLogin.Display();
            Console.ReadLine();
        }

        public void Run()
        {
            //Does nothing yet.
        }
    }
}
