using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopMachine.Training.FrontEnd
{
    public class DropDownPairs
    {
        private string display;
        private object val;

        public string Display
        {
            get { return display; }
            set { display = value; }
        }

        public object Value
        {
            get { return val; }
            set { val = value; }
        }

        public DropDownPairs(string d, object v)
        {
            display = d;
            val = v;
        }

    }

}
