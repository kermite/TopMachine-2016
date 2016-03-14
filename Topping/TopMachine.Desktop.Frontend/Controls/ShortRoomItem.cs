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
using Topping.Core.Logic.Client;
using TopMachine.Desktop.Controls.Tools;
using TopMachine.Desktop.Overall;

namespace TopMachine.Topping.Frontend.Controls
{
    public partial class ShortRoomItem : UserControl,IMessageHandler
    {
        public delegate void RoomEvent(VRoom room);
        public delegate void PlayerEvent(ShortRoomItem room,string[] player);

        public VRoom room;
        public event RoomEvent OnJoinRoom;
        public event RoomEvent OnQuitRoom;
        public event PlayerEvent OnPlayers;
        public event RoomEvent OnStartGame;

        Guid MemoryCheckId; 

        public ShortRoomItem()
        {
            InitializeComponent();

            MemoryCheckId = MemoryCheck.Register(this, this.GetType().FullName);
            try
            {
                MessageProxy.Proxy.Session.AddMessageHandler("ShortRoomItem", this, true);
                timer1.Enabled = false;
            } catch(Exception ee)
            {
              // NLog.LogManager.GetLogger("wcf").Error("ShortRoomITem Constructor");
            }
        }


        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (MessageProxy.Proxy.Session != null)
            {
                MessageProxy.Proxy.Session.RemoveMessageHandler("ShortRoomItem");
            }

            if (timer1 != null)
            {
                timer1.Enabled = false;
                timer1.Dispose();
                timer1 = null;
            }
            MemoryCheck.Unregister(MemoryCheckId);

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);


        }


        private void EnableCtrl(VRoom r) 
        {
            switch (r.GameStatus)
            {
                case RoomStatus.Finished:
                    btnRestart.Enabled = true;
                    break;
                default:
                    btnRestart.Enabled = false;
                    break;
            }      
        }

        public void SetItem(VRoom r)
        {
            this.room = r;
            if (r != null)
            {
                if (room.Configuration != null)
                {
                    bindingConfig.DataSource = room.Configuration;
                    UpdateInfo();
                }

                EnableCtrl(r); 
            }
        }

        private void UpdateInfo()
        {

            if (room.Configuration != null)
            {
                bindingConfig.DataSource = room.Configuration;
            }

            VRoom r = MessageProxy.Proxy.Session.Rooms.Find(p => p.RoomId == room.RoomId);

            if (r.GameStartTime != null)
            { 
                TimeSpan ts = new TimeSpan(r.GameStartTime.Ticks);
                ts = ts.Subtract(new TimeSpan(DateTime.Now.Ticks));

                string t = ts.Minutes.ToString("000") + ":" + ts.Seconds.ToString("00");
                if (r.GameStatus < RoomStatus.Started)
                {
                    timer1.Enabled = true;
                    if (ts.TotalSeconds < 0)
                    {
                        lblStart.Text = "La partie va commencer";
                    }
                    else
                    {
                        lblStart.Text = string.Format(lblStart.Tag.ToString(), t);

                    }
                }
                else
                {
                    timer1.Enabled = false;
                    switch (r.GameStatus)
                    {
                        case RoomStatus.Empty:
                            lblStart.Text = "Ce salon est disponible";
                            break;
                        case RoomStatus.Created:
                            lblStart.Text = "La partie est créée";
                            break;
                        case RoomStatus.Finished:
                            lblStart.Text = "La partie est finie ";
                            break;
                        case RoomStatus.Started:
                            lblStart.Text = "La partie est en cours";
                            break;
                    }
                }


            }

            EnableCtrl(r);  
            lblStatus.Text = string.Format(lblStatus.Tag.ToString(), r.GameStatus.ToString());
            lblPlayers.Text = string.Format(lblPlayers.Tag.ToString(), r.TotalPlayers);
            lblStart.Invalidate();



            
        }

        delegate bool HandleMessageCallbackDelegate(MessageDto m);

        public bool HandleMessage(MessageDto message)
        {
       /*     return (bool)this.Invoke(new HandleMessageCallbackDelegate(HandleMessageSafe), new object[] { m });
        }

        public bool HandleMessageSafe(MessageDto message)
        {*/
            VRoom r;
            switch (message.MessageType)
            {
                case MessageType.RelaunchGame :
                    timer1.Enabled = true;
                    return false; 
                case MessageType.RoomStatusChange:
                    if (message.Room.RoomId == MessageProxy.Proxy.Session.CurrentRoom.RoomId)
                    {
                        this.SetItem(room);
                    }
                    return false; 
                default:
                    return false;
            }
            return true;
        }


        private void btnJoueurs_Click(object sender, EventArgs e)
        {
            if (room != null)
            {
                string[] listPlayers;
                listPlayers = MessageProxy.Proxy.ListRoomPlayers(room.RoomId);
                if (OnPlayers != null) 
                {
                    OnPlayers(this,listPlayers); 
                } 
            }
        }

        IGameState state = null;
        private void btnRestart_Click(object sender, EventArgs e)
        {
            Progress.Instance.Show(this);

            state = MessageProxy.Proxy.LoadGameState();

            if (state.Room == null)
            {
                Progress.Instance.Hide();
                MessageBox.Show("Une erreur s'est produite. Vraisemblablement que le nombre maximal de parties est atteint. Veuillez recommencer plus tard",
                  "Erreur", MessageBoxButtons.OK);
                return;
            }

            state.OnCreated += new CreatedDelegate(state_OnCreated);

            try
            {
                state.GenerateGame();
            }
            catch (Exception ee)
            {
                Progress.Instance.Hide();
                MessageBox.Show("Une erreur s'est produite. Vraisemblablement que le nombre maximal de parties est atteint. Veuillez recommencer plus tard",
                  "Erreur", MessageBoxButtons.OK);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        void state_OnCreated(VRoom r)
        {
            state.OnCreated -= new CreatedDelegate(state_OnCreated);
            Progress.Instance.Hide();
        }

    }
}
