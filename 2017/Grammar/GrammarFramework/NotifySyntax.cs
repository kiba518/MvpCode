using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrammarFramework
{
    /*
    

     */
    class NotifySyntax
    {
        string Name { get; set; }
        string NameNotify { get; set; }
        public void Excute()
        {
            NotifyEntity ne = new NotifyEntity();
            Name = ne.Name;
            Binding bding = new Binding("NameNotify", ne, "NameNotify"); 
           
            NameNotify = ne.NameNotify;
            Console.WriteLine("Name:" + ne.Name);
            Console.WriteLine("NameNotify:" + ne.NameNotify);
            Name = "kiba518";
            NameNotify = "kiba518";
            Console.WriteLine("Name:" + ne.Name);
            Console.WriteLine("NameNotify:" + ne.NameNotify);
        } 
    }

    public class NotifyEntity : INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;
        private string _NameNotify = "notify";
        public string NameNotify
        {
            get { return _NameNotify; }
            set
            {

                _NameNotify = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("NameNotify"));
                }
            }
        }
        private string _Name= "normal";
        public string Name
        {
            get 
            { 
                return _Name; 
            }
            set
            {
                _Name = value;
            }
        }
        
    }

}
 
