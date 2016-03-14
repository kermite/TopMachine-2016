using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg;
using TopMachine.Desktop.Overall;
using CMWA.Packager;
using System.IO;

namespace TopMachine.Topping.Engine.GUI.Board
{
    public partial class Board : UserControl, IBoardManager, IKeyHandler
    {
        #region Private properties

        CArrow arrow;

        bool blnMouseDown = false;
        bool blnDrawingLine = false;

        int iDirection = 0;

        int[,] rowcolStates;
        int[,] rowcolPoints;
        char[] chrTirage;
        bool[] chrPlaced;
        bool[] chrJoker;

        int LastX = 0, LastY = 0;
        int iCaseH = 0, iCaseV = 0;

        int iFirstPlayedX = 0;
        int iFirstPlayedY = 0;

        int iFirstDrawX = 0;
        int iFirstDrawY = 0;

        int iCaseEditH = 0, iCaseEditV = 0;

        int nbv, nbh;

        string[,] LettersPlaced;
        char[,] Letters;
        int[,] Status;
        int[,] Light;
        Color BackGround = Color.White;
        Color Grid = Color.Gray;

        Color[,] LetterColors = new Color[30, 2];
        int Margin = 20;

        Rectangle grid;
        DateTime ts;
        GameCfg gc;

        #endregion

        // bool activate;
        SafeBitmap bmpBuffer;
        SafeBitmap bmpBackground;
        bool redraw = true;
        Guid MemoryCheckId;

        #region Public properties and events

        public int[] Tiles_points;

        public event OnWordUpdateEvent OnWordUpdate;

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (bmpBuffer != null)
            {
                bmpBuffer.Dispose();
                bmpBuffer = null;
            }

            if (bmpBackground != null)
            {
                bmpBackground.Dispose();
                bmpBackground = null; 
            }

            MemoryCheck.Unregister(MemoryCheckId);

            if (disposing && (components != null))
            {

                components.Dispose();
            }

            base.Dispose(disposing);
        }


        public GameCfg GameConfig
        {
            set { gc = value; }
            get { return gc; }
        }

        public Control ResultControl
        {
            get { return this; }
        }

        public Board(GameCfg cfg)
        {
            InitializeComponent();
            gc = cfg;
            Init();


            // this.DoubleBuffered = true;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserMouse, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.StandardClick, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);
            this.UpdateStyles();
            MemoryCheckId = MemoryCheck.Register(this, "Board CREATE");

            // Get Random BackGround
            string dir = FileUtility.GetFile("Background", LocationType.PersonalFiles);

            if (Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir);


                if (files != null && files.Length > 0)
                {
                    int c = (new Random()).Next(files.Length);
                    string fn = files[c];

                    Image img = Bitmap.FromFile(fn);
                    Bitmap bmp = new Bitmap(img);
                    bmpBackground = new SafeBitmap(bmp, this);
                    img.Dispose();
                }
            }

        }

        public Board()
        {
            InitializeComponent();
            gc = new GameCfg();

            // this.DoubleBuffered = true;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserMouse, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.StandardClick, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);
            this.UpdateStyles();
            MemoryCheckId = MemoryCheck.Register(this, "Board CREATE");

            // Get Random BackGround
            string dir = FileUtility.GetFile("Background", LocationType.PersonalFiles);

            if (Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir);

                if (files != null && files.Length > 0)
                {
                    int c = (new Random()).Next(files.Length);
                    string fn = files[c];

                    Image img = Bitmap.FromFile(fn);
                    Bitmap bmp = new Bitmap(img);
                    bmpBackground = new SafeBitmap(bmp, this);
                    img.Dispose();
                }
            }
       
        }


        private void Init()
        {
            Margin = 10;

            nbv = gc.GridConfig.dimY;
            nbh = gc.GridConfig.dimX;

            LettersPlaced = new string[nbv + 1, nbh + 1];
            Letters = new char[nbv + 1, nbh + 1];
            Status = new int[nbv + 1, nbh + 1];
            Light = new int[nbv + 1, nbh + 1];

            if (gc.Config.TypeOfGame == 1)
            {
                Margin = 0;
            }

            for (int x = 0; x < gc.GridConfig.NbLetters; x++)
            {
                char c = gc.GridConfig.GetChar(x);
                if (x == 0)
                {
                    LetterColors[x, 0] = Color.Red;
                }
                else
                {
                    LetterColors[x, 0] = Color.Black;
                }


                LetterColors[x, 1] = Color.FromArgb(233, 203, 158);
            }

            rowcolStates = new int[gc.GridConfig.dimX, 2];
            rowcolPoints = new int[gc.GridConfig.dimX, 2];

            Tiles_points = DicoTools.GetDicoPoints(gc.Config.Language);
            // activate = true;

        }



        #region Boad Management

        public void Reset()
        {
            LettersPlaced = new string[nbv + 1, nbh + 1];
            Letters = new char[nbv + 1, nbh + 1];
            Status = new int[nbv + 1, nbh + 1];
            Light = new int[nbv + 1, nbh + 1];

            redraw = true;
        }


        public void SetWordTmp(string strWord, int[] orig, int posv, int posh, int direction)
        {
            int firsty = posv;
            int firstx = posh;

            RemoveTmp(false);
            strWord = strWord.ToUpper();
            for (int x = 0; x < strWord.Length; x++)
            {
                char c = strWord[x];

                if (Letters[firsty, firstx] == 0)
                {
                    Letters[firsty, firstx] = c;
                    Status[firsty, firstx] = 2;
                }

                if ((orig[x] & 4) == 4)
                    Status[firsty, firstx] = Status[firsty, firstx] | 0x04;

                if (direction == 1)
                    firstx++;
                else
                    firsty++;

                if (firstx == nbh || firsty == nbv) break;
            }

            //redraw = true;
            this.Invalidate();
        }

        public void RemoveAll(bool doredraw)
        {
            for (int x = 0; x < nbh; x++)
            {
                for (int y = 0; y < nbv; y++)
                {
                    Letters[x, y] = (char)0;
                    Status[x, y] = 0;
                }
            }

            if (doredraw)
            {
                redraw = true;
                this.Invalidate();
            }
        }

        public bool RemoveTmp(bool doredraw)
        {// la variable change permet de savoir si une des lettres du board n'est pas fixe 
            bool change = false;
            for (int x = 0; x < nbh; x++)
            {
                for (int y = 0; y < nbv; y++)
                {
                    if ((Status[x, y] & 2) == 2)
                    {
                        Letters[x, y] = (char)0;
                        Status[x, y] = 0;
                        change = true;
                    }
                }
            }

            if (change) ResetLetters();

            if (doredraw)
            {
                //                redraw = true; 
                this.Invalidate();
            }
            return change;
        }

        public void SetWord(CRound round)
        {
            string word = round.getword();
            //lvSolutions.Items[0].Text;

            int[] origin = new int[word.Length];
            for (int x = 0; x < word.Length; x++)
            {
                origin[x] = round.Round.tileorigin[x];
            }

            SetWord(word, origin, round.row() - 1, round.column() - 1, round.dir());

        }

        public void SetWordTmp(CRound round)
        {
            string word = round.getword();

            int[] origin = new int[word.Length];
            for (int x = 0; x < word.Length; x++)
            {
                origin[x] = round.Round.tileorigin[x];
            }

            SetWordTmp(word, origin, round.row() - 1, round.column() - 1, round.dir());
        }

        public void SetWord(string strWord, int[] orig, int posv, int posh, int direction)
        {
            int firsty = posv;
            int firstx = posh;

            strWord = strWord.ToUpper();
            for (int x = 0; x < strWord.Length; x++)
            {
                char c = strWord[x];

                Letters[firsty, firstx] = c;
                if (Status[firsty, firstx] == 0)
                {
                    Status[firsty, firstx] = 1;
                }

                if ((orig[x] & 4) == 4)
                    Status[firsty, firstx] = Status[firsty, firstx] | 0x04;

                if (direction == 1)
                    firstx++;
                else
                    firsty++;

                if (firstx == nbh || firsty == nbv) break;
            }

            redraw = true;
            this.Invalidate();

            if ((int)gc.GameState < (int)enuGameState.Finished)
            {
                ;// Application.DoEvents();
            }
        }

        public bool LoadArrow(bool doRenew, bool doRenewPos, int dir)
        {
            if (arrow == null)
            {
                arrow = new CArrow();
                arrow.BackColor = Color.SlateGray;
                arrow.TabStop = false;
            }



            if (doRenew)
            {
                if (!grid.Contains(new Point(LastX, LastY)))
                {
                    return false;
                }

                iCaseH = (int)((double)((LastX - grid.Left) / Stepw));
                iCaseV = (int)((double)((LastY - grid.Top) / Stepw));
            }

            if (iCaseH >= 0 && iCaseV >= 0 && iCaseH < nbh && iCaseV < nbv)
            {
                int ii = Light[iCaseV, iCaseH];
                /* if (ii < 2)
                 {
                     arrow.BackColor = gc.GridConfig.GetColor(iCaseV + 1, iCaseH + 1);
                 }
                 else
                 {
                     arrow.BackColor =Color.DarkGray;
                 }*/


                Rectangle r = CreateRectangle((float)grid.Left + (int)(Stepw * iCaseH), grid.Top + (int)(Steph * iCaseV), Stepw - 1, Steph - 1);

                arrow.Left = r.Left;
                arrow.Top = r.Top;
                arrow.Width = r.Width;
                arrow.Height = r.Height;

                if ((Status[iCaseV, iCaseH] & 1) == 1)
                {
                    if (arrow != null)
                    {
                        Controls.Remove(arrow);
                        arrow.OnChangeDirection -= new CArrow.OnChangeDirectionEvent(ChangeDir);
                        arrow.Dispose();
                        arrow = null;
                    }
                    return true;
                }

                if (dir == -1) dir = iDirection;
                // *** 
                arrow.Direction = dir;
                iDirection = dir;
                arrow.OnChangeDirection += new CArrow.OnChangeDirectionEvent(ChangeDir);

                if (!Controls.Contains(arrow))
                {
                    Controls.Add(arrow);
                }
                arrow.Refresh();


                if (doRenewPos)
                {
                    iFirstPlayedX = iCaseH;
                    iFirstPlayedY = iCaseV;
                    if (RemoveTmp(true))
                    {
                        UpdateWord(false, false);
                    }
                }
                return true;
            }

            /*if (arrow != null) Controls.Remove(arrow);
            arrow = null;*/

            return true;
        }


        private void ChangeDir(int Direction)
        {
            iDirection = Direction;
            RemoveTmp(true);
        }
        #endregion

        #region Paint functions
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                if (redraw)
                {
                    Redraw();
                    redraw = false;
                }
                e.Graphics.DrawImage(bmpBuffer.bmp, 0, 0);

            }
            catch
            {
                ;
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {

            try
            {
                grid = CreateRectangle(Margin, Margin, Width - Margin * 2, Height - Margin * 2);
                float stepw = (float)System.Math.Floor((double)grid.Width / nbv);
                float steph = (float)System.Math.Floor((double)grid.Height / nbh);
                float ww = stepw * nbh;
                float yy = steph * nbv;
                int neww = (int)((Width - ww) / 2);
                int newh = (int)((Height - yy) / 2);
                grid = CreateRectangle(neww, newh, (int)ww, (int)yy);

                float fs = (steph - 8) / e.Graphics.DpiX * 72;
                using (System.Drawing.Font FontLetter = new System.Drawing.Font("Verdana", fs, System.Drawing.FontStyle.Bold))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    for (int x = 0; x < nbv; x++)
                    {
                        for (int y = 0; y < nbh; y++)
                        {
                            if (Letters[x, y] != 0 && (Status[x, y] & 1) != 1)
                            {
                                DrawTile(x, y, e.Graphics, FontLetter, stepw, steph, sf);
                            }
                        }
                    }
                }
            }
            catch
            {
                ;
            }




        }

        float Stepw;
        float Steph;

        public void Redraw()
        {

            if (bmpBuffer == null)
            {
                bmpBuffer = new SafeBitmap(new Bitmap(this.Width, this.Height), this);
            }

            BackGround = BackColor;

            try
            {
                bmpBuffer.g.Clear(this.BackColor);

                Graphics g = bmpBuffer.g;

                using (SolidBrush b = new SolidBrush(BackColor))
                {
                    g.FillRectangle(b, 0, 0, Width, Height);
                }

                grid = CreateRectangle(Margin, Margin, Width - Margin * 2, Height - Margin * 2);

                using (Pen GridPen = new Pen(this.Grid, 1))
                {

                    float stepw = (float)System.Math.Floor((double)grid.Width / nbv);
                    float steph = (float)System.Math.Floor((double)grid.Height / nbh);

                    float ww = stepw * nbh;
                    float yy = steph * nbv;

                    int neww = (int)((Width - ww) / 2);
                    int newh = (int)((Height - yy) / 2);

                    grid = CreateRectangle(neww, newh, (int)ww, (int)yy);

                    Stepw = stepw;
                    Steph = steph;

                    if (bmpBackground != null)
                    {
                        g.DrawImage(bmpBackground.bmp, grid);
                    }
                    else
                    {
                        using (SolidBrush b = new SolidBrush(Color.White))
                        {
                            g.FillRectangle(b, grid);
                        }
                    }

                    float fs = (steph - 8) / g.DpiX * 72;
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        DisplayCoordonne(stepw, steph, g, sf);

                        float posx;
                        float posy;

                        for (int x = 0; x < nbh; x++)
                        {

                            for (int y = 0; y < nbv; y++)
                            {


                                Brush b = null;

                                int ii = Light[y, x];
                                if (ii < 2)
                                {
                                    
                                    Color tmp = gc.GridConfig.GetColor(y + 1, x + 1);
                                    if (bmpBackground != null && tmp.A > 150)
                                    {
                                        tmp = Color.FromArgb(150, tmp);
                                    }
                                    b = new SolidBrush(tmp);
                                }
                                else
                                {
                                    b = new SolidBrush(Color.DarkGray);
                                }

                                posx = stepw * x + grid.Left;
                                posy = steph * y + grid.Top;

                                g.FillRectangle(b, (int)posx, (int)posy, (int)(stepw + 1), (int)(steph + 1));

                                b.Dispose();
                            }

                        }

                        g.DrawRectangle(GridPen, grid);

                        for (float x = 0; x < nbh; x++)
                        {
                            g.DrawLine(GridPen, grid.Left, grid.Top + (int)(steph * x), grid.Right, grid.Top + (int)(steph * x));
                        }


                        for (float x = 0; x < nbv; x++)
                        {
                            g.DrawLine(GridPen, grid.Left + (int)(stepw * x), grid.Top, grid.Left + (int)(stepw * x), grid.Bottom);
                        }



                        using (System.Drawing.Font FontLetter = new System.Drawing.Font("Verdana", fs, System.Drawing.FontStyle.Bold))
                        {
                            using (System.Drawing.Font FontBoarder = new System.Drawing.Font("Verdana", (float)14, System.Drawing.FontStyle.Regular))
                            {

                                for (int x = 0; x < nbv; x++)
                                {
                                    for (int y = 0; y < nbh; y++)
                                    {
                                        if (Letters[x, y] != 0 && (Status[x, y] & 1) == 1)
                                        //if (Letters[x,y] != 0)
                                        {
                                            DrawTile(x, y, g, FontLetter, stepw, steph, sf);
                                        }
                                        else
                                        {
                                            if (LettersPlaced[x, y] != null)
                                            {
                                                if (LettersPlaced[x, y].Length > 0)
                                                {
                                                    Rectangle cr = CreateRectangle((float)grid.Left + (int)(stepw * y) + 2, grid.Top + (int)(steph * x) + 2, stepw - 4, steph - 4);
                                                    DrawLetters(g, LettersPlaced[x, y], cr, x, y);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
               // NLog.LogManager.GetLogger("wcf").ErrorException("Board : Redraw", ee);
            }
            finally
            {
                GC.Collect();
            }
        }



        private void DrawTile(int x, int y, Graphics g, Font FontLetter, float stepw, float steph, StringFormat sf)
        {

            //float size = wl;
            Rectangle r = CreateRectangle((float)grid.Left + (int)(stepw * y), grid.Top + (int)(steph * x), stepw - 1, steph - 1);
            float size = (stepw - 2) / g.DpiX * 72;
            using (Font FontScore = new System.Drawing.Font("Tahoma", size / 4, System.Drawing.FontStyle.Bold))
            {
                int colorNdx = 0;

                if ((Status[x, y] & 4) == 4)
                {
                    colorNdx = 0;
                }
                else
                {
                    colorNdx = gc.GridConfig.GetCharFromAscii(Letters[x, y]);
                }


                if ((Status[x, y] & 1) == 1)
                {
                    using (SolidBrush b = new SolidBrush(LetterColors[colorNdx, 1]))
                    {
                        g.FillRectangle(b, r);
                    }
                }
                else
                {
                    using (SolidBrush b = new SolidBrush(Color.SandyBrown))
                    {
                        g.FillRectangle(b, r);
                    }
                }


                g.DrawRectangle(new Pen(Color.FromArgb(64, 64, 64), 1), r);

                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                using (SolidBrush b = new SolidBrush(LetterColors[colorNdx, 0]))
                {
                    g.DrawString(new String(Letters[x, y], 1), FontLetter, b, r, sf);
                }

                if (!((Status[x, y] & 4) == 4))
                {

                    float w = (float)grid.Left + (int)(stepw * (y));
                    float h = grid.Top + (int)(steph * (x));

                    DisplayLetterPoint(size, x, y, w, h, stepw, steph, g);
                }
            }

        }

        private void DisplayCoordonne(float stepw, float steph, Graphics g, StringFormat sf)
        {
            using (System.Drawing.Font FontCoordonne = new System.Drawing.Font("Verdana", 6, System.Drawing.FontStyle.Bold))
            {
                using (System.Drawing.Font FontCoordonneMoreTen = new System.Drawing.Font("Verdana", 5, System.Drawing.FontStyle.Bold))
                {
                    using (SolidBrush b = new SolidBrush(Color.White))
                    {
                        float posx;
                        float posy;

                        for (int x = 0; x < nbh; x++)
                        {
                            for (int y = 0; y < nbv; y++)
                            {
                                if (x == 0)
                                {
                                    posx = (stepw * nbh) + grid.Left;
                                    posy = (steph * y) + grid.Top + (steph / 2);
                                    char c = (char)(65 + y);
                                    g.DrawString(c.ToString(), FontCoordonne, b, new RectangleF((int)posx, (int)posy, 10, 10), sf);

                                }
                            }
                            posx = (stepw * x + 1) + grid.Left + (stepw / 2);
                            posy = (steph * (nbv)) + grid.Top + 2;

                            if (x < 9)
                            {
                                g.DrawString((x + 1).ToString(), FontCoordonne, b, new RectangleF((int)posx, (int)posy, 15, 10), sf);
                            }
                            else
                            {
                                g.DrawString((x + 1).ToString(), FontCoordonneMoreTen, b, new RectangleF((int)posx, (int)posy, 15, 10), sf);
                            }
                        }
                    }
                }
            }

        }
        private void DisplayLetterPoint(float size, int x, int y, float w, float h, float stepw, float steph,  Graphics g)
        {
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Far;
                sf.LineAlignment = StringAlignment.Far;

                using (Font FontScore = new System.Drawing.Font("Tahoma", size / 4, System.Drawing.FontStyle.Bold))
                {
                    float posXLetterPts = 0, posYLetterPts = 0, withLetterPts = 0, heightLetterPts = 0;
                    int pts = Tiles_points[Letters[x, y] - 64];

                    posXLetterPts = w;
                    posYLetterPts = h;
                    withLetterPts = stepw;
                    heightLetterPts = steph;

                    /*if (pts < 10)
                    {
                        posXLetterPts = w + 15;
                        posYLetterPts = h + 15;
                        withLetterPts = stepw - 1;
                        heightLetterPts = steph - 1;

                    }
                    else
                    { // more large
                        posXLetterPts = w + 12;
                        posYLetterPts = h + 15;
                        withLetterPts = stepw - 1;
                        heightLetterPts = steph - 1;

                    }*/

                    using (SolidBrush b = new SolidBrush(Color.Gray))
                    {
                        g.DrawString(pts.ToString(), FontScore, b, new RectangleF(posXLetterPts, posYLetterPts, withLetterPts, heightLetterPts), sf);
                    }
                }
            }
        }

        private void DrawLetters(Graphics g, string str, Rectangle r, int x, int y)
        {
            float stepw = r.Width;
            float wl = 0;
            float fs = 0;
            int nbcv = 0, nbch = 0;

            int nbl = LettersPlaced[x, y].Length;
            if (nbl == 1)
            {
                wl = fs = stepw;
                nbcv = nbch = 1;
            }
            else
            {
                if (nbl < 5)
                {
                    wl = fs = stepw / 2;
                    nbcv = nbch = 2;
                }
                else
                {
                    if (nbl < 7)
                    {
                        wl = stepw / 3;
                        fs = stepw / 2;
                        nbcv = 2; nbch = 3;
                    }
                    else
                    {
                        if (nbl < 10)
                        {
                            wl = fs = stepw / 3;
                            nbcv = nbch = 3;
                        }
                    }
                }
            }


            //float size = wl;
            float size = (stepw - 2) / g.DpiX * 72;

            using (StringFormat sf = new StringFormat())
            {

                if (nbl == 2)
                {
                    size += 4;
                }



                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                float istep = (nbcv * nbch / nbl);

                using (SolidBrush b = new SolidBrush(ForeColor))
                {
                    for (float letter = 0; letter < nbl; letter++)
                    {
                        float posx = (float)System.Math.Floor(letter / nbch);
                        float posy = letter % nbch;

                        using (Font FontBoarder = new System.Drawing.Font("Verdana", (float)size, System.Drawing.FontStyle.Regular))
                        {

                            float posXLetter = 0, posYLetter = 0, withLetter = 0, heightLetter = 0;

                            posXLetter = 3;
                            posYLetter = 3;
                            withLetter = Width - 5;
                            heightLetter = Height - 5;

                            g.DrawString(str[(int)letter].ToString(), FontBoarder, b, new RectangleF(posXLetter, posYLetter, withLetter, heightLetter), sf);
                        }
                    }
                }
            }
        }
        #endregion

        #region Event Mouse

        private void Board_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LastX = e.X;
            LastY = e.Y;
        }

        int counter = 0;
        private void Board_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            blnMouseDown = false;

            bool ok = false;
            if (e.Button == MouseButtons.Left)
                ok = LoadArrow(true, true, 1);
            else
            {
                ok = LoadArrow(true, true, 0);
            }

        }
        private void Board_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LastX = e.X;
            LastY = e.Y;
        }

        private void Board_MouseLeave(object sender, System.EventArgs e)
        {
            blnMouseDown = false;
            blnDrawingLine = false;
        }
        # endregion

        public void SetTirage(string Tirage)
        {
            RemoveTmp(true);

            chrTirage = Tirage.ToCharArray();
            chrJoker = new bool[chrTirage.Length];
            chrPlaced = new bool[chrTirage.Length];

            for (int x = 0; x < chrTirage.Length; x++)
            {
                if (chrTirage[x] == '?')
                    chrJoker[x] = true;
                else
                    chrJoker[x] = false;
            }
        }

        public void ResetLetters()
        {
            if (chrTirage != null)
            {
                for (int x = 0; x < chrTirage.Length; x++)
                {
                    if (chrPlaced[x]) chrPlaced[x] = false;
                }
            }

            UpdateWord(false, true);

        }

        private void SetLetter(char key)
        {
            if (Status[iCaseV, iCaseH] != 0) return;

            if (iCaseV < nbv && iCaseH < nbh && iCaseV >= 0 && iCaseH >= 0)
            {
                if (chrTirage != null)
                {
                    if (!CheckArrow(false)) return;

                    if (key >= 'a' && key <= 'z')
                    {
                        for (int x = 0; x < chrTirage.Length; x++)
                        {
                            if (chrTirage[x] == '?' && !chrPlaced[x])
                            {
                                Letters[iCaseV, iCaseH] = (char)((int)key - 32);
                                Status[iCaseV, iCaseH] = 6;
                                chrPlaced[x] = true;
                                CheckArrow(true);
                                break;
                            }
                        }
                    }

                    if (key >= 'A' && key <= 'Z')
                    {
                        bool done = false;
                        for (int x = 0; x < chrTirage.Length; x++)
                        {
                            if (chrTirage[x] == key && !chrPlaced[x])
                            {
                                Letters[iCaseV, iCaseH] = (char)key;
                                Status[iCaseV, iCaseH] = 2;
                                chrPlaced[x] = true;
                                CheckArrow(true);
                                done = true;
                                break;
                            }
                        }

                        if (!done)
                        {
                            for (int x = 0; x < chrTirage.Length; x++)
                            {
                                if (chrTirage[x] == '?' && !chrPlaced[x])
                                {
                                    if (chrTirage[x] == key)
                                        Status[iCaseV, iCaseH] = 2;
                                    else
                                        Status[iCaseV, iCaseH] = 6;

                                    Letters[iCaseV, iCaseH] = (char)((int)key);
                                    chrPlaced[x] = true;
                                    CheckArrow(true);
                                    break;
                                }
                            }
                        }

                    }

                    if (iDirection == 1)
                    {
                        for (int x = 0; x < nbv; x++)
                        {
                            if (Status[iCaseV, x] == 2 || Status[iCaseV, x] == 6)
                            {
                                iFirstPlayedX = x;
                                break;
                            }
                        }
                        iFirstPlayedY = iCaseV;
                    }
                    else
                    {
                        iFirstPlayedX = iCaseH;
                        for (int x = 0; x < nbv; x++)
                        {
                            if (Status[x, iCaseH] == 2 || Status[x, iCaseH] == 6)
                            {
                                iFirstPlayedY = x;
                                break;
                            }
                        }
                    }

                    this.Refresh();
                    UpdateWord(false, true);
                }
            }
        }

        private bool CheckArrow(bool doMove)
        {

            if (doMove)
            {
                while ((Status[iCaseV, iCaseH]) != 0)
                {
                    if (iDirection == 1 && iCaseH < nbh)
                    {
                        iCaseH++;
                        if (iCaseH == nbh) break;
                    }

                    if (iDirection == 0 && iCaseV < nbv)
                    {
                        iCaseV++;
                        if (iCaseV == nbv) break;
                    }

                }
            }

            LoadArrow(false, false, -1);
            if ((Status[iCaseV, iCaseH] & 1) == 1)
                return false;

            return true;
        }

        private void CheckArrowBack()
        {
            bool doArrow = false;

            if (iDirection == 1 && iCaseH > 0)
            {
                iCaseH--;
            }

            if (iDirection == 0 && iCaseV > 0)
            {
                iCaseV--;
            }


            if (iDirection == 1 && iCaseH > 0)
            {
                while (iCaseH > 0 && (Status[iCaseV, iCaseH] & 1) == 1)
                {
                    iCaseH--;
                };
            }

            if (iDirection == 0 && iCaseV > 0)
            {
                while (iCaseV > 0 && (Status[iCaseV, iCaseH] & 1) == 1)
                {
                    iCaseV--;
                };
            }


            char c = Letters[iCaseV, iCaseH];

            if ((Status[iCaseV, iCaseH] & 4) == 4)
            {
                for (int x = 0; x < chrTirage.Length; x++)
                {
                    if (chrTirage[x] == '?' && chrPlaced[x])
                    {
                        chrPlaced[x] = false;
                        doArrow = true;
                        break;
                    }
                }
            }
            else
            {
                for (int x = 0; x < chrTirage.Length; x++)
                {
                    if (chrTirage[x] == c && chrPlaced[x])
                    {
                        chrPlaced[x] = false;
                        doArrow = true;
                        break;
                    }
                }
            }

            if ((Status[iCaseV, iCaseH] & 2) == 2)
            {
                Status[iCaseV, iCaseH] = 0;
                Letters[iCaseV, iCaseH] = (char)0;
                redraw = true;
                Invalidate();
            }


            if (iDirection == 1 && iCaseH > 0)
            {
                while (iCaseH > 0 && (Status[iCaseV, iCaseH - 1] & 1) == 1)
                {
                    iCaseH--;
                };
            }

            if (iDirection == 0 && iCaseV > 0)
            {
                while (iCaseV > 0 && (Status[iCaseV - 1, iCaseH] & 1) == 1)
                {
                    iCaseV--;
                };
            }

            if (iCaseV < iFirstPlayedY) iCaseV = iFirstPlayedY;
            if (iCaseH < iFirstPlayedX) iCaseH = iFirstPlayedX;


            CheckArrow(true);
            UpdateWord(false, true);
            LoadArrow(false, false, -1);


        }

        private void CheckArrowDelete()
        {
            bool doArrow = false;

            char c = Letters[iCaseV, iCaseH];

            if ((Status[iCaseV, iCaseH] & 4) == 4)
            {
                for (int x = 0; x < chrTirage.Length; x++)
                {
                    if (chrTirage[x] == '?' && chrPlaced[x])
                    {
                        chrPlaced[x] = false;
                        doArrow = true;
                        break;
                    }
                }
            }
            else
            {
                for (int x = 0; x < chrTirage.Length; x++)
                {
                    if (chrTirage[x] == c && chrPlaced[x])
                    {
                        chrPlaced[x] = false;
                        doArrow = true;
                        break;
                    }
                }
            }

            if ((Status[iCaseV, iCaseH] & 2) == 2)
            {
                Status[iCaseV, iCaseH] = 0;
                Letters[iCaseV, iCaseH] = (char)0;
                redraw = true;
                Invalidate();
            }


            CheckArrow(true);
            UpdateWord(false, true);
            LoadArrow(false, false, -1);


        }

        public void UpdateWord(bool Validate, bool refresh)
        {

            string s = "";

            int x = iFirstPlayedX;
            int y = iFirstPlayedY;

            if (x == -1 || y == -1)
            {
                RemoveTmp(true);
                return;
            }


            if (iDirection == 1)
            {
                while (x != 0 && Status[y, x - 1] != 0)
                {
                    if (x != 0)
                        x--;

                    if (x == 0)
                        break;
                }
            }

            if (iDirection == 0)
            {
                while (y != 0 && Status[y - 1, x] != 0)
                {
                    if (y != 0)
                        y--;

                    if (y == 0)
                        break;
                }
            }

            iFirstPlayedX = x;
            iFirstPlayedY = y;

            int[] TilesStatus = new int[nbh];

            int xx = 0;
            while (Status[y, x] != 0)
            {
                TilesStatus[xx] = Status[y, x]; // true; 

                s += (char)Letters[y, x];

                if (iDirection == 1)
                    x++;

                if (iDirection == 0)
                    y++;

                if (!(x >= 0 && x < nbh && y >= 0 && y < nbv))
                    break;

                //if ( (Status[y,x] & 4) == 4)

                xx++;
            }

            if (OnWordUpdate != null)
                OnWordUpdate(s, iFirstPlayedX, iFirstPlayedY, iDirection, Validate, TilesStatus, chrTirage, chrPlaced, refresh);

        }

        # region event Key
        public bool isAlt = false;

        public bool HandleKey(System.Windows.Forms.KeyEventArgs e)
        {
            bool result = false;

            if (gc.GameState == enuGameState.Playing)
            {
                switch (e.KeyData)
                {
                    case Keys.Up:
                    case Keys.Left:
                    case Keys.Down:
                    case Keys.Right:
                    case Keys.Home:
                    case Keys.PageUp:
                    case Keys.PageDown:
                    case Keys.End:
                        result = SendKeyMove(e.KeyCode);
                        break;
                }
            }

            if (result) return result;

            if (gc.GameState == enuGameState.Playing)
            {
                if (!gc.blnRoundEnd)
                {
                    if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
                    {
                        if (e.Shift == true)
                            result = SendKey(e.KeyValue + 32);
                        else
                            result = SendKey(e.KeyValue);

                        // If AlphaNumeric force HANDLEd
                        result = true;
                    }
                }

                if (e.KeyData == Keys.Delete)
                {
                    SendKey(7);
                    result = true;
                }

                if (e.KeyData == Keys.Space)
                {
                    SendKey(32);
                    result = true;
                }

                if (e.KeyData == Keys.Back)
                {
                    SendKey(8);
                    result = true;
                }

                if (e.KeyData == Keys.Return)
                {
                    SendKey(13);
                    result = true;
                }
            }

            e.Handled = result;
            return result;
        }

        private bool SendKey(int KeyCode)
        {
            if (KeyCode == 8)
            {
                CheckArrowBack();
                return true;
            }

            if (KeyCode == 7)
            {
                CheckArrowDelete();
                return true;
            }

            if (KeyCode == 13)
            {
                UpdateWord(true, true);
                return true;
            }

            if ((int)KeyCode >= 97 && (int)KeyCode <= 122)
            {
                SetLetter((char)(KeyCode));
                return true;
            }

            if ((int)KeyCode >= 65 && (int)KeyCode <= 90)
            {
                SetLetter((char)KeyCode);
                return true;
            }

            if (KeyCode == ' ')
            {
                iFirstPlayedX = iCaseH;
                iFirstPlayedY = iCaseV;

                if (iDirection == 1) iDirection = 0;
                else iDirection = 1;

                RemoveTmp(true);

                CheckArrow(false);
                return true;
            }

            return false;
        }

        private bool SendKeyMove(System.Windows.Forms.Keys key)
        {
            bool doReload = false;
            switch (key)
            {
                case Keys.Up:
                    iCaseV--;
                    if (iDirection == 1) doReload = true;
                    break;
                case Keys.Left:
                    iCaseH--;
                    if (iDirection == 0) doReload = true;
                    break;
                case Keys.Down:
                    iCaseV++;
                    if (iDirection == 1) doReload = true;
                    break;
                case Keys.Right:
                    iCaseH++;
                    if (iDirection == 0) doReload = true;
                    break;
                case Keys.Home:
                    iCaseV = iCaseH = 0;
                    doReload = true;
                    break;
                case Keys.PageUp:
                    iCaseV -= 8;
                    if (iDirection == 1) doReload = true;
                    break;
                case Keys.PageDown:
                    iCaseV += 8;
                    if (iDirection == 1) doReload = true;
                    break;
                case Keys.End:
                    iCaseV = nbv - 1;
                    if (iDirection == 1) doReload = true;
                    break;
                default:
                    return false;
            }

            if (iCaseV < 0)
                iCaseV = 0;
            if (iCaseH < 0)
                iCaseH = 0;
            if (iCaseH == nbh)
                iCaseH = nbh - 1;
            if (iCaseV == nbv)
                iCaseV = nbv - 1;

            if (doReload)
            {
                if (RemoveTmp(true))
                {
                    UpdateWord(false, false);
                }
                CheckArrow(false);
            }
            else
            {
                UpdateWord(false, false);
                LoadArrow(false, false, -1);
            }

            return true;


        }

        # endregion




        public bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                // Add the list of special keys that you want to handle 
                case Keys.Tab:
                    return true;
                case Keys.Down:
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                    return true;
                default:
                    // ****
                    // return base.IsInputKey(keyData);
                    return true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            RemoveTmp(true);
            UpdateWord(false, false);
        }

        protected override void OnResize(EventArgs e)
        {
            if (arrow != null)
            {
                Controls.Remove(arrow);
                arrow.OnChangeDirection -= new CArrow.OnChangeDirectionEvent(ChangeDir);
                arrow.Dispose();
                arrow = null;
            }

            redraw = true;
            if (bmpBuffer != null)
            {
                bmpBuffer.Dispose();
                bmpBuffer = null;
            }
            this.Refresh();
            base.OnResize(e);

        }

        private void Board_Leave(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private Rectangle CreateRectangle(float x, float y, float w, float h)
        {
            return new Rectangle((int)x, (int)y, (int)w, (int)h);
        }

        private void Board_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
