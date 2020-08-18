using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversion.MVVM.Model
{
    public class Animal
    {
        public string name { get; set; }
        public int count { get; set; }
        public string habitat { get; set; }

        public Animal(string name, int count, string habitat)
        {
            this.name = name;
            this.count = count;
            this.habitat = habitat;
        }
    }
}
