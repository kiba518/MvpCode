using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReflectionTest;
namespace ReflectionFrom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        } 
        private void Form1_Load(object sender, EventArgs e)
        {
           
           
            Type type = GetType("System.Windows.Forms.Button");
            Button kiba = (Button)Activator.CreateInstance(type);
            Type type2 = GetType2("System.Windows.Forms.Button");
            Button kiba2 = (Button)Activator.CreateInstance(type2);
        } 
        public static Type GetType(string fullName)
        {
            Assembly assembly = Assembly.LoadWithPartialName("System.Windows.Forms");
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
