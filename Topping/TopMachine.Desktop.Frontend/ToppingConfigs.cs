using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TopMachine.Topping.Dto;
using Topping.Core.Logic;
using TopMachine.Topping.Engine.GameController;
using TopMachine.Topping.Common.Structures;
using Topping.Core.Proxy.Local.Client;
using TopMachine.Topping.Frontend.Controls;
using TopMachine.Desktop.Controls.Tools;
using Topping.Core.Proxy.Local;
using Topping.Core.Logic;
using Topping.Core.Logic.Client;
using System.Xml.Serialization;
using System.IO;
using TopMachine.Desktop.Overall;

namespace TopMachine.Topping.Frontend
{
    public partial class ToppingConfigs : UserControl
    {
        public delegate void StartDelegate(VRoom r); 
        public event  StartDelegate OnStartDelegate;
        List<ConfigGameDto> configs = null;
        Statistics stat;
        int IndexGenerate = 0;
       
        ConfigGameDto dtoCurrent;
        

        public ToppingConfigs()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadList();
			tabControl1.TabPages.Remove(TP_Simulation);
			stat = new Statistics(); 
            base.OnLoad(e);
        }


        private void LoadList()
        {
            foreach (Control ctl in pnlConfigs.Controls)
            {
                ctl.Dispose();
            }

            pnlConfigs.Controls.Clear();

            configs = MessageProxy.Proxy.GetConfigurations().OrderBy(p => p.Name).ToList();
            foreach (ConfigGameDto cfg in configs)
            {
                ConfigItem item = new ConfigItem();
                item.OnStartConfig += new ConfigItem.ConfigEvent(item_OnStartConfig);
                item.OnEditConfig += new ConfigItem.ConfigEvent(item_OnEditConfig);
                item.SetItem(cfg);
                item.Dock = DockStyle.Top;
                pnlConfigs.Controls.Add(item);
                item.BringToFront();
            }

            cmb_config.DisplayMember = "Name";
            cmb_config.ValueMember = "Id";
            cmb_config.DataSource = configs; 

            ddlConfigs.DisplayMember = "Name";
            ddlConfigs.ValueMember = "Id"; 
            ddlConfigs.DataSource = configs;

            ddlConfigs2.DisplayMember = "Name";
            ddlConfigs2.ValueMember = "Id";
            ddlConfigs2.DataSource = configs;
            
        }

        void item_OnEditConfig(ConfigGameDto dto)
        {
            pnlConfigs.Enabled = false;
            ToppingConfig tcfg = new ToppingConfig();
            tcfg.OnFinishDelegate += new ConfigGameDelegate(tcfg_OnFinishDelegate);
            tcfg.SetItem(dto);
            this.Controls.Add(tcfg);
            tcfg.BringToFront();
            tcfg.Top = (this.Height - tcfg.Height) / 2;
            tcfg.Left = (this.Width - tcfg.Width) / 2;
        }

        IGameState gameState = null;
        void item_OnStartConfig(ConfigGameDto dto)
        {
            this.Enabled = false;
            Progress.Instance.Show(this);
            gameState = MessageProxy.Proxy.CreateGameState(dto);
            MessageProxy.Proxy.StartGame();  
            gameState.OnCreated += new CreatedDelegate(gameState_OnCreated);
            gameState.OnFinishStatEvent += new FinishStatDelegate(gameState_OnFinishStatEvent); 
            gameState.GenerateGame();
        }

        void gameState_OnFinishStatEvent(object s)
        {
            stat = stat + (Statistics)s;

            IndexGenerate--;
            Generate();
         


        }

        void gameState_OnCreated(VRoom r)
        {
            gameState.OnCreated -= new CreatedDelegate(gameState_OnCreated);
            Progress.Instance.Hide(this);
            OnStartDelegate(r); 
        }

        private void btnNewConfig_Click(object sender, EventArgs e)
        {
            pnlConfigs.Enabled = false;
            ToppingConfig tcfg = new ToppingConfig();
            tcfg.OnFinishDelegate += new ConfigGameDelegate(tcfg_OnFinishDelegate);
            tcfg.SetItem(new ConfigGameDto());
            this.Controls.Add(tcfg);
            tcfg.BringToFront();
            tcfg.Top = (this.Height - tcfg.Height) / 2;
            tcfg.Left = (this.Width - tcfg.Width) / 2;
        }

        void tcfg_OnFinishDelegate(Control ctl, ConfigGameDto cfg, bool ok)
        {
            ctl.Dispose();
            pnlConfigs.Enabled = true;
            if (ok)
            {
                LoadList();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<RankingDto> list = MessageProxy.Proxy.GetRankings(MessageProxy.Proxy.Session.Pseudo, dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, Guid.Empty);

            foreach (RankingDto dto in list)
            {
                dto.ConfigName = configs.Where(p=>p.Id == dto.Config).First().Name; 
            }

            gridRankings.DataSource = list; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<RankingDto> list = MessageProxy.Proxy.GetRankings(MessageProxy.Proxy.Session.Pseudo, 0, 0, (Guid) ddlConfigs.SelectedValue);

            foreach (RankingDto dto in list)
            {
                dto.ConfigName = configs.Where(p => p.Id == dto.Config).First().Name;
            }

            gridRankings.DataSource = list; 
        }

        private void btn_ListeParties_Click(object sender, EventArgs e)
        {
            List<GamesDetailDto> list = MessageProxy.Proxy.GetGamesDetail(MessageProxy.Proxy.Session.Pseudo, dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, (Guid)ddlConfigs.SelectedValue);

            foreach (GamesDetailDto dto in list)
            {
                dto.ConfigName = configs.Where(p => p.Id == dto.ConfigId).First().Name;
            }

            gridDetail.DataSource = list;
        }
        private void Generate() 
        {
            if (IndexGenerate > 0 && gameState != null)
            {
                // display stat 
                
                gameState.Dispose();
                System.GC.Collect();
                // MemoryCheck.CheckInstance();

            }

            if (IndexGenerate == 0)
            {
                if (gameState != null)
                {
                   

                    Progress.Instance.Hide(this);
                    txt_result.Text = "Statistique sur " + txtNb_Games.Value + " parties \r\n";
                    txt_result.Text += stat.ToString();
                    btnPlay.Enabled = true;
                }
            }
            else
            {   if (gameState != null)
                {
                   gameState.Dispose();
                
                }

                gameState = MessageProxy.Proxy.CreateGameState(dtoCurrent);
                MessageProxy.Proxy.StartGame();
                gameState.OnCreated += new CreatedDelegate(gameState_OnCreated);
                gameState.OnFinishStatEvent += new FinishStatDelegate(gameState_OnFinishStatEvent); 
                gameState.GenerateGame(true);
            }
            
        }     
        private void btn_generateGame_Click(object sender, EventArgs e)
        {
            IndexGenerate = (int)txtNb_Games.Value;
            if (cmb_config.SelectedValue != null)
            {
                stat = new Statistics();
            
                Guid id = (Guid)cmb_config.SelectedValue;
                dtoCurrent  = configs.Where(x => x.Id == id).ToList()[0];
                Progress.Instance.Show(this);

                Generate();
            
            }  
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            gameState.PlayGame();
            
        }


    }
}
