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
using TopMachine.Desktop.Overall;

namespace TopMachine.Topping.Frontend.Controls
{
    public partial class RoomItem : UserControl,IMessageHandler
    {
        public delegate void RoomEvent(VRoom room);
        public delegate void FinalRoomEvent(FinalRoomDto finalroom);
        public delegate void PlayerEvent(RoomItem room,string[] player);

        public VRoom room;
        public event RoomEvent OnJoinRoom;
        public event RoomEvent OnQuitRoom;
        public event PlayerEvent OnPlayers;
        public event RoomEvent OnStartGame;

        private string key = "";

        Guid MemoryCheckId; 
        public RoomItem()
        {
            InitializeComponent();
            MemoryCheckId = MemoryCheck.Register(this, this.GetType().FullName);

            key = "RoomItem" + Guid.NewGuid().ToString(); 

            try
            {
                MessageProxy.Proxy.Session.AddMessageHandler(key, this, true);
            } catch
            {
        ;
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
                MessageProxy.Proxy.Session.RemoveMessageHandler(key);
            }  

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            MemoryCheck.Unregister(MemoryCheckId);
            base.Dispose(disposing);
        }


        public void ChangeState(bool isjoin,bool isstart ) 
        {
            btnJoin.Click -= new EventHandler(btnJoin_Click);
            btnJoin.Click -= new EventHandler(btnQuitte_Click);
           

            if (isjoin && !isstart)
            {
                btnJoin.Text = "Rejoignez";
                btnJoin.Click += new EventHandler(btnJoin_Click);
                btnJoin.Click -=  new EventHandler (btnQuitte_Click);
               
            }
            else if (isjoin && isstart)
            {
                btnJoin.Text = "Observez";
                btnJoin.Click += new EventHandler(btnJoin_Click);
                btnJoin.Click -= new EventHandler(btnQuitte_Click);
            
                
            }
            else 
            {
                btnJoin.Text = "Quittez";
                btnJoin.Click -= new EventHandler(btnJoin_Click);
                btnJoin.Click += new EventHandler(btnQuitte_Click);

               
            }
        }

        private void EnableCtrl(VRoom r) 
        {
        //    btnJoin.Text = "Rejoignez"; 
            switch (r.GameStatus)
            {
                case RoomStatus.Created:
                    btnJoin.Enabled = false;
                    btnJoueurs.Visible = true; 
                    break;
                case RoomStatus.WaitingForGame:
                case RoomStatus.WaitingForStart:
                    btnJoin.Enabled = true;
                    btnJoueurs.Visible = true; 
                    break;
                case RoomStatus.Started:
                   // btnJoin.Text = "Observez"; 
                    btnJoin.Enabled = true;
                    btnJoueurs.Visible = true; 
                    break;
                default:
                    btnJoin.Enabled = false;
                    btnJoueurs.Visible = true;
                    break;
            }
        
        }

        public void  SetItemForObservation(VRoom r)
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
                if (r.GameStatus < RoomStatus.Started && r.GameStatus > RoomStatus.Created)
                {
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
            
        }

        private void btnQuitte_Click(object sender, EventArgs e)
        {
            if (OnQuitRoom != null)
            {
                if (room != null)
                {
                    MessageProxy.Proxy.LeaveRoom();
                }
                OnQuitRoom(this.room);

            }
        }
        private void btnJoin_Click(object sender, EventArgs e)
        {
            if (MessageProxy.Proxy.Session.CurrentRoom == null)
            {
                if (OnJoinRoom != null)
                {
                    OnJoinRoom(room);
                }
            }
        }


        delegate bool HandleMessageCallbackDelegate(MessageDto m);

        public bool HandleMessage(MessageDto message)
        {
      /*      return (bool)this.Invoke(new HandleMessageCallbackDelegate(HandleMessageSafe), new object[] { m });
        }

        public bool HandleMessageSafe(MessageDto message)
        {*/
            VRoom r;
            switch (message.MessageType)
            {
                case MessageType.RoomGameStart:
                    if (OnStartGame != null && MessageProxy.Proxy.Session.CurrentRoom != null)
                    {
                        if (MessageProxy.Proxy.Session.CurrentRoom.RoomId  == message.Room.RoomId )
                        {
                            OnStartGame(MessageProxy.Proxy.Session.CurrentRoom);
                        }

                    }
                    break;

                case MessageType.FinalRoom:

                    break;

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


       
    }
}
