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
using TopMachine.Topping.Dawg;

namespace TopMachine.Training.FrontEnd
{
    public partial class UpdateList : UserControl, IFinished, IDisposable
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

        private const string _PathList = "Training4dbo\\Lists";
        private bool _isSearchWord;
        private List<Word> _lstResult;
        private List<DropDownPairs> _Category;
        ListAccessor la; 

        public int editMode = 0;

        public UpdateList()
        {
            InitializeComponent();
            InitDropDowns();
        }

        public void LoadList() 
        {
            Package p = (Package)PackageManager.Instance.Project.GetPackageItem(_PathList);
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

           
            _Category = new List<DropDownPairs>();
            _Category.Add(new DropDownPairs("mot non joué", 0));
            _Category.Add(new DropDownPairs("mot trouvé", 1));
            _Category.Add(new DropDownPairs("mot non trouvé ", 2));    
            _Category.Add(new DropDownPairs("mot isolé", 3));

            cbCategory.DataSource = _Category;
            cbCategory.DisplayMember = "Display";
            cbCategory.ValueMember = "Value";

            cbCategory.Visible = false;
            LoadList(); 
                     
        }

        string packageKey; 
        Config config; 
        ListConfig listConfig;
        GameConfig gameConfig;
        ListAccessor _laSource;
        

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
            _laSource.Dispose();
            if (OnFinished != null)
            {  
                OnFinished();
            }
        }

        private void BindingGridView(List<Word> result) 
        {
            GVWords.Rows.Clear();
            List<string> lst;
            List<string> LstResult;
            if (result.Count == 0)
            {
                MessageBox.Show("Ce mot n'existe pas");
                BtnAdd.Enabled = true;
                BtnDel.Enabled = false;
            }
            else
            {
                cbCategory.Visible = true;
                BtnAdd.Enabled = false;
                BtnDel.Enabled = true;
                btnUpdate.Enabled = true;

                foreach (var item in result)
                {
                    lst = item.getWordsSolution();
                    cbCategory.SelectedValue = item.Status;
                    foreach (var word in lst)
                    {
                        LstResult = new List<string>();
                        LstResult.Add(word);
                        LstResult.Add(item.Succeeded.ToString());
                        LstResult.Add(item.Lost.ToString());
                        LstResult.Add(item.Total.ToString());
                        LstResult.Add(item.Rack);
                        string category = _Category.Find(x => (int)(x.Value) == item.Status).Display;
                        LstResult.Add(category);
                        GVWords.Rows.Add(LstResult.ToArray());

                    }
                }
            }


        }

        private List<Word> LoadList(string nameSource)
        {
            if (_laSource != null)
            {
                _laSource.Dispose();
            }


            _laSource = new ListAccessor(nameSource);
            return _laSource.LoadWords();
        }
       
        private List<Word> SearchWord(string nameSource, string word) 
        {
            if (_laSource != null) 
            {
                _laSource.Dispose();
            }


            _laSource = new ListAccessor(nameSource);
            _lstResult = _laSource.GetWords(word);

            return _lstResult;
        }

        private List<Word> SearchRack(string nameSource, string rack)
        {
            if (_laSource != null)
            {
                _laSource.Dispose();
            }


            _laSource = new ListAccessor(nameSource);
            _lstResult = _laSource.GetWordsByRack(SortCharacter(rack));
            return _lstResult;
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
       
        private void BtnDelWord_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vous êtes sur le point de supprimer ce mot , confirmer votre choix ?","Suppression",MessageBoxButtons.YesNo) == DialogResult.Yes) 
            {
                _laSource.DeleteWord(txtWordRack.Text);
                initialise();
               
            }
        }
        private void initialise() 
        {
            GVWords.Rows.Clear();
            BtnDel.Enabled = false;
            BtnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            cbCategory.Visible = false;
        
        }
        private void BtnAddWord_Click(object sender, EventArgs e)
        {
            Word w = new Word();


            
            w.Lost = 0;
            w.Succeeded = 0;
            w.Total = 0;
            w.Status = 0;
            string wordRack = txtWordRack.Text.ToUpper();
            w.Rack = SortCharacter(wordRack);
            WordList wl = new WordList();
            if (_isSearchWord)
            {                
                wl.Words.Add(wordRack);
                w.WordsXML = new TopMachine.Desktop.Overall.ObjectSerializer<WordList>().Serialize(wl);
            }

            else 
            {
               var lst = DicoUtils.Instance.DoWordsRack(wordRack);
                               
               WordContainer wCurrent;
               foreach (var item in lst)
               {
                   wCurrent = item as WordContainer;

                   wl.Words.Add(wCurrent.Word);
               }

               w.WordsXML = new TopMachine.Desktop.Overall.ObjectSerializer<WordList>().Serialize(wl); 
            
            }
            _laSource.AddWord(w);
            initialise();
        }
        private void btnSearchRack_Click(object sender, EventArgs e)
        {
            if (cbList.SelectedValue != null && !string.IsNullOrEmpty(txtWordRack.Text))
            {
                _isSearchWord = false;
                string nameOfList = _PathList + "\\" + (cbList.SelectedValue as Package).Key;
                BindingGridView(SearchRack(nameOfList, txtWordRack.Text.ToUpper()));
            }

        }
        private void BtnSearchWord_Click(object sender, EventArgs e)
        {
            if (cbList.SelectedValue != null && !string.IsNullOrEmpty(txtWordRack.Text))
            {
                _isSearchWord = true;
                string nameOfList = _PathList + "\\" + (cbList.SelectedValue as Package).Key;
                BindingGridView(SearchWord(nameOfList, txtWordRack.Text.ToUpper()));
            }
        }
        private void btnDisplayList_Click(object sender, EventArgs e)
        {
            if (cbList.SelectedValue != null)
            {
                string nameOfList = _PathList + "\\" + (cbList.SelectedValue as Package).Key;
                BindingGridView(LoadList(nameOfList));
                BtnDel.Enabled = false;
                BtnAdd.Enabled = false;
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cbCategory.SelectedValue != null)
            {
                foreach (var item in _lstResult)
                {
                    if ((int)cbCategory.SelectedValue != item.Status)
                        item.Status = (int)cbCategory.SelectedValue;
                    _laSource.Save(item);
                }

                initialise();
            }
            else 
            {
                MessageBox.Show("Choisir une categorie");
            }

            
        } 
  
     
    }
}
