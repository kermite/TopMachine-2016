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
using Topping.Core.Proxy.Local.Client;
using TopMachine.Desktop.Controls.Tools;
using Topping.Core.Logic.Client;
using TopMachine.Desktop.Controls;
using Topping.Core.Proxy.Local;
using TopMachine.Desktop.Overall;
using Topping.Core.Logic.SQL;

namespace TopMachine.Topping.Frontend.Controls
{
    public partial class RoomControl : UserControl//, IMessageHandler 
    {
        public delegate void RoomEvent(RoomControl ctrl, VRoom room);

        VRoom room;
        IGameState state;

        public event RoomEvent OnJoinRoom;
        public event RoomEvent OnQuitRoom;
        public event RoomEvent OnCreateRoom;
        public event RoomEvent OnStartGame;

        Guid MemoryCheckId; 

        public RoomControl()
        {
            MemoryCheckId = MemoryCheck.Register(this, this.GetType().FullName);
            InitializeComponent();
           
        }

        private void EnableCtrl(VRoom r)
        {
            switch (r.GameStatus)
            {
                case RoomStatus.Empty :
                     btnCreateGame.Enabled = true;
                     btnCreateGame.Visible = true;
                     pnlSelectConfig.Visible = true;
                     lstPlayersRoom.Visible = false;
                     btnReload.Visible = false; 
                    break;
                case RoomStatus.Created:
                     btnCreateGame.Enabled = false;
                     btnCreateGame.Visible = false;
                     lstPlayersRoom.Visible = false;
                     btnReload.Visible = true; 
                    break;
                case RoomStatus.WaitingForGame:
                case RoomStatus.WaitingForStart:
                     btnCreateGame.Enabled = false;
                     btnCreateGame.Visible = false;
                     lstPlayersRoom.Visible = true;
                     btnReload.Visible = true; 
                    break;
                default:
                   break;
            }

        }

        public void SetItem(VRoom r)
        {
            if (r != null)
            {
                this.room = r;
                bindingConfig.DataSource = room.Configuration;
                timer1.Enabled = true;
                pnlSelectConfig.Visible = false;
                room.GameStartTime = DateTime.Now.AddSeconds(room.WaitingTime);
                EnableCtrl(r);
            }
            else
            {
                this.room = r;
                if (MessageProxy.Proxy.Session.Configurations != null)
                {
                    bindingConfig.DataSource = MessageProxy.Proxy.Session.Configurations;
                }
                else
                {
                    bindingConfig.DataSource = MessageProxy.Proxy.GetConfigurations();
                }

                bindingConfig.Position = 0; 
                timer1.Enabled = true;
                pnlSelectConfig.Visible = true;
            }
        }

        private void reloadRoom() 
        {
            if (MessageProxy.Proxy.Session.CurrentRoom != null)
            {
                VRoom r = MessageProxy.Proxy.Session.CurrentRoom;
               // RoomDto rdt = MessageProxy.Proxy.GetRoom(r.RoomId);     
                if (r.GameStartTime != null)
                {
                    
                    TimeSpan ts = new TimeSpan(r.GameStartTime.Ticks);
                    ts = ts.Subtract(new TimeSpan(DateTime.Now.Ticks));

                    string t = ts.Minutes.ToString("000") + ":" + ts.Seconds.ToString("00");
                    if (ts.TotalSeconds < 0)
                    {
                        lblStart.Text = "La partie va commencer";
                    }
                    else
                    {
                        lblStart.Text = string.Format(lblStart.Tag.ToString(), t);

                    }
                }
                lblPlayers.Text = string.Format(lblPlayers.Tag.ToString(), r.TotalPlayers);
                lblStatus.Text = string.Format(lblStatus.Tag.ToString(), r.GameStatus.ToString());
                EnableCtrl(r);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            reloadRoom(); 
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
           
            if (OnQuitRoom != null)
            {               
                OnQuitRoom(this,this.room );
            }
            this.Dispose();
        }

        private void btnCreateGame_Click(object sender, EventArgs e)
        {

            Progress.Instance.Show(this);
            pnlSelectConfig.Visible = false;
            VRoom r   = new  Room();
            state = MessageProxy.Proxy.CreateGameState((ConfigGameDto)cbConfigs.SelectedItem);

            if (state.Room == null)
            {
                Progress.Instance.Hide();
                MessageBox.Show("Une erreur s'est produite. Vraisemblablement que le nombre maximal de partie est atteint. Veuillez recommencer plus tard",
                  "Erreur", MessageBoxButtons.OK);
                return;
            }

            this.room = state.Room;
            state.OnCreated += new CreatedDelegate(state_OnCreated);
            try
            {
                state.GenerateGame();
               
            }
            catch (Exception ee)
            {
                Progress.Instance.Hide();
                MessageBox.Show("Une erreur s'est produite. Vraisemblablement que le nombre maximal de partie est atteint. Veuillez recommencer plus tard",
                  "Erreur", MessageBoxButtons.OK);
            }
        }

        void state_OnCreated(VRoom r)
        {
            Progress.Instance.Hide();
            SetItem(MessageProxy.Proxy.Session.CurrentRoom);
            
            state.OnCreated -= new CreatedDelegate(state_OnCreated);

            if (OnCreateRoom != null) 
            {
                OnCreateRoom(this, r);
                Progress.Instance.Hide(this); 
                this.Dispose(); 
            }

        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
          //  MessageProxy.Proxy.JoinRoom(room.RoomId);
        }


        //public bool HandleMessage(MessageDto message)
        //{
        //    Room r;
        //    switch (message.MessageType)
        //    {
        //        case MessageType.RoomGameStart:
        //            if (OnStartGame != null)
        //            {
        //                MessageProxy.Proxy.StartGame();
        //                MessageProxy.Proxy.DeactivateChat();   
        //                OnStartGame(this,MessageProxy.Proxy.Session.CurrentRoom);

        //            }
        //            break;
        //        default:
        //            return false;
        //    }
        //    return true;
        //}

        private void btnReload_Click(object sender, EventArgs e)
        {
            reloadRoom();
            btnPlayers_Click(null, null);
        }

        private void btnPlayers_Click(object sender, EventArgs e)
        {
           
            if (room != null)
            {
                // list of players room 
                string[] listPlayers;
                listPlayers = MessageProxy.Proxy.ListRoomPlayers(room.RoomId);
                lstPlayersRoom.DataSource = listPlayers;
                lstPlayersRoom.Visible = true;
            }
        }
        
    }
}
