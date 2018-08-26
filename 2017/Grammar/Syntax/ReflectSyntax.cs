using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Syntax
{
    public partial class ReflectionSyntax
    { 
        //public static void Excute()
        //{
        //    Type type = GetType("Syntax.Kiba");
        //    Kiba kiba = (Kiba)Activator.CreateInstance(type);
        //    Type type2 = GetType2("Syntax.Kiba");
        //    Kiba kiba2 = (Kiba)Activator.CreateInstance(type2);
        //}
        public static Type GetType(string fullName)
        {
            Assembly assembly = Assembly.Load("Syntax");
            Type type = assembly.GetType(fullName, true, false);
            return type;
        }

        public static Type GetType2(string fullName)
        {
            Type t = Type.GetType(fullName);
            return t;
        }

        public static void Excute()
        {
            Type t1 = Type.GetType("Syntax.Kiba");
            Type t2 = Type.GetType("Kiba");
            Kiba kiba = (Kiba)Activator.CreateInstance(t1); 
            Kiba kiba2 = (Kiba)Activator.CreateInstance(t2);
        }

        public static void ExcuteMethod()
        { 
            Assembly assembly = Assembly.Load("Syntax"); 
            Type type = assembly.GetType("Syntax.Kiba", true, false);
            MethodInfo method =  type.GetMethod("PrintName"); 
            object kiba = assembly.CreateInstance("Syntax.Kiba");
            object[] pmts = new object[] { "Kiba518" };
            method.Invoke(kiba, pmts);//执行方法  
        }



        public static void ExcuteProperty()
        {
            Kiba kiba = new Kiba();
            kiba.Name = "Kiba518";
            object name = ReflectionSyntax.GetPropertyValue(kiba, "Name");
            Console.WriteLine(name); 
        } 
        public static object GetPropertyValue(object obj, string name)
        {
            PropertyInfo property = obj.GetType().GetProperty(name);
            if (property != null)
            {
                object drv1 = property.GetValue(obj, null);
                return drv1;
            }
            else
            {
                return null;
            } 
        }
    } 
    public class Kiba
    {
        public string Name { get; set; }
        public void PrintName(string name)
        {
            Console.WriteLine(name);
        }
    }

}

   
 
