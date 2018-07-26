using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrammarFramework
{
    class DelegateSyntax
    {
        public delegate void TestDelegate(string message);
        public delegate string TestDelegate2(int m, long num);

        //TestDelegate2 td = Double;
        //string result = td(51, 8);
        //Console.WriteLine(result);
        //delegate string anonymousDelegate(int m, long num);
        //anonymousDelegate ad = delegate (int m, long num) { return m.ToString() + num.ToString(); };//2.0时代的匿名委托
        //anonymousDelegate ad2 = (m, num) => { return m.ToString() + num.ToString(); };//3.0以后匿名委托 
        //public delegate void Action();
        //public delegate TResult Func<out TResult>();
        //Action<int> a1 = (i) =>
        //{

        //};
        //Func<string, int> f1 = (str) =>
        //{
        //    return 1;//必须写 return 1;
        //};
        //Task taskAction = new Task(() => { });//无入参匿名Action
        //taskAction.Start(); 
        //    Task<int> taskFunc = new Task<int>(() => { return 1; });//无入参匿名Func
        //taskFunc.Start();
        //    int result = taskFunc.GetAwaiter().GetResult();

        public static void Excute()
        {
            Action action = new Action(() => { });
            IAsyncResult result = action.BeginInvoke((iar) =>
            {
            }, null);

            Func<int> func = new Func<int>(() => { return 1; });  
            IAsyncResult resultfunc = func.BeginInvoke((iar) =>
            {
                var res = func.EndInvoke(iar); 
            }, null);
        }

        static string Double(int m, long num)
        {
            return m.ToString() + num.ToString();
        }
        public void AsyncLoad(Action action)
        {
           
        }
        public void AsyncLoad(Action action, Action callback)
        {
            IAsyncResult result = action.BeginInvoke((iar) =>
            {
                callback();
            }, null);
        }
        public void AsyncLoad<T>(Action<T> action, T para, Action callback)
        {
            IAsyncResult result = action.BeginInvoke(para, (iar) =>
            {
                callback();
            }, null);
        }
        public void AsyncLoad<T, R>(Func<T, R> action, T para, Action<R> callback)
        {
            IAsyncResult result = action.BeginInvoke(para, (iar) =>
            {
                var res = action.EndInvoke(iar);
                callback(res);
            }, null);
        }
    }

}

   
 
