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
using ReflectionTest;

namespace Syntax
{
    public class ReflectionSyntax
    { 
        public static void Excute()
        {
            Type type = GetType("Syntax.Kiba");
            Kiba kiba = (Kiba)Activator.CreateInstance(type);
            Type type2 = GetType2("Syntax.Kiba");
            Kiba kiba2 = (Kiba)Activator.CreateInstance(type2);
        }
        public static Type GetType(string fullName)
        {
            Assembly assembly = Assembly.LoadWithPartialName("Syntax");
            Type type = assembly.GetType(fullName, true, false);
            return type;
        }

        public static Type GetType2(string fullName)
        {
            Type t = Type.GetType(fullName);
            return t;
        } 
    }

    public class Kiba
    { }

}

   
 
