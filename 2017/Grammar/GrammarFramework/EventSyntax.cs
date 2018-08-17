using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrammarFramework
{
    /*
     事件是C#的基础之一，学好事件对于了解.NET框架大有好处。
     事件最常见的比喻就是订阅，即，如果你订阅了我的博客，那么，当我发布新博客的时候，你就会得到通知。而这个过程就是事件，或者说是事件运行的轨迹。
     事件是发散，以我的博客为核心，向所有订阅者发送消息。
     最基础的自定义事件

     */
    class EventSyntax
    {
        //public System.Windows.RoutedEventHandler routedEventHandler; 利益系统委托定义事件
        //public event EventHandler eventHandler;
        //public event Action actionEvent;
        //public void Excute()
        //{

        //    routedEventHandler += (s, e) =>
        //    {
        //        e.Handled = false;
        //    };
        //    actionEvent += () =>
        //    {
        //        throw new Exception();
        //        Console.WriteLine("2");
        //    };
        //    actionEvent += () =>
        //    {
        //        Console.WriteLine("1");
        //    };
        //    actionEvent += () =>
        //    {
        //        Console.WriteLine("1");
        //    };
        //    actionEvent += () =>
        //    {
        //        Console.WriteLine("1");
        //    };
        //    actionEvent += () =>
        //    {
        //        Console.WriteLine("1");
        //    };
        //    actionEvent += () =>
        //    {
        //        Console.WriteLine("1");
        //    };
        //    actionEvent += () =>
        //    {
        //        Console.WriteLine("1");
        //    };
        //    actionEvent();
        //}
        //首先定义一个委托，然后利用event关键字，定义一个事件。整体上看，好像就是在定义一个委托，只是在委托的定义之前，加了个event关键字。
        // 没错，事件的定义就是这样，因为要声明一个事件，需要两个元素： 标识提供对事件的响应的方法的委托，和一个类，用存储事件的数据。
        public delegate void TestDelegate(string message);      
        public event TestDelegate testEvent;
        public void Init()
        { 
            testEvent += new TestDelegate(EventSyntax_testEvent); 
            testEvent += EventSyntax_testEvent; 
        }
        private void EventSyntax_testEvent(string message)
        {
            Console.WriteLine(message);
        }
        void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine("The threshold of {0} was reached at {1}.", e.Threshold, e.TimeReached);
            Environment.Exit(0);
        }
      

          //Counter c = new Counter(new Random().Next(10));
          //  c.ThresholdReached += c_ThresholdReached; 
          //  Console.WriteLine("press 'a' key to increase total");
          //  while (Console.ReadKey(true).KeyChar == 'a')
          //  {
          //      Console.WriteLine("adding one");
          //      c.Add(1);
          //  }  
    }


    class Counter
    {
        private int threshold;
        private int total;

        public Counter(int passedThreshold)
        {
            threshold = passedThreshold;
        }

        public void Add(int x)
        {
            total += x;
            if (total >= threshold)
            {
                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                args.Threshold = threshold;
                args.TimeReached = DateTime.Now;
                OnThresholdReached(args);
            }
        }

        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;
    }

    public class ThresholdReachedEventArgs : EventArgs
    {
        public int Threshold { get; set; }
        public DateTime TimeReached { get; set; }
    }


}
 
