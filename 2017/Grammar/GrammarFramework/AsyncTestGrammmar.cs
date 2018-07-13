using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrammarFramework
{
    class AsyncTestGrammmar
    {
        public static async void Excute()
        {
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + " 开始 Excute " + DateTime.Now);
            var waitTask = AsyncTestRun();
            waitTask.Start();
            int i = await waitTask;
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + " i " + i);
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + " 结束 Excute " + DateTime.Now);
        }
        public static Task<int> AsyncTestRun()
        {
            Task<int> t = new Task<int>(() => {
                Console.WriteLine(Thread.CurrentThread.GetHashCode() + " Run1 " + DateTime.Now);
                Thread.Sleep(1000);
                return 100;
            });
            return t;
        }
        public static async Task<int> AsyncTest()
        {
            Task<int>.Run(() =>
            {
                Console.WriteLine(Thread.CurrentThread.GetHashCode() + " Run1 " + DateTime.Now);
                Thread.Sleep(1000);
            }).GetAwaiter().GetResult();
            return 1;
        }

        public static async Task<int> AsyncTestInt()
        {
            int i = Task<int>.Run(() =>
            {
                Console.WriteLine(Thread.CurrentThread.GetHashCode() + " Run1 " + DateTime.Now);
                Thread.Sleep(1000);
                return 100;
            }).GetAwaiter().GetResult();
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + " i " + i);
            return 1;
        }
      
    }
}
