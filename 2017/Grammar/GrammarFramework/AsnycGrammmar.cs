using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrammarFramework
{
    class AsnycGrammmar
    {
        //1，await 只能在标记了async的函数内使用
        //1，await 等待的函数必须标记async 
        public static void Excute()
        {
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + " 开始 Excute " + DateTime.Now); 
            Start();
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + " 结束 Excute " + DateTime.Now);

        }
        
        //使用 async 修饰符可将方法、lambda 表达式或匿名方法指定为异步。 如果对方法或表达式使用此修饰符，则其称为异步方法。 如下示例定义了一个名为 Start 的异步方法： 
        private async static void Start()
        {
            try
            {
                Console.WriteLine(Thread.CurrentThread.GetHashCode() + " 开始 Task1 时间：" + DateTime.Now); 
                Task<int> task1 = new Task<int>(Sleep);
                task1.Start();
                int exampleInt = (await task1);
                Console.WriteLine(Thread.CurrentThread.GetHashCode() + " 结束 Task1 时间：" + DateTime.Now +" 结果："+ exampleInt);


                Console.WriteLine(Thread.CurrentThread.GetHashCode() + " 开始 Task2 时间：" + DateTime.Now);
                var funcSleep = new Func<object, int>(Sleep2);
                Task<int> task2 = new Task<int>(funcSleep, 1000);
                task2.Start();
                int exampleInt2 = (await task2);
                Console.WriteLine(Thread.CurrentThread.GetHashCode() + " 结束 Task2 时间：" + DateTime.Now + " 结果：" + exampleInt2);

            }
            catch (Exception)
            {
                 
            }
        }
        public static int Sleep()
        {
            Thread.Sleep(5000);
            return 1;
        }
        public static int Sleep2(object result)
        {
            Thread.Sleep(5000);
            return int.Parse(result.ToString());
        }

       
    }
   
}
