using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrammarFramework
{
    class BaseDelegateSyntax
    { 
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
    class ChildDelegateSyntax : BaseDelegateSyntax
    {
        public void Excute()
        {
            //开启异步方法
            base.AsyncLoad(() => { });

            //开启异步方法，并且在异步结束后，触发回调方法
            base.AsyncLoad(() => { },
                ()=> 
                {
                    //我是回调方法
                });

            //开启异步有入参的方法，传递参数，并且在异步结束后，触发回调方法
            base.AsyncLoad<string>((s) => { },"Kiba518",
               () =>
               {
                    //我是回调方法
               });

            //开启异步有入参的方法，传递字符串参数Kiba518，之后返回int型结果518，
            //并且在异步结束后，触发回调方法，回调函数中可以获得结果518
            base.AsyncLoad<string,int>((s) => {
                return 518;
            }, "Kiba518",
               (result) =>
               {
                   //我是回调方法 result是返回值518
               });
        }
    }
}

   
 
