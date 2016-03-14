using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using TopMachine.Desktop.Overall;


namespace TopMachine.Topping.Engine.GUI.Board
{
    /// <summary>
    /// Summary description for CShapeButton.
    /// </summary>
    public class CArrow : UserControl
    {
        Guid MemoryCheckId;
        public delegate void OnChangeDirectionEvent(int i);
        public event OnChangeDirectionEvent OnChangeDirection;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private int iDirection = 0;
        private int iBorderSize;
        private Color cBorderColor;

        private string strCaption;
        private int intOpacity = 255;

        public int Direction
        {
            get { return iDirection; }
            set { iDirection = value; }
        }

        public int BorderSize
        {
            get { return iBorderSize; }
            set { iBorderSize = value; this.Invalidate(); }
        }

        public Color BorderColor
        {
            get { return cBorderColor; }
            set { cBorderColor = value; this.Invalidate(); }
        }


        public CArrow()
        {

            InitializeComponent();
            MemoryCheckId = MemoryCheck.Register(this, "CArrow CREATE");
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            if( this.Region != null) this.Region.Dispose(); 

            MemoryCheck.Unregister(MemoryCheckId);
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();
            // 
            // CArrow
            // 
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.Name = "CArrow";
            this.Click += new System.EventHandler(this.CArrow_Click);
            this.ResumeLayout(false);

        }
        #endregion


        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            RectangleF f = new RectangleF(0, 0, Width, Height);

            Color c = BackColor;

            if (c != Color.Transparent)
            {
                g.Clear(c);
            }

            using (System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath())
            {

                if (iDirection == 1)
                {
                    gp.AddLine(2, 2, Width - 2, Height / 2);
                    gp.AddLine(Width - 2, Height / 2, 2, Height - 2);
                    gp.AddLine(2, 2, 2, Height / 2);
                    ResetRegion(new System.Drawing.Region(gp));
                }
                else
                {
                    gp.AddLine(2, 2, Width - 2, 2);
                    gp.AddLine(Width - 2, 2, Width / 2, Height - 2);
                    gp.AddLine(Width / 2, Height - 2, 2, 2);
                    ResetRegion(new System.Drawing.Region(gp));
                }

                using (SolidBrush b = new SolidBrush(BackColor))
                {
                    g.FillPath(b, gp);
                }

                using (SolidBrush b = new SolidBrush(Color.Black))
                {
                    using (Pen p = new Pen(b, 2))
                    {
                        g.DrawPath(p, gp);
                    }
                }

            }
        }


        private void ResetRegion(Region reg)
        {
            Region oldReg = this.Region;
            this.Region = reg;

            if (oldReg != null) oldReg.Dispose();

        }

        private void CArrow_Click(object sender, System.EventArgs e)
        {
            if (iDirection == 1) iDirection = 0;
            else iDirection = 1;

            Invalidate();

            if (OnChangeDirection != null)
            {
                OnChangeDirection(iDirection);
            }
        }

    }
}
