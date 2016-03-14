using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TopMachine.Desktop.Controls.Tools
{
    public partial class Progress : UserControl
    {
        public static Progress _progress;

        public static Progress Instance
        {
            get {
                if (_progress == null || _progress.IsDisposed)
                {
                    _progress = new Progress();
                }
                return _progress;
            }
        }

        public Progress()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }

        public void  Show(Control parent)
        {
            if (Instance.Parent != null)
            {
                Instance.Parent.Controls.Remove(Instance); 
            }

            this.progressBar1.Value = 0;
            this.Visible = true; 

            this.Width = parent.Width -40;
            this.Left = 20;
            this.Top = (parent.Height - this.Height) / 2;

            timer1.Enabled = true;

            parent.Controls.Add(Instance);
            this.BringToFront();
            Application.DoEvents(); 
        }

        public void Hide(Control parent)
        {
            try
            {
                parent.Controls.Remove(this);
            }
            catch (Exception ee)
            {
                ;
            }
            this.Visible = false;
            timer1.Enabled = false;
            this.Dispose();
            _progress = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!IsDisposed && progressBar1 != null)
            {
                progressBar1.Value = (progressBar1.Value + 5) % 100;
                Application.DoEvents();
            }
        }
    }
}
