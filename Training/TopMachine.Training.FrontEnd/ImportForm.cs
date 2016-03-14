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

namespace TopMachine.Training.FrontEnd
{
    public partial class ImportForm : UserControl, IFinished, IDisposable
    {
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

        public ImportForm()
        {
            InitializeComponent();
            InitDropDowns();
        }

        public void LoadList() 
        {
            Package p = (Package)PackageManager.Instance.Project.GetPackageItem("Training4dbo\\Lists");
            List<DropDownPairs> ListWord = new List<DropDownPairs>();
            ListWord.Add(new DropDownPairs("", null));
            foreach (Package pList in p.Items)
            {
                ListWord.Add(new DropDownPairs(pList.Key, pList));
          
            }
          
            
            cbList.DisplayMember = "Display";
            cbList.ValueMember = "Value";
            cbList.DataSource = ListWord; 
        
        }
        public void InitDropDowns()
        {

            List<DropDownPairs> ExtensionFile = new List<DropDownPairs>();
            ExtensionFile.Add(new DropDownPairs("Txt", "*.txt|*.csv"));
            //ExtensionFile.Add(new DropDownPairs("TopMachine 5.0", "*.xml"));
            //ExtensionFile.Add(new DropDownPairs("TopMachine < 5.0", "*.xml"));
            ExtensionFile.Add(new DropDownPairs("Export","*.TPE"));
            ExtensionFile.Add(new DropDownPairs("Topmachine", "*.db"));
        
            cbExtension.DisplayMember = "Display";
            cbExtension.ValueMember = "Value";
            cbExtension.DataSource = ExtensionFile;

            List<DropDownPairs> Category = new List<DropDownPairs>();
            Category.Add(new DropDownPairs("Tous les mots non joués",(int)Tools.TypeStatus.NonJoue));
            Category.Add(new DropDownPairs("Tous les mots trouvés", (int)Tools.TypeStatus.Trouve_Tous));
            Category.Add(new DropDownPairs("Tous les mots non trouvés", (int) Tools.TypeStatus.NonTrouve));    
            Category.Add(new DropDownPairs("Tous les mots isolés", (int)Tools.TypeStatus.Isole));
            

            chkLstCategorie.DataSource = Category;
            chkLstCategorie.DisplayMember = "Display";
            chkLstCategorie.ValueMember = "Value";

            LoadList(); 
                     
        }

        string packageKey; 
        Config config; 
        ListConfig listConfig;
        GameConfig gameConfig;
        
        

        #region IFinished Members

        public event OnFinishedDelegate OnFinished;

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = string.Empty ;

            if (cbList.Text == "")
            {
                name = txtGameName.Text;
            }
            else
            {
                name = cbList.Text;
            }
            List<Word> lstResult = null;
            
            TopMachine.Desktop.Controls.Tools.Progress.Instance.Show(this);

            switch (cbExtension.Text  )
            {
                case "Txt" :
                    lstResult = ReadTxt(openFileImport.FileName);
                    break;
                case "Export":
                    lstResult = ReadTxtExport(openFileImport.FileName);
                    break;
                //case "TopMachine 5.0":
                //    lstResult = ReadXml(openFileImport.FileName);
                //    break;
                //case "TopMachine < 5.0":
                //    lstResult = ReadXmlOld (openFileImport.FileName);
                //    break;

                case "Topmachine":
                    lstResult = ReadDb(openFileImport.FileName);
                    break;
                default:
                    break;
            }

           
            saveInDb(lstResult, name, cbList.Text == "");

            TopMachine.Desktop.Controls.Tools.Progress.Instance.Hide(this);

            if (OnFinished != null)
            {
                OnFinished();
            }
        }

     
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

        //private void imageButton2_Click(object sender, EventArgs e)
        //{
        //    this.Validate();
        //    ListCriterium crit = new ListCriterium();
        //    listConfig.Criteria.Add(crit);
        //    criteriaBindingSource.DataSource = listConfig.Criteria;
        //    dataGridView1.DataSource = null;
        //    dataGridView1.DataSource = criteriaBindingSource;

        //}

        //private void imageButton3_Click(object sender, EventArgs e)
        //{
        //    this.Validate();
        //    if (criteriaBindingSource.Current != null)
        //    {
        //        listConfig.Criteria.Remove(criteriaBindingSource.Current as ListCriterium);
        //        criteriaBindingSource.DataSource = listConfig.Criteria;
        //        dataGridView1.DataSource = null;
        //        dataGridView1.DataSource = criteriaBindingSource;

        //    }
        //}

        
   
        private void cbExtension_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbExtension.SelectedValue == null) return; 

            string[] lst = cbExtension.SelectedValue.ToString().Split('|');
            string result = "";
            foreach (var item in lst)
            {
                result += "(" + cbExtension.Text + ") |" + item + "|";   
            }
            result = result.Remove(result.Length - 1);
            openFileImport.Filter = result  ;    
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (openFileImport.ShowDialog() == DialogResult.OK) 
            {
                txtFileName.Text = openFileImport.SafeFileName;   
            }     
        }

        private void cbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtWord.Visible =  cbList.SelectedValue == null;
            lblName.Visible = cbList.SelectedValue == null; 
        }

        private Boolean saveInDb( List<Word> LstResult ,string nameDestination, bool newDestination) 
        {
          
            ListAccessor ladestination = null;

            if (ListAccessor.isExistList("Training4dbo\\Lists\\" + nameDestination))
            {
                ladestination = new ListAccessor("Training4dbo\\Lists\\" + nameDestination);
                if (MessageBox.Show("Attention vous êtes sur le point d'écraser vos données, désirez vous continuer ? ","Export", MessageBoxButtons.YesNo) == DialogResult.No) {
                    return false;
                }
                ladestination.DeleteWords();
                                
                //config = ladestination.GetConfig();
            }

            else
            {               
                listConfig = new ListConfig();
                gameConfig = new GameConfig();

                bindingConfig.DataSource = listConfig;
                bindingParams.DataSource = gameConfig;

                listConfig.Name = nameDestination;
                ladestination = new ListAccessor();
                config = ladestination.CreateConfig(listConfig, gameConfig);
               // LoadBaseIO lbi = new LoadBaseIO(listConfig.Name, config);
                //    lbi.DoData(); 

              //  lbi.CreateList();
              //  lbi.Dispose();
            }

            //ladestination.ReinitializeEntity();
           
            foreach (var ww in LstResult)
            {
                if (ladestination.GetWordByRack(ww.Rack) == null)
                {
                    ladestination.AddWord(ww);
                    
                }
            }


            //ladestination.Save();
            ladestination.Dispose();
            return true; 
              
        }
        private List<Word> ReadDb(string nameSource) 
        {

            long totalWords = 0;
            Dictionary<int, WordStatistic> StatisticsSource = new Dictionary<int, WordStatistic>();
            List<Word> LstResult = new List<Word>();
            ListAccessor laSource = new ListAccessor();
            laSource.initializeEntity(nameSource);

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

            foreach (DropDownPairs item in chkLstCategorie.CheckedItems)
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
                        LstResult.Add(ww);
                    

                    }
                }
            }
            laSource.Dispose();  
            return LstResult; 
        
        }
        //private void ReadDb(string nameSource, string nameDestination, bool newDestination)
        //{
        //    long totalWords = 0;
        //    Dictionary<int, WordStatistic> StatisticsSource = new Dictionary<int, WordStatistic>();
        //    List<Word> LstResult = new List<Word>();
        //    ListAccessor laSource = new ListAccessor();
        //    laSource.initializeEntity(nameSource);

        //    totalWords = 0;

        //    for (int x = 0; x < 4; x++)
        //    {
        //        StatisticsSource.Add(x, new WordStatistic());
        //    }

        //    foreach (WordStatistic gws in laSource.GetWordStatistics(nameSource))
        //    {
        //        totalWords += gws.Counter;
        //        StatisticsSource[gws.Status] = gws;
        //    }
        //    WordStatistic temp = null;

        //    ListAccessor ladestination = null;
        //    if (!newDestination)
        //    {
        //        ladestination = new ListAccessor("TrainingLists\\Lists\\" + nameDestination);
        //    }

        //    else
        //    {
        //        listConfig = new ListConfig();
        //        gameConfig = new GameConfig();

        //        bindingConfig.DataSource = listConfig;
        //        bindingParams.DataSource = gameConfig;

        //        listConfig.Name = nameDestination;
        //        ladestination = new ListAccessor();
        //        config = ladestination.CreateConfig(listConfig, gameConfig);
        //        LoadBaseIO lbi = new LoadBaseIO(listConfig.Name, config);
        //        lbi.DoData();

        //        lbi.CreateList();
        //        lbi.Dispose();
        //    }

        //    ladestination.ReinitializeEntity();
        //    Word currentWordDto;

        //    foreach (DropDownPairs item in chkLstCategorie.CheckedItems)
        //    {
        //        if (StatisticsSource[(int)item.Value].Words != null)
        //        {
        //            foreach (var word in StatisticsSource[(int)item.Value].Words)
        //            {
        //                Word ww = new Word();

        //                currentWordDto = laSource.GetWord(word);
        //                ww.WordsXML = currentWordDto.WordsXML;
        //                ww.Total = currentWordDto.Total;
        //                ww.Succeeded = currentWordDto.Succeeded;
        //                ww.Status = currentWordDto.Status;
        //                ww.Rack = word;
        //                ww.Lost = currentWordDto.Lost;
        //                LstResult.Add(ww);
        //                ladestination.AddWord(ww);

        //            }
        //        }
        //    }

        //    ladestination.Save();
        //    laSource.Dispose();
        //    ladestination.Dispose();




        //}
        private List<Word> ReadXml(string nameSource) 
        {
            DataTable dt;
            DataSet ds;
            ds = new DataSet() ;
            ds.ReadXmlSchema(nameSource); 
            ds.ReadXml(nameSource);
             List<Word> LstResult = new List<Word>();
             DataRow[] lstDr,lstDrType;
             WordList wl ;
             foreach (DropDownPairs Choice in chkLstCategorie.CheckedItems)
             {
                 lstDrType = ds.Tables[0].Select("intStatus = " + Choice.Value.ToString()); 
                 foreach (DataRow row in lstDrType)
                 {
                     Word ww = new Word();

                     /* solution list */
                     lstDr = ds.Tables[1].Select("TiragesSimples_id = " + row["TiragesSimples_id"].ToString());
                     wl = new WordList();
                     foreach (DataRow item in lstDr)
                     {
                         wl.Words.Add(item[0].ToString());

                     }
                     ww.WordsXML = new TopMachine.Desktop.Overall.ObjectSerializer<WordList>().Serialize(wl);
                     /* */

                     ww.Total = int.Parse(row["intTotal"].ToString());
                     ww.Succeeded = int.Parse(row["intReussi"].ToString());
                     ww.Status = int.Parse(row["intStatus"].ToString());
                     ww.Rack = row["Tirage"].ToString();
                     ww.Lost = int.Parse(row["intPerdu"].ToString());
                     LstResult.Add(ww);
                 }
             }
            return LstResult;
        }
        private List<Word> ReadXmlOld(string nameSource)
        {
            DataTable dt;
            DataSet ds;
            ds = new DataSet();
            ds.ReadXmlSchema(nameSource);
            ds.ReadXml(nameSource);
            List<Word> LstResult = new List<Word>();
            DataRow[] lstDr;
            WordList wl;
    
                foreach (DataRow row in ds.Tables["anyType"].Rows )
                {
                    Word ww = new Word();

                    /* solution list */
                    lstDr = ds.Tables["arltirages"].Select("anyType_id = " + row["anyType_id"].ToString());
                    wl = new WordList();
                    foreach (DataRow item in lstDr)
                    {
                        wl.Words.Add(item[0].ToString());

                    }
                    ww.WordsXML = new TopMachine.Desktop.Overall.ObjectSerializer<WordList>().Serialize(wl);
                    /* */

                    ww.Total = int.Parse(row["intTotal"].ToString());
                    ww.Succeeded = int.Parse(row["intTrouve"].ToString());
                    ww.Rack = row["strTirage"].ToString();
                    ww.Lost = int.Parse(row["intPerdu"].ToString());
                    int status;
                    if (ww.Total == 0) 
                    {
                        status = 0; 
                    }
                    else if (ww.Total > 0 && ww.Succeeded > ww.Lost)
                    {
                        status = 1;
                    }

                    else 
                    {
                        status = 2;
                    }
                    ww.Status = status;
                
                    LstResult.Add(ww);
                }
            
            return LstResult;
        }
        private string SortCharacter(string s)
        {
            string result;
            var s2 = (from c in s
                      orderby c ascending
                      select c
         ).ToList();
            result = "";
            foreach (var c in s2)
            {
                result += c;
            }

            return result; 
        }

        private List<Word> ReadTxtExport(string nameSource) 
        {
            List<Word> LstResult = new List<Word>();
            string lineFile;
            Word w;
            WordList wl;
            string result;

            List<int> check = new List<int>();
            foreach (DropDownPairs item in chkLstCategorie.CheckedItems )
            {
                check.Add(int.Parse(item.Value.ToString()));
            }
           

            using (var streamReader = new StreamReader(nameSource, Encoding.Default))
            {
                if (!streamReader.EndOfStream) 
                {
                    streamReader.ReadLine();
                }
                while (!streamReader.EndOfStream)
                {

                    lineFile = streamReader.ReadLine();

                    var lst = lineFile.Split(';'); 

                   w = new Word(); 
                    //Rack + ";" + Succeeded + ";"+ Lost + ";" + Status + ";" + Total + ";" + words; 

                    w.Rack = lst[0];
                    w.Succeeded = int.Parse(lst[1]);
                    w.Lost = int.Parse(lst[2]);
                    w.Status = int.Parse(lst[3]);
                    w.Total = int.Parse(lst[4]);
                    var lstword = lst[5].Split(' ');
                    
                    wl = new WordList();
                    wl.Words.AddRange(lstword.Where(x=>!string.IsNullOrEmpty(x)));
                    w.WordsXML = new TopMachine.Desktop.Overall.ObjectSerializer<WordList>().Serialize(wl);

                    if (check.Contains(w.Status))
                    {
                        LstResult.Add(w);
                    }

                }
            }

            return LstResult;
        
        }

        private List<Word> ReadTxt(string nameSource) 
        {
            List<Word> LstResult = new List<Word>();
            string lineFile;
            Word ww;
            WordList wl;
            string result;
           List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
           
            using (var streamReader = new StreamReader(nameSource, Encoding.Default))
            {
                while (!streamReader.EndOfStream)
                {
                                    
                    lineFile = streamReader.ReadLine();
                   lst.Add(new KeyValuePair<string, string>(SortCharacter(lineFile), lineFile));     
                   
                 
                }
            }
            lst.Sort(delegate(KeyValuePair<string, string> x, KeyValuePair<string, string> y) 
                         { return x.Key.CompareTo(y.Key);}); 
            string rack_prec = "" ;
             wl = new WordList();
            foreach (var item in lst )
	        {   
               if (rack_prec != item.Key.ToString() && rack_prec != "")
                {
                      ww = new Word();
                      ww.WordsXML = new TopMachine.Desktop.Overall.ObjectSerializer<WordList>().Serialize(wl); 
                      ww.Rack = rack_prec;    
                      LstResult.Add(ww); 
                      wl = new WordList(); 
                        
                }  
             
                wl.Words.Add(item.Value);  
                rack_prec = item.Key;  
	        } 
	        
            return LstResult ;
        } 
  
     
    }
}
