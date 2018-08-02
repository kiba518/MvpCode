using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
namespace MSQ
{
    /*
     * 
     * 
     * “消息队列”技术允许在不同时间运行的应用程序在可能暂时脱机的异类网络和系统之间进行通信。应用程序发送、
     * 接收或查看（读取而不移除）队列中的消息。“消息队列”是 Windows 2000 和 Windows NT 的可选组件，而且必须单独安装。
     
        msdtc.exe是微软分布式传输协调程序。该进程调用系统Microsoft Personal Web Server和Microsoft SQL Server。该服务用于管理多个服务器。

      最佳答案解决办法：重新安装msdtc服务
    开始－－运行－－cmd
    net stop msdtc
    msdtc -uninstall
    msdtc - install
    net start msdtc
     */
    class Program
    {
        static void Main(string[] args)
        {
            string path = @".\private$\test";

            if (MessageQueue.Exists(path))
            {
                MessageQueue.Delete(path);
            }
            //创建一个消息队列 并发送
            MessageQueue msq = MessageQueue.Create(path);//指定现有的资源链接路径
            /*
            MessageQueue 类提供对“消息队列”队列的引用。可以在 MessageQueue 构造函数中指定一个连接到现有资源的路径，
            或者可在服务器上创建新队列。在调用 Send(Object)、Peek 或 Receive 之前，
            必须将 MessageQueue 类的新实例与某个现有队列关联。此时，可操作该队列的属性，如 Category 和 Label。
            MessageQueue 支持两种类型的消息检索：同步和异步。同步的 Peek 和 Receive 方法使进程线程用指定的间隔时间等待新消息到达队列。
            异步的 BeginPeek 和 BeginReceive 方法允许主应用程序任务在消息到达队列之前，在单独的线程中继续执行。这些方法通过使用回调对象和状态对象进行工作，以便在线程之间进行信息通信。
            当创建 MessageQueue 类的新实例时，并不是要创建新的“消息队列”队列。而是可使用 Create(String)、Delete 和 Purge 方法管理服务器上的队列。
            与 Purge 不同，Create(String) 和 Delete 是 static 成员，因此可以调用它们而无需创建 MessageQueue 类的新实例。
            可以使用以下三种名称之一设置 MessageQueue 对象的 Path 属性：友好名称、FormatName 或 Label。友好名称由队列的 MachineName 和 QueueName 属性定义，
            对于公共队列为 MachineName\QueueName，对于专用队列为 MachineName\Private$\QueueName。FormatName 属性允许对消息队列进行脱机访问。
            最后，可使用队列的 Label 属性设置队列的 Path。
            有关 MessageQueue 的实例的初始属性值列表，请参见 MessageQueue 构造函数。
             * 
             * 对于多线程操作，只有以下方法是安全的：BeginPeek、BeginReceive、EndPeek、EndReceive、GetAllMessages、Peek 和 Receive。
             */
            msq.Send(8);
            msq.Send("Kiba518");

            Mathes mm = new Mathes();
            msq.Send(mm);
            msq.Send(mm);

            //出队
            ((XmlMessageFormatter)msq.Formatter).TargetTypes = new Type[] { typeof(System.Int32) };
            Message m1 = msq.Receive();

            Console.WriteLine(m1.Body.ToString());

            ((XmlMessageFormatter)msq.Formatter).TargetTypes = new Type[] { typeof(System.String) };
            Message m2 = msq.Receive();

            Console.WriteLine(m2.Body.ToString());

            ((XmlMessageFormatter)msq.Formatter).TargetTypes = new Type[] { typeof(Mathes) };
            Message m3 = msq.Receive();
            Console.WriteLine(mm.SumAB(10, 20));

            #region 异步

            ((XmlMessageFormatter)msq.Formatter).TargetTypes = new Type[] { typeof(Mathes) };
            msq.BeginReceive(new TimeSpan(10000), msq, new AsyncCallback(MySum));

            #endregion

            Console.Read();
        }
        static void MySum(IAsyncResult ia)
        {
            MessageQueue msq = ia.AsyncState as MessageQueue;

            Message m4 = msq.EndReceive(ia);

            Mathes mm = m4.Body as Mathes;

            Console.WriteLine(mm.SumAB(25, 35));
        }
    }


    public class Mathes
    {
        private int a = 25;

        private string b = "35";

        public string B
        {
            get { return b; }
            set { b = value; }
        }

        public int A
        {
            get { return a; }
            set { a = value; }
        }

        public int SumAB(int a, int b)
        {
            return a + b;
        }

    }
 
}
