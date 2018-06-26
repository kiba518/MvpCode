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
            //目标框架.net framework 4.0 ，我们可以看到属性的声明：

            var tuple = new Tuple<string, int, int, int>(
                             "Kiba", 00001, 00002,
                             00003);

            Console.WriteLine(tuple.Item1);
            Console.WriteLine(tuple.Item2);
            Console.WriteLine(tuple.Item3);
            Console.WriteLine(tuple.Item4);

            var tupleCalss = new Tuple<A, B>(
                         new A(), new B());
            Console.WriteLine(tupleCalss.Item1.Name);
            Console.WriteLine(tupleCalss.Item2.Name);
             
            Console.ReadKey();
        }
    }
    public class A
    {
        public string name = "A";

        public string Name { get => name; set => name = value; }
    }
    public class B
    {
        public string Name = "B";
    }
}
