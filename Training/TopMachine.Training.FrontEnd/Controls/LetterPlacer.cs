using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TopMachine.Training.FrontEnd.Controls
{
    public partial class LetterPlacer: IDisposable 
    {
        public bool OnBoard = true;
        public Status enmStatus;


        bool Activated = true;

        public string strLetter = "A";
        public string strPts;

        public Color Col_Lettre;

        public int Left;
        public int Top;
        public int Width;
        public int Height;
        public Color ForeColor;
        public Color BackColor;
        public Rectangle Bounds;
        public bool Visible = true;

        public LetterPlacer()
        {
            BackColor = Color.Tan;
            ForeColor = Color.FromArgb(30, 30, 30);
        }

        public LetterPlacer(string Letter)
        {
            strLetter = Letter;
            BackColor = Color.Tan;
            ForeColor = Color.FromArgb(30, 30, 30);
        }

        public LetterPlacer(string Letter, string pts, int left, int top, int height, int width, bool activate)
        {
            try
            {
                strLetter = Letter;
                strPts = pts;
                SetPosition(left, top, height, width);
                BackColor = Color.Tan;
                ForeColor = Color.FromArgb(30, 30, 30);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SetLetterPlacer(string Letter, string pts, int left, int top, int height, int width, bool activate)
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
                throw new Exception(e.Message);
            }
        }


        public void SetLetter(string Letter)
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

            if (Activated)
            {
                this.BackColor = Color.DarkGreen;
                this.ForeColor = Color.LightGray; 
            }
            else
            {
                this.BackColor = Color.LightGray;
                this.ForeColor = Color.Black; 
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



        System.Drawing.Font FontLetter = new System.Drawing.Font("Tahoma", 40, System.Drawing.FontStyle.Bold);
        System.Drawing.Font FontScore = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);


        Bitmap bmp = null;

        public Bitmap GetBmp()
        {
            if (bmp == null)
            {
                Paint();
            }
            return bmp;
        }

        public void Paint()
        {
            if (this.Width == 0 || this.Height == 0)
            {
                return;
            }

            if (bmp != null)
            {
                bmp.Dispose();
            }

            bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.Clear(BackColor);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            float size = (this.Height - 8) / g.DpiX * 72;

            using (Font FontLetter = new System.Drawing.Font("Tahoma", size, System.Drawing.FontStyle.Bold))
            {
            using (SolidBrush b = new SolidBrush(ForeColor))
            {
                if (OnBoard)
                {
                    g.DrawString(strLetter.ToString().ToUpper(), FontLetter, b, new RectangleF(3, 3, Width - 5, Height - 5), sf);
                }
                else
                {
                    g.DrawString(strLetter.ToString().ToUpper(), FontLetter, b, new RectangleF(0, 0, Width, Height), sf);
                }
            }
        }

            g.Dispose();
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null;
            }

            FontLetter.Dispose();
            FontScore.Dispose();
        }

        #endregion
    }

}
