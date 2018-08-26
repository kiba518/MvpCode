using Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Syntax
{
    class Program
    {
        static void Main(string[] args)
        {
            ReflectionSyntax rs = new ReflectionSyntax();
            rs.ExcuteKibaAttribute();

            Console.ReadKey();
        }
    }
}
