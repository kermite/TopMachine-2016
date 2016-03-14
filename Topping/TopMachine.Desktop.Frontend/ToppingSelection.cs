using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TopMachine.Desktop.Controls;

namespace TopMachine.Topping.Frontend
{

    public partial class ToppingSelection : UserControl, IFinished
    {
        public delegate void ItemSelectedEvent(string tag);
        public event ItemSelectedEvent ItemSelected; 


        public ToppingSelection()
        {
            InitializeComponent();
        }

        private void lblLocal_Click(object sender, EventArgs e)
        {
            if (ItemSelected != null)
            {
                ItemSelected( ((LinkLabel)sender).Tag.ToString()); 
            }
        }

        #region IFinished Members

        public event OnFinishedDelegate OnFinished;

        #endregion

        private void lblLocal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
