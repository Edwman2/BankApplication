﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankApp bankApp = new BankApp();
            bankApp.Run();
        }
    }
}
