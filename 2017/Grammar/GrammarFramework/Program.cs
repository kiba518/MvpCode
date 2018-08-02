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
            KMP.KMP1("ABCDABCD", "ABCGABC");
            //EventSyntax fds = new EventSyntax();
            //fds.Excute();
            ////事件的外部定义
            //fds.testEvent += Fds_testEvent;

            Console.ReadKey();
        }

        private static void Fds_testEvent(string message)
        {
            throw new NotImplementedException();
        }
    }
    
}
