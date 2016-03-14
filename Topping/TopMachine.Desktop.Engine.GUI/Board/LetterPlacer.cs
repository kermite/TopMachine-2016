using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TopMachine.Desktop.Overall;
using TopMachine.Desktop.Controls;

namespace TopMachine.Topping.Engine.GUI.Board
{
    public partial class LetterPlacer :Control,  IDisposable
    {
        public bool OnBoard = true;
        public Status enmStatus;


      //  bool Activated = true;

        public char strLetter = 'A';
        public string strPts;

        public Color Col_Lettre;

       // public int Left;
       // public int Top;
       // public int Width;
       // public int Height;
       // public Color ForeColor;
       // public Color BackColor;
       // public Rectangle Bounds;
        public ImageButton letterControl; 

        System.Drawing.Font FontLetter = new System.Drawing.Font("Arial Black", 40, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((System.Byte)(0)));
        System.Drawing.Font FontScore = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((System.Byte)(0)));


        public LetterPlacer()
        {
            BackColor = Color.Tan;
            ForeColor = Color.FromArgb(30, 30, 30);
            this.SetStyle(ControlStyles.UserMouse, true);
        }

        public LetterPlacer(char Letter, string pts, int left, int top, int height, int width, bool activate)
        {
            try
            {
                strLetter = Letter;
                strPts = pts;
                SetPosition(left, top, height, width);
                BackColor = Color.Tan;
                ForeColor = Color.FromArgb(30, 30, 30);
                //this.Click += new EventHandler(LetterPlacer_Click);
                //this.DragOver += new System.Windows.Forms.DragEventHandler(this.LetterPlacer_DragOver);
                //this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LetterPlacer_MouseMove);
                //this.DragDrop += new System.Windows.Forms.DragEventHandler(this.LetterPlacer_DragDrop);
                //this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LetterPlacer_MouseDown);
                //this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.letterControl_MouseUp);
                this.SetStyle(ControlStyles.UserMouse, true);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //void LetterPlacer_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        public void SetLetterPlacer(char Letter, string pts, int left, int top, int height, int width, bool activate)
        {
            try
            {
                this.strLetter = Letter;
                this.strPts = pts;
                SetPosition(left, top, height, width);

                Paint();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void SetLetter(char Letter)
        {
            strLetter = Letter;
        }

        public void SetPts(string pts)
        {
            strPts = pts;
        }

        public enum Status
        {
            Chevalet,
            Tableau

        }


        public void CacheLetter()
        {
            this.enmStatus = Status.Tableau;
        }

        public void ShowLetter()
        {
            this.enmStatus = Status.Chevalet;
        }

        public void SetCustomColor(Color Back, Color Fore)
        {
            if (Back != Color.Empty)
            {
                this.BackColor = Back;
            }

            if (Fore != Color.Empty)
            {
                ForeColor = Fore;
            }
        }

        public void SetColor(bool Activated)
        {
            Color BackColor = Color.DarkRed;

            if (Activated)
            {
                this.BackColor = Color.LightGreen;
                if (this.BackColor != BackColor)
                {
                    this.BackColor = BackColor;
                }
            }
            else
            {
                this.BackColor = Color.LightGray;
            }

        }

        private void SetPosition(int left, int top, int height, int width)
        {
            this.Left = left;
            this.Top = top;
            this.Height = height;
            this.Width = width;

            Bounds = new Rectangle(Left, Top, Width, Height);
        }

        SafeBitmap bmp = null;

        public Bitmap GetBmp()
        {
            if (bmp == null)
            {
                Paint();
            }
            return bmp.bmp;
        }


        public void ResetBmp()
        {
            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null;

            }
        }
        private void DisplayLetterPoint(float size, Graphics g, StringFormat sf) 
        {
            float posXLetterPts = 0, posYLetterPts = 0, withLetterPts = 0, heightLetterPts = 0;
            FontScore = new System.Drawing.Font("Tahoma", size / 4, System.Drawing.FontStyle.Bold);
            int pts = int.Parse(strPts);

            if (pts < 10)
            {
                posXLetterPts = Width - 15;
                posYLetterPts = Height - 15;
                withLetterPts = 15;
                heightLetterPts = 15;

            }
            else
            { // more large
                posXLetterPts = Width - 20;
                posYLetterPts = Height - 15;
                withLetterPts = 20;
                heightLetterPts = 15;

            }

            using (SolidBrush b = new SolidBrush(Color.Gray))
            {
            g.DrawString(strPts, FontScore, b, new RectangleF(posXLetterPts, posYLetterPts, withLetterPts, heightLetterPts), sf);
            }

        }

        private void letterControl_OwnerPaint(Graphics g)
        {
           // Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                float size = (this.Height - 8) / g.DpiX * 72;
                float posXLetter = 0, posYLetter = 0, withLetter = 0, heightLetter = 0;

                if (OnBoard)
                {
                    posXLetter = 3;
                    posYLetter = 3;
                    withLetter = Width - 5;
                    heightLetter = Height - 5;

                    using (FontLetter = new System.Drawing.Font("Tahoma", size, System.Drawing.FontStyle.Bold))
                    {
                        using (SolidBrush b = new SolidBrush(ForeColor))
                        {
                            g.DrawString(strLetter.ToString().ToUpper(), FontLetter, b, new RectangleF(posXLetter, posYLetter, withLetter, heightLetter), sf);
                        }
                        sf.LineAlignment = StringAlignment.Far;
                        sf.Alignment = StringAlignment.Far;
                    }
                }
            }

        }

        private void Paint()
        {
            if (this.Width == 0 || this.Height == 0)
            {
                return;
            }

            if (bmp != null)
            {
                bmp.Dispose();
            }

            bmp = new SafeBitmap(new Bitmap(this.Width, this.Height), this);
            Graphics g = bmp.g;
            g.Clear(BackColor);

            letterControl_OwnerPaint(g);

        }

        #region IDisposable Members

        public void Dispose()
        {
            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null;
            }


        }

        #endregion
              
       
    }

}
