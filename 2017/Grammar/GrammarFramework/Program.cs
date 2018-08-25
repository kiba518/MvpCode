using GrammarFramework.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            NotifySyntax fds = new NotifySyntax();
            fds.Excute(); 
            Console.ReadKey();
        }

        private static void Fds_testEvent(string message)
        {
            throw new NotImplementedException();
        }
    }
    
}
