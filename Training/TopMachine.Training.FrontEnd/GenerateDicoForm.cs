using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TopMachine.Desktop.Controls;
using TopMachine.Training.DAL.fdbo ;
using CMWA.Packager.Custom;
using CMWA.Packager;
using System.IO;
using System.Xml;
using TopMachine.Topping.Dawg;
using CMWA.Packager.Tools.Bytes;

namespace TopMachine.Training.FrontEnd
{
    public partial class GenerateDicoForm : UserControl, IFinished, IDisposable
    {
        private List<DropDownPairs> _ListWord; 

        public class DropDownPairs
        {           
            private string display;
            private object val;

            public string Display
            {
                get { return display; }
                set { display = value; }
            }

            public object Value
            {
                get { return val; }
                set { val = value; }
            }

            public DropDownPairs(string d, object v)
            {
                display = d;
                val = v;
            }

        }

        ListAccessor la; 

        public int editMode = 0;

        public GenerateDicoForm()
        {
            InitializeComponent();
            LoadList();

            btnadd.Image = PackageManager.Instance.Project.GetBitmap("TopMachineData\\ApplicationImages\\Plus");
            btnadd.Text = string.Empty;
            btnadd.Width = btnadd.Image.Width;
        }

        public void LoadList() 
        {
            Package p = (Package)PackageManager.Instance.Project.GetPackageItem("Training4dbo\\Lists");
            _ListWord = new List<DropDownPairs>();
            foreach (Package pList in p.Items)
            {

                _ListWord.Add(new DropDownPairs(pList.Key, pList));
          
            }
            refreshCombo();
           
        
        }
       
        
        string packageKey; 
        Config config; 
        ListConfig listConfig;
        GameConfig gameConfig;
        
        

        #region IFinished Members

        public event OnFinishedDelegate OnFinished;

        #endregion

            
        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (la != null)
            {
                la.Dispose();
                la = null;
            }

            base.Dispose(); 
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (OnFinished != null)
            {
                OnFinished();
            }
        }

        private List<string> ReadDb(ElementListe el)
        {
            string nameSource = "Training4dbo\\Lists\\" + el.pckg.Key;

            long totalWords = 0;
            Dictionary<int, WordStatistic> StatisticsSource = new Dictionary<int, WordStatistic>();
            List<string> LstResult = new List<string>();
            ListAccessor laSource = new ListAccessor(nameSource);
           
            totalWords = 0;

            for (int x = 0; x < 4; x++)
            {
                StatisticsSource.Add(x, new WordStatistic());
            }

            foreach (WordStatistic gws in laSource.GetWordStatistics(nameSource))
            {
                totalWords += gws.Counter;
                StatisticsSource[gws.Status] = gws;
            }

            Word currentWordDto;

            foreach (TrainingConfig.DropDownPairs item in el.Status)
            {
                if (StatisticsSource[int.Parse(item.Value.ToString())].Words != null)
                {
                    foreach (var word in StatisticsSource[int.Parse(item.Value.ToString())].Words)
                    {
                        Word ww = new Word();
                        currentWordDto = laSource.GetWord(word);
                        //ww.WordsXML = currentWordDto.WordsXML;

                        string mot = "init";
                        using (XmlReader reader = XmlReader.Create(new StringReader(currentWordDto.WordsXML)))
                        {

                            while (mot != string.Empty)
                            {
                                reader.ReadToFollowing("string");
                                mot = reader.ReadInnerXml();

                                if (mot != string.Empty) 
                                {
                                    LstResult.Add(mot);
                                }

                            }
                        }

                    


                    }
                }
            }
            laSource.Dispose();
            return LstResult;

        }
      
        private void refreshCombo() 
        {
              cbList.DataSource = null;
              if (_ListWord.Count > 0)
              {
                  cbList.DisplayMember = "Display";
                  cbList.ValueMember = "Value";
                  cbList.DataSource = _ListWord;
                  cbList.SelectedValue = _ListWord[0].Value;
              }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (cbList.SelectedIndex > -1)
            {
                Package pckg = (Package)(cbList.SelectedValue);

                ElementListe el = new ElementListe(pckg);
                el.onNotificationDelete += el_onNotificationDelete;

                el.Top = pnlLists.Controls.Count * el.Height;
                pnlLists.Controls.Add(el);
                var itemDelete = _ListWord.Where(x=>x.Display == pckg.Key).First();
                _ListWord.Remove(itemDelete);
                refreshCombo();
                                               
            }
        }

        void el_onNotificationDelete(ElementListe sender)
        {
            DropDownPairs itemNew = new DropDownPairs(sender.pckg.Key, sender.pckg);
            _ListWord.Add(itemNew);

            refreshCombo();

            int i = pnlLists.Controls.IndexOf(sender);

            for (int j = i + 1; j < pnlLists.Controls.Count; j++)
            {
                pnlLists.Controls[j].Top = (j - 1) * sender.Height;
            }

            // redecaler les controles 

            pnlLists.Controls.Remove(sender);
        }

        private List<string> GetWordsOfList(ElementListe el) 
        {
            List<string> lstResult = new List<string>();

            lstResult = ReadDb(el);

            return lstResult;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {

            List<string> lstResult = new List<string>();

            foreach (var item in pnlLists.Controls)
            {
                lstResult.AddRange(GetWordsOfList(item as ElementListe));
                
            }
            if (MessageBox.Show(string.Format("vous êtes sur le point de générer {0} mots, confirmez votre choix  ?", lstResult.Count), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TopMachine.Desktop.Controls.Tools.Progress.Instance.Show(this);
                Dawg dawg = new Dawg();

                foreach (var word in lstResult)
                {
                    dawg.AddWord(word);

                    
                }

                dawg.Optimisation();

                cpt cptTools = new cpt();
                byte[] lstBOutput = dawg.getByteArray();
                cptTools.WriteFile(CMWA.Packager.FileUtility.GetFile("Data\\training.dawg", CMWA.Packager.LocationType.PersonalFiles), lstBOutput);
                TopMachine.Desktop.Controls.Tools.Progress.Instance.Hide(this);
                btnClose_Click(this, null);
            }
        }

       
  
     
    }
}
