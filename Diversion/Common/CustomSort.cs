using Diversion.MVVM.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversion.Common
{
    public class CustomSort : IComparer
    {
        private string type { get; set; }

        public int Compare(object x, object y)
        {
            Animal objX = x as Animal;
            Animal objY = y as Animal;
            if (type == "count")
            {
                int digitsX = objX.count;
                int digitsY = objY.count;
                if (digitsX < digitsY)

                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return objX.name.CompareTo(objY.name);
            }
        }

        public CustomSort(string type)
        {
            this.type = type;
        }
    }
}
