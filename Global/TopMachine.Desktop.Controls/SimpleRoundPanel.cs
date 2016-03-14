using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Drawing.Drawing2D;
 

namespace TopMachine.Desktop.Controls
{


    public class SimpleRoundPanel : System.Windows.Forms.Panel
    {

        protected override void Dispose(bool disposing)
        {
            if (bgBitmap != null)
            {
                bgBitmap.Dispose();
                bgBitmap = null;
            }

            if (bpath != null)
            {
                bpath.Dispose();
                bpath = null;
            }

            if (gpath != null)
            {
                gpath.Dispose();
                gpath = null;
            }

            if (ShapeImage != null)
            {
                ShapeImage.Dispose();
                ShapeImage = null; 
            }

            base.Dispose(disposing);
        }


        public delegate void OwnerPaintEvent(PaintEventArgs e);
        public event OwnerPaintEvent OwnerPaint;

        #region Public Properties


        private int round;
        [Bindable(true), Category("Button Appearance"),
        DefaultValue(false),
        Description("Corners rounding.")]
        public int Round
        {
            get
            {
                return round;
            }
            set
            {
                round = value;
                this.Invalidate();
            }
        }

        public Image ShapeImage { get; set; }

        #endregion Public Properties

        /// <summary>
        /// Windows form control derived from Button, to create round and elliptical buttons
        /// </summary>
        public SimpleRoundPanel()
        {
            this.Name = "SimpleRoundPanel";
            this.Size = new System.Drawing.Size(50, 50);		// Default size of control

            this.ResizeRedraw = true; 
            this.DoubleBuffered = true; 
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint , true);
            this.SetStyle(ControlStyles.ContainerControl, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

        }

        #region Private properties

        private GraphicsPath bpath;
        private GraphicsPath gpath;

        #endregion

        #region Base draw function


        Bitmap bgBitmap = null; 

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

            if (bgBitmap == null)
            {
                try
                {
                    if (Width > 0 && Height > 0)
                    {
                        FillBackground();
                    }
                }
                catch 
                {
                    ;
                }
            }

            if (bgBitmap != null)
            {
                pevent.Graphics.DrawImage(bgBitmap, 0, 0);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            base.OnPaint(e);

            if (OwnerPaint != null)
            {
                OwnerPaint(e);
            }

        }

        protected override void OnResize(EventArgs eventargs)
        {
            if (bgBitmap != null)
            {
                bgBitmap.Dispose(); 
            }

            bgBitmap = null;
            base.OnResize(eventargs);
        }

        /// <summary>
        /// Fill in the control background
        /// </summary>
        /// <param name="g">Graphics object</param>
        /// <param name="rect">Rectangle defining the button background</param>
        protected void FillBackground()
        {

            SetClickableRegion();

            if (bgBitmap != null)
            {
                bgBitmap.Dispose(); 
            }

            bgBitmap = new Bitmap(this.Width, this.Height);

            Graphics g = Graphics.FromImage(bgBitmap);

            Shapes s = SetClickableRegion();

            if (ShapeImage != null)
            {
                g.DrawImage(ShapeImage, new Rectangle(0, 0, this.Width, this.Height));
            }
            g.Dispose(); 

        }

        public override void Refresh()
        {
            bgBitmap.Dispose();
            bgBitmap = null; 
            base.Refresh();
        }

  
        #endregion Base draw functions

        #region Overrideable shape-specific methods

        protected virtual Shapes SetClickableRegion()
        {
            Shapes s = new Shapes(this.ClientRectangle, round);
            this.Region = new Region(s.path);			// Click only activates on elipse
            return s; 
        }

        protected virtual void FillShape(Graphics g, Object brush, Rectangle rect)
        {
            Shapes s = new Shapes(rect, round);
            if (brush.GetType().ToString() == "System.Drawing.Drawing2D.LinearGradientBrush")
            {
                g.FillPath((LinearGradientBrush)brush, s.path);
            }
            else if (brush.GetType().ToString() == "System.Drawing.Drawing2D.PathGradientBrush")
            {
                g.FillPath((PathGradientBrush)brush, s.path);
            }
        }

        protected virtual void AddShape(GraphicsPath gpath, Rectangle rect)
        {
            Shapes s = new Shapes(rect, round);
            gpath.AddPath(s.path, false);
        }

        protected virtual void DrawShape(Graphics g, Pen pen, Rectangle rect)
        {
            Shapes s = new Shapes(rect, round);
            g.DrawPath(pen, s.path);
        }

        #endregion Overrideable shape-specific methods

        #region Help functions


        #endregion Private Help functions


    }

}
