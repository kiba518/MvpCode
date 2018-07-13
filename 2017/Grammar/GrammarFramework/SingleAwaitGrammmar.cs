using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GrammarFramework
{
    class SingleAwaitGrammmar
    {
        //1，await 只能在标记了async的函数内使用
        //2，await 等待的函数必须标记async 
        //3，await 等待的函数必须返回Task 
        //4，声明可等待函数时必须以async Task<int>模式
        public static async void Excute()
        {
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + " 开始 Excute " + DateTime.Now);
            await SingleNoAwait(); 
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + " 结束 Excute " + DateTime.Now);
        }
        public static async Task SingleNoAwait()
        {
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + " SingleNoAwait开始 " + DateTime.Now);
            Task.Run(() =>
            {
                Console.WriteLine(Thread.CurrentThread.GetHashCode() + " Run1 " + DateTime.Now);
                Thread.Sleep(1000);
            }).GetAwaiter().GetResult();
            Task.Run(() =>
            {
                Console.WriteLine(Thread.CurrentThread.GetHashCode() + " Run2 " + DateTime.Now);
                Thread.Sleep(1000);
            }).GetAwaiter().GetResult();
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + " SingleNoAwait结束 " + DateTime.Now);
            return;
        }
        /// <summary>
        /// 定义被等待的线程函数，注意
        /// </summary> 
        public static async Task SingleAwait()
        {
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + " AwaitTest开始 " + DateTime.Now);
            await Task.Run(() =>
            {
                Console.WriteLine(Thread.CurrentThread.GetHashCode() + " Run1 " + DateTime.Now);
                Thread.Sleep(1000);
            });
            await Task.Run(() =>
            {
                Console.WriteLine(Thread.CurrentThread.GetHashCode() + " Run2 " + DateTime.Now);
                Thread.Sleep(1000);
            });
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + " AwaitTest结束 " + DateTime.Now);
            return;
        }

      

        //public static async void SingleAwait()
        //{
        //    Console.WriteLine(Thread.CurrentThread.GetHashCode() + " 开始 SingleAwait " + DateTime.Now);
        //    await AwaitTest();
        //    //await AwaitTest().ConfigureAwait(false);
        //    Console.WriteLine(Thread.CurrentThread.GetHashCode() + " 结束 SingleAwait " + DateTime.Now);
        //}



        //public static async Task<int> AsyncTest()
        //{
        //    //Console.WriteLine(Thread.CurrentThread.GetHashCode() + " Await " + DateTime.Now);
        //    //int result = 1;
        //    //Console.WriteLine(Thread.CurrentThread.GetHashCode() + " Await " + DateTime.Now);
        //    //return result;
        //    //HttpClient _httpClient = new HttpClient();
        //    //var getDotNetFoundationHtmlTask = _httpClient.GetStringAsync("http://www.dotnetfoundation.org");
        //    //var html = await getDotNetFoundationHtmlTask;
        //    //int count = Regex.Matches(html, @"\.NET").Count;
        //    //Console.WriteLine(Thread.CurrentThread.GetHashCode() + " Await " + DateTime.Now);
        //    //return count;

        //    //int result = 1;
        //    //var CompletedTask = Task.FromResult(result);


        //    //return CompletedTask.Result;
        //    return 1;
        //}
        public static int NoAsyncTest()
        {
            return 1;
        }
       
    }
}