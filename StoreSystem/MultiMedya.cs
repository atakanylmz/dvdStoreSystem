using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem
{
    public abstract class MultiMedya
    {
        public int id { get; set; }
        public string name { get; set; }
        public int duration { get; set; }
        public double price { get; set; }
        public string category { get; set; }
    }
}
