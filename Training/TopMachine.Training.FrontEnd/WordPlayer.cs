using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace TopMachine.Training.FrontEnd.Controls
{
    /// <summary>
    /// Summary description for WordPlayer.
    /// </summary>
    public class WordPlayer : System.Windows.Forms.UserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private string strTirage, strWord, strCurrent;
        private char[] chrTirage, chrCurrent, chrPlaced;
        private LetterPlacer[] objLetters;
        private int intNbLetters;
        private int intHeight, intFirstX, intFirstY;

        public string Current
        {
            get { return strCurrent; }
        }

        #region drag & drop
        int LastX;
        int LastY;
        int LastPosition;

       
        void this_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        void this_DragDrop(object sender, DragEventArgs e)
        {

            Point clientPoint = this.PointToClient(new Point(e.X, e.Y));

            LastX = clientPoint.X;
            LastY = clientPoint.Y;
            LetterPlacer current= null, move;
            move = (LetterPlacer)e.Data.GetData(typeof(LetterPlacer));
            int temp = 0;
            current = getLetter(ref temp);

            if (move != null && current != null)
            {
                Switch(move, current, LastPosition, temp);
                SetTirage(strTirage, "");
            
            }

        }
      
        private void this_MouseMove(object sender, MouseEventArgs e)
        {
            LastX = e.X;
            LastY = e.Y;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {

                LetterPlacer current = null;
                int temp = 0;
                current = getLetter(ref temp);
                if (current != null)
                {
                    LastPosition = temp;
                    this.DoDragDrop(current, DragDropEffects.All | DragDropEffects.Move);

                }
            }
        }

        private LetterPlacer getLetter(ref int pos)
        {
            Point p = new Point(LastX, LastY);

            for (int x = 0; x < intNbLetters; x++)
            {
                LetterPlacer lp = objLetters[x];
                if (lp.Bounds.Contains(p))
                {
                    pos = x;
                    return lp;

                }
            }
            return null;
        }

        public void Switch(LetterPlacer O1, LetterPlacer O2, int posO1, int posO2)
        {
            string c;
            string pt;
            int st;

            c = O1.strLetter;
            pt = O1.strPts;
            st = (int)O1.enmStatus;

            //O1.strLetter = O2.strLetter;
            //O1.strPts = O2.strPts;
            //O1.enmStatus = O2.enmStatus;

            char[] lstCharTirage = strTirage.ToCharArray();
            lstCharTirage[posO1] = O2.strLetter[0];
            lstCharTirage[posO2] = c[0];

            strTirage = new string(lstCharTirage);

            //O2.strLetter = c;
            //O2.strPts = pt;
            //O2.enmStatus = (LetterPlacer.Status)st;


        }
        # endregion

        public WordPlayer()
        {
            InitializeComponent();

            SetStyle(ControlStyles.Selectable, false);
            SetStyle(ControlStyles.ContainerControl, false);

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserMouse, true);
            SetStyle(ControlStyles.StandardClick, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            this.AllowDrop = true;
            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);
            this.UpdateStyles();

            string Tirage = "";
            strTirage = Tirage;
            chrTirage = Tirage.ToCharArray();
            strCurrent = "";
            chrCurrent = strCurrent.ToCharArray();
            intNbLetters = strTirage.Length;

        }

        public void SetBack()
        {
            if (strCurrent.Length == 0) return;
            strCurrent = strCurrent.Substring(0, strCurrent.Length - 1);
            chrCurrent = strCurrent.ToCharArray();
            ResetLetters();

        }
        public void ShuffleWord()
        {
            System.Random rnd = new System.Random();
            List<KeyValuePair<int, char>> list = new List<KeyValuePair<int, char>>();
            foreach (char s in chrTirage)
            {
                list.Add(new KeyValuePair<int, char>(rnd.Next(), s));
            }

            // Sort the list by the random number
            var sorted = from item in list
                         orderby item.Key
                         select item;

            string strTirage = "";
            foreach (KeyValuePair<int, char> pair in sorted)
            {
                strTirage += pair.Value;

            }


            SetTirage(strTirage, "");

        }

        //public void ShuffleWord()
        //{
        //    int posLetter = 0;
        //    int[] CheckLetters = new int[intNbLetters];

        //    // We First Check Characters already Played 
        //    for (int xx = 0; xx < intNbLetters; xx++)
        //        CheckLetters[xx] = 0;

        //    foreach (char c in chrCurrent)
        //    {
        //        for (int x = 0; x < intNbLetters; x++)
        //        {
        //            if (chrTirage[x] == c && CheckLetters[x] == 0)
        //            {
        //                CheckLetters[x] = 1;
        //                break;
        //            }
        //        }

        //        posLetter++;
        //    }

        //    System.Random objr = new System.Random();
        //    int l = chrTirage.Length - posLetter - 1;
        //    for (int ll = posLetter; ll < chrTirage.Length; ll++)
        //    {
        //        int newpl = objr.Next(l);
        //        char c = chrPlaced[posLetter];
        //        char c2 = chrPlaced[posLetter + newpl + 1];
        //        objLetters[posLetter].SetLetter(c2.ToString());
        //        objLetters[posLetter + newpl + 1].SetLetter(c2.ToString());
        //        chrPlaced[posLetter] = c2;
        //        chrPlaced[posLetter + newpl + 1] = c;
        //    }

        //    strTirage = "";
        //    foreach (var item in chrPlaced)
        //    {
        //        strTirage = strTirage + item;  
        //    }
        //    SetTirage(strTirage,""); 
        //    PaintTirage();

        //    //  this.Invalidate();

        //}

        public void SetLetter(char Letter)
        {
            int[] CheckLetters = new int[intNbLetters];

            // We First Check Characters already Played 
            for (int xx = 0; xx < intNbLetters; xx++)
                CheckLetters[xx] = 0;

            foreach (char c in chrCurrent)
            {
                bool Ok = false;

                for (int x = 0; x < chrTirage.Length; x++)
                {
                    if (c == chrTirage[x] && CheckLetters[x] == 0)
                    {
                        CheckLetters[x] = 1;
                        Ok = true;
                        break;
                    }

                }

                if (Ok == false)
                {
                    for (int x = 0; x < chrTirage.Length; x++)
                    {

                        if (chrTirage[x] == '?' && CheckLetters[x] == 0)
                        {
                            CheckLetters[x] = 1;
                            Ok = true;
                            break;
                        }
                    }
                }
            }

            // We Check if Letter is Valid 
            bool isValid = false;
            for (int l = 0; l < chrTirage.Length; l++)
            {
                if ((chrTirage[l] == Letter ||
                        chrTirage[l] == '?')
                    && CheckLetters[l] == 0)
                {
                    isValid = true;
                    break;
                }
            }

            if (isValid == true)
            {
                strCurrent += Letter.ToString();
            }
            chrCurrent = strCurrent.ToCharArray();
            ResetLetters();
        }

        public void ResetLetters()
        {
            int posLetter = 0;
            int[] CheckLetters = new int[intNbLetters];

            // We First Check Characters already Played 
            for (int xx = 0; xx < intNbLetters; xx++)
                CheckLetters[xx] = 0;

            foreach (char c in chrCurrent)
            {
                objLetters[posLetter].SetColor(true);
                objLetters[posLetter].SetLetter(c.ToString());
                chrPlaced[posLetter] = c;

                bool Ok = false;
                for (int x = 0; x < intNbLetters; x++)
                {
                    if (chrTirage[x] == c && CheckLetters[x] == 0)
                    {
                        CheckLetters[x] = 1;
                        Ok = true;
                        break;
                    }

                }

                if (Ok == false)
                {
                    for (int x = 0; x < intNbLetters; x++)
                    {
                        if (chrTirage[x] == '?' && CheckLetters[x] == 0)
                        {
                            CheckLetters[x] = 1;
                            break;
                        }
                    }
                }
                objLetters[posLetter].Paint();
                posLetter++;


            }

            for (int l = 0; l < chrTirage.Length; l++)
            {
                if (CheckLetters[l] == 0)
                {
                    objLetters[posLetter].SetColor(false);
                    objLetters[posLetter].SetLetter(chrTirage[l].ToString());
                    chrPlaced[posLetter] = chrTirage[l];
                    objLetters[posLetter].Paint();
                    posLetter++;

                }
            }

            PaintTirage();
          //  this.Invalidate();

        }
        
        public void SetTirage(string Tirage, string Current)
        {
            strTirage = Tirage;

            chrTirage = Tirage.ToCharArray();
            chrPlaced = Tirage.ToCharArray();

            for (int x = 0; x < strTirage.Length; x++)
            {
                if (strTirage[x] >= 'a' && strTirage[x] <= 'z')
                {
                    chrTirage[x] = '?';
                    chrPlaced[x] = '?';
                }
            }

            if (Current != null)
                strCurrent = Current;
            else
                strCurrent = "";

            chrCurrent = strCurrent.ToCharArray();

            intNbLetters = strTirage.Length;

            InitRack();
        }

        public void InitRack()
        {
            try
            {
                if (intNbLetters == 0 || this.Width == 0) return;

                int maxWidth = (this.Width) / intNbLetters;
                maxWidth -= 4;

                if (maxWidth < this.Height)
                    intHeight = maxWidth;
                else
                    intHeight = this.Height;

                if (intHeight == this.Height) intHeight -= 6;

                intFirstY = (this.Height - intHeight) / 2;
                intFirstX = (this.Width - ((intHeight * intNbLetters) + 5 * (intNbLetters - 1))) / 2;

                this.SuspendLayout();

                if (objLetters == null)
                {
                    objLetters = new LetterPlacer[15];
                    for (int x = 0; x < 15; x++)
                    {
                        objLetters[x] = new LetterPlacer("");
                        objLetters[x].Visible = false; 
                       // this.Controls.Add(objLetters[x]);
                    }
                }
                else
                {
                    for (int x = 0; x < 15; x++)
                    {
                        objLetters[x].Visible = false;
                    }
                                    
                }

                for (int x = 0; x < intNbLetters; x++)
                {
                    objLetters[x].SetLetterPlacer(chrTirage[x].ToString(), "0", intFirstX + (intHeight + 5) * x, intFirstY, intHeight, intHeight, true);  
                    //objLetters[x].SetLetter(chrTirage[x].ToString());
                    objLetters[x].BackColor = this.BackColor;
                    //objLetters[x].Left = intFirstX + (intHeight + 5) * x;
                    //objLetters[x].Top = intFirstY;
                    //objLetters[x].Height = intHeight;
                    //objLetters[x].Width = intHeight;
                    objLetters[x].SetColor(false);
                    objLetters[x].Visible = true;
                    objLetters[x].Paint();
                }
            }
            catch
            {
                MessageBox.Show(this, "une erreur s'est produite... Bug de windows ! Arrêtez la partie.", "Error", MessageBoxButtons.OK);
            }
            finally
            {
                this.ResumeLayout(); 
            }

            PaintTirage();

        }
        
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {

            if (bmpBuffer != null)
            {
                bmpBuffer.Dispose(); 
                gBuffer.Dispose();

                bmpBuffer = null;
                gBuffer.Dispose();
            }

            if (objLetters != null)
            {
                for (int x = 0; x < 15; x++)
                {
                    objLetters[x].Dispose();
                }
                objLetters = null;
            }


            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WordPlayer
            // 
            this.AllowDrop = true;
            this.Name = "WordPlayer";
            this.Size = new System.Drawing.Size(560, 56);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.this_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.this_DragOver);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.this_MouseMove);
            this.Resize += new System.EventHandler(this.WordPlayer_Resize);
            this.ResumeLayout(false);

        }
        #endregion

        private void WordPlayer_Resize(object sender, System.EventArgs e)
        {
            if (bmpBuffer != null)
            {
                gBuffer.Dispose();
                bmpBuffer.Dispose();

                gBuffer = null;
                bmpBuffer = null;
            }

            InitRack();
        }

        Bitmap bmpBuffer = null;
        Graphics gBuffer = null;
        private void PaintTirage()
        {
            if (bmpBuffer == null)
            {
                bmpBuffer = new Bitmap(this.Width, this.Height);
                gBuffer = Graphics.FromImage(bmpBuffer);
            }

            try
            {
                gBuffer.Clear(this.BackColor);

                for (int x = 0; x < intNbLetters; x++)
                {
                    LetterPlacer lp = objLetters[x];
                    gBuffer.DrawImage(lp.GetBmp(), lp.Left, lp.Top);
                }

                /*this.BackgroundImageLayout = ImageLayout.None;
                this.BackgroundImage = bmpBuffer;*/


                this.Invalidate();
                Application.DoEvents();
            }
            catch
            {
                ;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (bmpBuffer != null)
            {
                e.Graphics.DrawImage(bmpBuffer, 0, 0);
            }
           //base.OnPaintBackground(e);
        }
    }


}
