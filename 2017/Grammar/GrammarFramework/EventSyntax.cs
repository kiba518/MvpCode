using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrammarFramework
{
     
    class EventSyntax  
    {
        public System.Windows.RoutedEventHandler routedEventHandler;
        public event EventHandler eventHandler;
        public event Action actionEvent;
        public void Excute()
        {

            routedEventHandler += (s,e) => {
                e.Handled = false;
            };
            actionEvent += () => {
                  throw new Exception();
                Console.WriteLine("2"); 
            };
            actionEvent += () => {
                Console.WriteLine("1");
            };
            actionEvent += () => {
                Console.WriteLine("1");
            };
            actionEvent += () => {
                Console.WriteLine("1");
            };
            actionEvent += () => {
                Console.WriteLine("1");
            };
            actionEvent += () => {
                Console.WriteLine("1");
            };
            actionEvent += () => {
                Console.WriteLine("1");
            };
            actionEvent();
        }
    }
}

   
 
