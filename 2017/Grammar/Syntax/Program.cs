﻿using Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Syntax
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.ExcuteGetNameCommand();

            Console.ReadKey();
        }
    }
}
