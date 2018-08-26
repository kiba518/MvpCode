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
        public void ExcuteKibaAttribute()
        {
            Kiba kiba = new Kiba();
            kiba.ClearName = "Kiba518";
            kiba.NoClearName = "Kiba518";
            kiba.NormalName = "Kiba518";
            ClearKibaAttribute(kiba);
            Console.WriteLine(kiba.ClearName);
            Console.WriteLine(kiba.NoClearName);
            Console.WriteLine(kiba.NormalName);
        }
        public void ClearKibaAttribute(Kiba kiba)
        {
            List<PropertyInfo> plist = typeof(Kiba).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).ToList();//只获取Public的属性 
            foreach (PropertyInfo pinfo in plist)
            {
                var attrs = pinfo.GetCustomAttributes(typeof(KibaAttribute), false);
                if (null != attrs && attrs.Length > 0)
                { 
                    var des = ((KibaAttribute)attrs[0]).Description; 
                    if (des == "Clear")
                    {
                        pinfo.SetValue(kiba, null); 
                    }
                }
            }
        } 
    } 
    public class Kiba
    {
        [KibaAttribute("Clear")]
        public string ClearName { get; set; }
        [KibaAttribute("NoClear")]
        public string NoClearName { get; set; }
        public string NormalName { get; set; }

    }
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class KibaAttribute : System.Attribute
    {
        public string Description { get; set; }
        public KibaAttribute(string description)
        {
            this.Description = description;
        }
    }
}

   
 
