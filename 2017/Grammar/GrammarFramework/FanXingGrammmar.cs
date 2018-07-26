using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrammarFramework
{
    class FanXingGrammmar
    {
        //Generic<FanXing> gFanXing = new Generic<FanXing>();
        //Generic<Base> gFanXingBase = new Generic<Base>();
        //Generic<string> gs = new Generic<string>(); 这样定义会报错
        public static void Excute()
        {
            GenericFunc gf = new GenericFunc();
            gf.FanXingFunc<FanXing>(new FanXing() { Name="Kiba518"});
        }
        public class GenericFunc
        {
            public void FanXingFunc<T>(T obj)
            { 
                var name = GetPropertyValue(obj, "Name");
                Console.WriteLine(name); 
            }
            public object GetPropertyValue(object obj, string name)
            {
                object drv1 = obj.GetType().GetProperty(name).GetValue(obj, null);
                return drv1;
            }
        }
       

        public class Generic<T> where T : Base
        {
            public T Name = default(T); 
        } 
        public class Base  
        {
            public string Name { get; set; }
        }
        public class FanXing : Base
        {
            public new string Name { get; set; }
        }

         
    }

   
}
