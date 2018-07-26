using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrammarFramework
{
    class MDelegateSyntax
    {
        public delegate int MyDelegate1(int i);
        public void Excute()
        {
            MyDelegate1 myDelegate1 = F1;
            myDelegate1 += F2;
            myDelegate1 += F3;
            myDelegate1 += F4;
            myDelegate1(1);
        }
        public int F1(int i)
        {
            Console.WriteLine("1");
            return 1;
        }
        public int F2(int i)
        {
            Console.WriteLine("2");
            return 2;
        }
        public int F3(int i)
        {
            
            Console.WriteLine("3");
            return 3;
        }
        public int F4(int i)
        {
            Console.WriteLine("4");
            return 4;

        }

    }
    
}

   
 
