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
    public partial class ExportForm : UserControl, IFinished, IDisposable
    {
        private List<DropDownPairs> _ListWord; 

        ListAccessor la; 

        public int editMode = 0;

        public ExportForm()
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
                if (StatisticsSource[(int)item.Value].Words != null)
                {
                    foreach (var word in StatisticsSource[(int)item.Value].Words)
                    {
                        Word ww = new Word();

                        currentWordDto = laSource.GetWordByRack(word);
                        ww.WordsXML = currentWordDto.WordsXML;
                        ww.Total = currentWordDto.Total;
                        ww.Succeeded = currentWordDto.Succeeded;
                        ww.Status = currentWordDto.Status;
                        ww.Rack = word;
                        ww.Lost = currentWordDto.Lost;
                        LstResult.Add(ww.ToString());


                    }
                }
            }
            laSource.Dispose();
            return LstResult;

        }
              
        private void refreshCombo() 
        {
            cbList.SelectedIndex = -1;
            //cbList.DataSource = new List<DropDownPairs>();
              if (_ListWord.Count > 0)
              {
                 // cbList.DisplayMember = "Display";
                 // cbList.ValueMember = "Value";
                  cbList.DataSource = _ListWord;
                  cbList.SelectedIndex = 0;
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
            List<string> lstResult;

            lstResult = ReadDb(el);

            return lstResult;
        }

        private void exportData(List<string> lstResult, string pathDestination) 
        {
            if (File.Exists(pathDestination))
            {
                File.Delete(pathDestination);
            }
                        
            File.AppendAllLines(pathDestination, lstResult);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

            List<string> lstResult = new List<string>();

            lstResult.Add(Word.GetHeader());
            foreach (var item in pnlLists.Controls)
            {
                lstResult.AddRange(GetWordsOfList(item as ElementListe));
                
            }
            if (lstResult.Count > 0)
            {
                if (MessageBox.Show(string.Format("vous êtes sur le point d'exporter {0} mots, confirmez votre choix  ?", lstResult.Count - 1), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    TopMachine.Desktop.Controls.Tools.Progress.Instance.Show(this);
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string pathFile; 

                        FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                        pathFile = System.IO.Path.GetFileNameWithoutExtension(fi.Name) + ".TPE";
                        exportData(lstResult, fi.DirectoryName +"\\"+ pathFile);
                        btnClose_Click(this, null);
                    }

                    TopMachine.Desktop.Controls.Tools.Progress.Instance.Hide(this);
                }
            }
            else 
            {
                MessageBox.Show("Votre liste est vide !!");
            }
        }

       
       

       
  
     
    }
}
