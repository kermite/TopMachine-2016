using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TopMachine.Desktop.Controls;
using TopMachine.Desktop.Overall;
using CMWA.Packager.Custom;
using TopMachine.Training.DAL.fdbo;
using TopMachine.Desktop.Dico;
using TopMachine.Topping.Dawg;
using System.Collections;
using TopMachine.Topping.DAL;
using System.IO;


namespace TopMachine.Training.FrontEnd
{
    public partial class TrainingGame : UserControl, IFinished, IKeyHandler
    {
        private const string _PathList = "Training4dbo\\Lists";

        private string pathOfList 
        {
            get 
            {
                return _PathList + "\\" + packageKey;
            }
        }
        public TrainingGame()
        {
            InitializeComponent();

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

        }

        DicoAccessor dr = new DicoAccessor();
        string packageKey;
        Config config;
        ListConfig listConfig;
        GameConfig gameConfig;
        long totalWords = 0;

        Dictionary<int, WordStatistic> Statistics = new Dictionary<int, WordStatistic>();
        List<string> WordsToPlay = null;

        private Word currentWordDto;
        private Word lastWordDto;

        private WordList currentWordList;
        private List<string> WordsMissed;
        private bool canSave = true;
        private WordList currentWordsPlayed;
        private Package packageCurrent;
        private bool isPlaying;

        private float totalPlayed = 0, totalFound = 0, totalLost = 0;


        ListAccessor la = null;
        public void GetEntity()
        {
            la = new ListAccessor(pathOfList);
            btnSnapshot.Enabled = WordsSnapshot.isExist(pathOfList);
            
        }

        public void SetConfig(Package package)
        {
            packageCurrent = package; 

            packageKey = package.Key;

            GetEntity();

            config = la.GetConfig();

            listConfig = new TopMachine.Desktop.Overall.ObjectSerializer<ListConfig>().Deserialize(config.XMLConfig);
            gameConfig = new TopMachine.Desktop.Overall.ObjectSerializer<GameConfig>().Deserialize(config.XMLPlay);

            Statistics.Clear();

            totalWords = 0;

            for (int x = 0; x < 4; x++)
            {
                Statistics.Add(x, new WordStatistic());
            }

            foreach (WordStatistic gws in la.GetWordStatistics())
            {
                totalWords += gws.Counter;
                Statistics[gws.Status] = gws;
            }

            ResetStatistics();
        }

        private void ResetStatistics()
        {


            lblNbWords.Text = totalWords.ToString();

            if (totalWords > 0)
            {
                lblNbWordsNJ.Text = Statistics[0].Counter.ToString();
                lblNbWordsNJP.Text = (Statistics[0].Counter * 100 / totalWords).ToString();

                lblNbWordsT.Text = Statistics[1].Counter.ToString();
                lblNbWordsTP.Text = (Statistics[1].Counter * 100 / totalWords).ToString();

                lblNbWordsP.Text = Statistics[2].Counter.ToString();
                lblNbWordsPP.Text = (Statistics[2].Counter * 100 / totalWords).ToString();

                lblNbWordsI.Text = Statistics[3].Counter.ToString();
                lblNbWordsIP.Text = (Statistics[3].Counter * 100 / totalWords).ToString();
            }

            lbScoreT.Text = totalFound.ToString();
            lbScoreM.Text = totalLost.ToString();
            lbScoreTP.Text = "0%";
            lbScoreMP.Text = "0%";

            if (totalPlayed > 0)
            {
                lbScoreTP.Text = String.Format("{0:0.00}", (totalFound / totalPlayed) * 100);
                lbScoreMP.Text = String.Format("{0:0.00}", (totalLost / totalPlayed) * 100);
            }
        }


        private ImageButton CreateButtonSolution(string tirage, string word)
        {
            ImageButton btnSolution = new ImageButton();
            btnSolution.AutoSize = true;
            btnSolution.BackColor = System.Drawing.Color.Khaki;
            btnSolution.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            btnSolution.CenterColor = System.Drawing.Color.White;
            btnSolution.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            btnSolution.FlatAppearance.BorderSize = 0;
            btnSolution.FocusDrawn = false;
            btnSolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnSolution.ForeColor = System.Drawing.Color.LightGray;
            btnSolution.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            btnSolution.Location = new System.Drawing.Point(3, 3);
            btnSolution.Name = "btnSolution";
            btnSolution.OverColor = System.Drawing.Color.Transparent;
            btnSolution.RecessDepth = 0;
            btnSolution.Round = 5;
            btnSolution.Size = new System.Drawing.Size(63, 23);
            btnSolution.TabIndex = 12;
            btnSolution.Text = tirage;
            btnSolution.UseVisualStyleBackColor = false;
            btnSolution.Tag = word;
            return btnSolution;
        }


        #region IKeyHandler Members

        public bool HandleKey(KeyEventArgs e)
        {
            bool handled = false;


            System.Windows.Forms.Keys ks = e.KeyCode;

            if (!(e.Alt == false && e.Shift == false && e.Control == false))
            {
                return false;
            }


            switch (ks)
            {

                case System.Windows.Forms.Keys.F6:
                    Isolate();
                    handled = true;
                    break;
                case System.Windows.Forms.Keys.F9:
                    SetNoPlayList();
                    handled = true;
                    break;
            }

            if (isPlaying == true)
            {
                switch (ks)
                {
                    case System.Windows.Forms.Keys.F1:
                        chevalet1.ShuffleWord();
                        handled = true;
                        break;

                    case System.Windows.Forms.Keys.F2:
                        CorrectAll(false);
                        handled = true;
                        break;
                    case System.Windows.Forms.Keys.F3:
                        CorrectAll(true);
                        handled = true;
                        break;
                    case System.Windows.Forms.Keys.F6:
                        Isolate();
                        handled = true;
                        break;

                    case System.Windows.Forms.Keys.F9:
                        SetNoPlayList();
                        handled = true;
                        break;


                    case System.Windows.Forms.Keys.Back:
                        chevalet1.SetBack();
                        handled = true;
                        break;
                    case System.Windows.Forms.Keys.Enter:
                        SetWord();
                        Correct(true);
                        handled = true;

                        break;
                    default:
                        string a = ks.ToString().ToUpper();
                        if (a.Length == 1)
                        {
                            if (a[0] >= 'A' && a[0] <= 'Z')
                            {
                                chevalet1.SetLetter(a[0]);
                                handled = true;
                            }
                        }
                        break;
                }
            }

            e.Handled = handled;
            return e.Handled;
        }

        private bool SetWord()
        {
            if (newTirage)
            {
                LoadWords();
                newTirage = false;
            }

            string current = chevalet1.Current;
            if (currentWordList.Words.Contains(current))
            {
                if (!currentWordsPlayed.Words.Contains(current))
                {
                    currentWordsPlayed.Words.Add(current);
                }

                foreach (ImageButton b in solutionsPanel.Controls)
                {
                    if (b.Tag.ToString() == current)
                    {
                        b.BackColor = Color.DarkGreen;
                        b.Text = b.Tag.ToString();
                    }
                }
                chevalet1.SetTirage(currentWordDto.Rack, "");
                return true;
            }

            chevalet1.SetTirage(currentWordDto.Rack, "");
            return false;

        }

        private void Correct(bool doCorrectAll)
        {
            if (newTirage)
            {
                LoadWords();
                newTirage = false;
            }

            bool ok = true;
            foreach (ImageButton b in solutionsPanel.Controls)
            {
                if (b.Tag.ToString() != b.Text)
                {
                    ok = false;
                }
            }

            if (ok)
            {
                foreach (Control c in dicoPanel.Controls)
                {
                    c.Visible = true;
                }
            }

            if (ok)
            {

                currentWordDto.Total++;
                currentWordDto.Succeeded++;
                totalFound++;
                CheckStatus(currentWordDto, ok);

                ChangeStatus(currentWordDto.Status);
                

                lblWordRack.Text = currentWordDto.Rack;
                lblWordTotal.Text = "# joués : " + currentWordDto.Total.ToString();
                lblWordFound.Text = "# trouvés : " + currentWordDto.Succeeded.ToString();
                lblWordLost.Text = "# perdus : " + currentWordDto.Lost.ToString();

                la.Save(currentWordDto);

                ShowSolutionDico();
                Display7p1(currentWordDto.Rack);

                SetTirage();

            }
            else
            {
                ;
            }

        }

        private bool ChangeStatus(int status) 
        {
            switch (status)
            {
                case (int)Tools.TypeStatus.NonJoue:
                    lblStatus.BackColor = Color.Gray;
                    lblStatus.Text = "Nouveau";
                    break;
                case (int)Tools.TypeStatus.Trouve_Tous:
                    lblStatus.BackColor = Color.DarkGreen;
                    lblStatus.Text = "Réussi";
                    break;
                case (int)Tools.TypeStatus.NonTrouve:
                    lblStatus.BackColor = Color.DarkRed;
                    lblStatus.Text = "Raté";
                    return false;
                    
                case (int)Tools.TypeStatus.Isole:
                    lblStatus.BackColor = Color.Orange;
                    lblStatus.Text = "Isolé";
                    return false;
                   
            }
            return true;
        }

        private void CorrectAll(bool forceok)
        {
            if (newTirage)
            {
                LoadWords();
                newTirage = false;
            }

            bool ok = true;

            if (!forceok)
            {
                foreach (ImageButton b in solutionsPanel.Controls)
                {
                    if (b.Tag != b.Text)
                    {
                        ok = false;
                        b.BackColor = Color.DarkRed;
                        b.Text = b.Tag.ToString();
                    }
                }
            }
            else
            {
                foreach (ImageButton b in solutionsPanel.Controls)
                {
                    if (b.Tag != b.Text)
                    {
                        ok = true;
                        b.BackColor = Color.DarkGreen;
                        b.Text = b.Tag.ToString();
                    }
                }
            }


            ShowSolutionDico();

            if (ok)
            {
                totalFound++;
                currentWordDto.Total++;
                currentWordDto.Succeeded++;
                CheckStatus(currentWordDto, ok);
            }
            else
            {
                totalLost++;
                currentWordDto.Total++;
                currentWordDto.Succeeded = 0;
                currentWordDto.Lost++;
                CheckStatus(currentWordDto, ok);
            }
            bool valid = true;

            valid = ChangeStatus(currentWordDto.Status);

            if (!valid && canSave && !forceok )
            {
                WordsMissed.Add(currentWordDto.Rack);
            }

            lblWordRack.Text = currentWordDto.Rack;
            lblWordTotal.Text = "# joués : " + currentWordDto.Total.ToString();
            lblWordFound.Text = "# trouvés : " + currentWordDto.Succeeded.ToString();
            lblWordLost.Text = "# perdus : " + currentWordDto.Lost.ToString();

            Display7p1(currentWordDto.Rack);

            if (canSave)
            {
                la.Save(currentWordDto);
            }
            SetTirage();
        }


        private void CheckStatus(Word word, bool ok)
        {
            if (!ok)
            {
                if (word.Status < 2)
                {
                    Statistics[word.Status].Counter--;
                    word.Status = 2;
                    Statistics[2].Counter++;
                }
            }
            else
            {
                if (word.Status == 0)
                {
                    Statistics[0].Counter--;
                    word.Status = 1;
                    Statistics[1].Counter++;
                }
                else
                {
                    if (word.Status == 2)
                    {
                        if (gameConfig.Suppress != 0 && word.Succeeded >= gameConfig.Suppress)
                        {
                            Statistics[2].Counter--;
                            word.Status = 1;
                            Statistics[1].Counter++;
                        }
                    }
                }
            }

            ResetStatistics();
        }

        private void SetNoPlayList()
        {
            if (lastWordDto != null)
            {
                if (lastWordDto.Status != 4)
                {
                    int intOldPos = lastWordDto.Status;

                    Statistics[intOldPos].Counter--;
                    lastWordDto.Status = 4;
                    la.Save(lastWordDto);
                    ResetStatistics();
                }
            }
        }

        private void Isolate()
        {

            if (lastWordDto != null)
            {
                if (lastWordDto.Status != 3)
                {
                    int intOldPos = lastWordDto.Status;

                    Statistics[intOldPos].Counter--;
                    Statistics[3].Counter++;
                    lastWordDto.Status = 3;
                    ChangeStatus(lastWordDto.Status);
                    la.Save(lastWordDto);
                    ResetStatistics();
                    WordsMissed.Add(lastWordDto.Rack);
                }
              

            }
        }
        #endregion

        #region IFinished Members

        public event OnFinishedDelegate OnFinished;

        #endregion


        private void LoadStat(int[] total) 
        {
            totalPlayed = totalFound = totalLost = 0;
            int nb = 0;
            if (gameConfig.TotalRounds == 0) gameConfig.TotalRounds = 10000;

            switch (gameConfig.TypeOfPlay)
            {
                case (int)Tools.TypeStatus.NonJoue:
                    total[(int)Tools.TypeStatus.NonJoue] = gameConfig.TotalRounds;
                    break;
                case (int)Tools.TypeStatus.Trouve_Tous:
                    total[(int)Tools.TypeStatus.NonJoue] = gameConfig.TotalRounds * 5 / 100;
                    total[(int)Tools.TypeStatus.Trouve_Tous] = gameConfig.TotalRounds * 15 / 100;
                    total[(int)Tools.TypeStatus.NonTrouve] = gameConfig.TotalRounds * 40 / 100;
                    total[(int)Tools.TypeStatus.Isole] = gameConfig.TotalRounds * 40 / 100;
                    
                    break;
                case (int)Tools.TypeStatus.NonTrouve:
                    total[(int)Tools.TypeStatus.NonTrouve] = gameConfig.TotalRounds;
                    break;
                case (int)Tools.TypeStatus.Isole:
                    total[(int)Tools.TypeStatus.Isole] = gameConfig.TotalRounds;
                    break;
                case (int)Tools.TypeStatus.NonTrouveIsole:
                    total[(int)Tools.TypeStatus.NonTrouve] = gameConfig.TotalRounds * 70 / 100;
                    total[(int)Tools.TypeStatus.Isole] = gameConfig.TotalRounds * 30 / 100;
                    break;
            }

            for (int x = 0; x < 4; x++)
            {
                nb += total[x];
            }

            nb = gameConfig.TotalRounds - nb;
            if (nb > 0)
            {
                switch (gameConfig.TypeOfPlay)
                {
                    case 2:
                        total[(int)Tools.TypeStatus.NonJoue] += nb;
                        break;
                    case 4:
                        total[(int)Tools.TypeStatus.NonTrouve] += nb;
                        break;
                }
            }
        }

        private void ChooseRandomWords(int[] total) 
        {
            WordsToPlay = new List<string>();

            for (int x = 0; x < 4; x++)
            {
                WordStatistic ws = Statistics[x];

                if (ws.Words != null)
                {
                    WordsToPlay.AddRange(Randomize(ws.Words, total[x]));
                }
            }

            WordsToPlay = Randomize(WordsToPlay, WordsToPlay.Count);
            WordsMissed = new List<string>();

        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            btnSnapshot.Tag = null;
            
            if (la != null) {
                la.Dispose();
            }
            SetConfig(packageCurrent);

            btnReview.Enabled = false;
            btnSnapshot.Enabled = true;
            btnSnapshot.Text = "sauver l'etat";
            ResetStatistics();

            int[] total = new int[4];

            LoadStat(total);
            ChooseRandomWords(total);
           
            isPlaying = true;
            SetTirage();
          

        }

        bool newTirage = true;
        public void SetTirage()
        {
            if (lastWordDto != null)
            {
                listWords.Items.Add(lastWordDto.Rack);
            }

            if (WordsToPlay.Count == 0)
            {
                MessageBox.Show("La partie est finie, ou aucun tirage est disponible", "Erreur", MessageBoxButtons.OK);
                return;
            }
            totalPlayed++;
            lastWordDto = currentWordDto;

            Random rnd = new Random();
            int ndx = rnd.Next(WordsToPlay.Count);

            string currentWord = WordsToPlay[ndx];
            WordsToPlay.RemoveAt(ndx);
            currentWordDto = la.GetWordByRack(currentWord);
            currentWordList = new TopMachine.Desktop.Overall.ObjectSerializer<WordList>().Deserialize(currentWordDto.WordsXML);
            currentWordsPlayed = new WordList();
            chevalet1.SetTirage(currentWord, "");

            newTirage = true;

        }

        private void LoadWords()
        {
            ChangeStatus(currentWordDto.Status);
            
            lblWordRack.Text = currentWordDto.Rack;
            lblWordTotal.Text = "# joués : " + currentWordDto.Total.ToString();
            lblWordTotal.Text = "# trouvés : " + currentWordDto.Succeeded.ToString();
            lblWordTotal.Text = "# perdus : " + currentWordDto.Lost.ToString();


            foreach (Control ctl in solutionsPanel.Controls)
            {
                ctl.Dispose();
            }

            solutionsPanel.Controls.Clear();

            foreach (string w in currentWordList.Words)
            {
                solutionsPanel.Controls.Add(CreateButtonSolution(currentWordDto.Rack, w));
            }


        }

        private void ShowSolutionDico(string word)
        {
            List<TopMachine.Topping.DAL.Dico> dictionnary = new List<TopMachine.Topping.DAL.Dico>();

            dictionnary = dr.GetWord(word,false);

            this.dicoPanel.SuspendLayout();

            foreach (Control ctl in dicoPanel.Controls)
            {
                ctl.Dispose();
            }

            dicoPanel.Controls.Clear();

            for
                (int x = 0; x < dictionnary.Count; x++)
            {
                DicoDefinition.GetDefinition(dictionnary[x]);
                Def d = new Def(dictionnary[x]);
                d.Visible = true;
                dicoPanel.Controls.Add(d);
            }

            foreach (Def d in dicoPanel.Controls)
            {
                d.ResizeDef();
            }

            dicoPanel.ResumeLayout();
        }


        private void ShowSolutionDico()
        {
            List<TopMachine.Topping.DAL.Dico> dictionnary = new List<TopMachine.Topping.DAL.Dico>();

            dictionnary = dr.GetWords(currentWordList.Words);

            this.dicoPanel.SuspendLayout();

            foreach (Control ctl in dicoPanel.Controls)
            {
                ctl.Dispose();
            }

            dicoPanel.Controls.Clear();

            for
                (int x = 0; x < dictionnary.Count; x++)
            {
                DicoDefinition.GetDefinition(dictionnary[x]);
                Def d = new Def(dictionnary[x]);
                d.Visible = true;
                dicoPanel.Controls.Add(d);
            }

            foreach (Def d in dicoPanel.Controls)
            {
                d.ResizeDef();
            }

            dicoPanel.ResumeLayout();
        }

        private List<string> Randomize(List<string> words, int c)
        {
            List<string> nl = new List<string>();

            Random rnd = new Random();

            while (words.Count > 0 && c > 0)
            {
                int x = rnd.Next(words.Count - 1);

                string s = words[x];
                nl.Add(s);
                words.RemoveAt(x);
                c--;
            }
            return nl;
        }

        private void imageButton3_Click(object sender, EventArgs e)
        {
            CorrectAll(false);
        }

        private void imageButton2_Click(object sender, EventArgs e)
        {
            CorrectAll(true);
        }

        private void imageButton5_Click(object sender, EventArgs e)
        {
            SetTirage();
        }

        private void imageButton4_Click(object sender, EventArgs e)
        {
            Isolate();
        }


        private void Display7p1(string tirage)
        {
            listSolutions.Items.Clear();
            listSolutions.Columns.Clear();
            ColumnHeader col = new ColumnHeader();
            col.Width = 40;
            col.Text = "?";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = listSolutions.Width - 120;
            col.Text = "Mot";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            listSolutions.SuspendLayout();

            ArrayList wc = DicoUtils.Instance.SearchWordsSmaller(tirage.ToUpper());

            foreach (WordContainer w in wc)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = "";
                lvi.SubItems.Add(w.Before);
                lvi.SubItems.Add(w.Word);
                lvi.SubItems.Add(w.After);
                lvi.Tag = w;
                listSolutions.Items.Add(lvi);
            }

            wc.Clear();

            wc = DicoUtils.Instance.Do7Plus1(tirage.ToUpper());

            foreach (WordContainer w in wc)
            {
                ListViewItem lvi = new ListViewItem();
                if (w.Additional == " ")
                {
                    lvi.ForeColor = Color.Green;
                }
                lvi.Text = w.Additional;
                lvi.SubItems.Add(w.Before);
                lvi.SubItems.Add(w.Word);
                lvi.SubItems.Add(w.After);
                lvi.Tag = w;

                listSolutions.Items.Add(lvi);
            }
            listSolutions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listSolutions.ResumeLayout();
        }

        DicoAccessor da = new DicoAccessor();
        private void listSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSolutions.SelectedItems.Count > 0)
            {
                WordContainer wc = (WordContainer)listSolutions.SelectedItems[0].Tag;
                string src = "";
                if (wc.Word == null) src = wc.BaseWord;
                else src = wc.Word;
                                   
                solutionsPanel.Controls[0].BackColor = Color.DarkGreen;
                solutionsPanel.Controls[0].Text = src;
                    

                ShowSolutionDico(src);

                //List<TopMachine.Topping.DAL.Dico> dicos = da.GetWord(src.ToUpper(), true);
                //dicoPanel.Controls.Clear();
                //foreach (TopMachine.Topping.DAL.Dico d in dicos)
                //{
                //    DicoDefinition.GetDefinition(d);
                //    Def def = new Def();
                //    def.SetDef(d);
                //    def.Dock = DockStyle.Top;
                //    dicoPanel.Controls.Add(def);
                //    def.BringToFront();
                //}
            }
        }


        private void btnReview_Click(object sender, EventArgs e)
        {
            totalPlayed = totalFound = totalLost = 0;

            ResetStatistics();

            int[] total = new int[4];
            int nb = 0;

            if (gameConfig.TotalRounds == 0) gameConfig.TotalRounds = 500;

            //switch (gameConfig.TypeOfPlay)
            //{
            //    case 0:
            //        total[0] = gameConfig.TotalRounds;
            //        break;
            //    case 1:
            //        total[2] = gameConfig.TotalRounds;
            //        break;
            //    case 2:
            //        total[0] = gameConfig.TotalRounds * 65 / 100;
            //        total[1] = gameConfig.TotalRounds * 10 / 100;
            //        total[2] = gameConfig.TotalRounds * 20 / 100;
            //        total[3] = gameConfig.TotalRounds * 5 / 100;
            //        break;
            //    case 3:
            //        total[3] = gameConfig.TotalRounds;
            //        break;
            //    case 4:
            //        total[2] = gameConfig.TotalRounds * 70 / 100;
            //        total[3] = gameConfig.TotalRounds * 30 / 100;
            //        break;
            //}

            //for (int x = 0; x < 4; x++)
            //{
            //    nb += total[x];
            //}

            //nb = gameConfig.TotalRounds - nb;
            //if (nb > 0)
            //{
            //    switch (gameConfig.TypeOfPlay)
            //    {
            //        case 2:
            //            total[0] += nb;
            //            break;
            //        case 4:
            //            total[2] += nb;
            //            break;
            //    }
            //}

            WordsToPlay.Clear();
            foreach (var item in WordsMissed)
            {
                WordsToPlay.Add(item);
            }
            canSave = false;

            WordsToPlay = Randomize(WordsToPlay, WordsToPlay.Count);

            isPlaying = true;
            SetTirage();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnReview.Enabled = true;
         
        }

        private void btnSnapshot_Click(object sender, EventArgs e)
        {
            WordsSnapshot wsn = new WordsSnapshot();

            if (btnSnapshot.Tag == null)
            {

                wsn.totalWords = totalWords;
                wsn.totalFound = totalFound;
                wsn.totalLost = totalLost;
                wsn.WordsToPlay = WordsToPlay;
                wsn.WordsMissed = WordsMissed;

                wsn.save(pathOfList);
                btnSnapshot.Enabled = false;
                MessageBox.Show("sauvegarde réussite");

            }
            else
            {
                wsn.load(pathOfList);


                totalWords = wsn.totalWords;
                totalFound = wsn.totalFound;
                totalLost = wsn.totalLost; 
                WordsToPlay = wsn.WordsToPlay;
                WordsMissed = wsn.WordsMissed;

                foreach (var item in WordsMissed)
                {
                    listWords.Items.Add(item);
                }

                btnSnapshot.Tag = null;
                btnReview.Enabled = false;
                btnSnapshot.Enabled = true;
                btnSnapshot.Text = "sauver l'etat";
                ResetStatistics();

                int[] total = new int[4];
                LoadStat(total);
              
                isPlaying = true;
                SetTirage();


            }


            

        }
    }

}
