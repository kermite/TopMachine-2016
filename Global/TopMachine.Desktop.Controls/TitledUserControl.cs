using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace TopMachine.Desktop.Controls
{
    public delegate bool CollapsedEvent(TitledUserControl ctl); 
 
    public class TitledUserControl : UserControl
    {
        public event CollapsedEvent Collapsed;

        [Localizable(true)]
        public string Title { get; set; }

        public TitledUserControl()
        {
        }

        public bool DoCollapse()
        {
            if (Collapsed != null)
            {
                return Collapsed(this);
            }

            return false; 
        }

    }
}
