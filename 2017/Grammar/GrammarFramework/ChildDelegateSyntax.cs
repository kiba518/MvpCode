using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrammarFramework
{
    public class FirstDelegateSyntax
    {
        public FirstDelegateSyntax()
        {
            Console.WriteLine(" First 开始 "  );
            SecondDelegateSyntax sds = new SecondDelegateSyntax(()=> {
                Console.WriteLine(" First传给Second委托被触发 ");
            });
            sds.Excute();
            Console.WriteLine(" First 结束 ");
        }
    }

    public class SecondDelegateSyntax
    {
        public Action Action { get; set; }
        public SecondDelegateSyntax(Action _action)
        {
            Console.WriteLine(" Second的构造函数 ");
            Action = _action;
        }
        public void Excute()
        {
            Console.WriteLine(" Second的Excute被触发 ");
            Action();
        }
    }
}

   
 
