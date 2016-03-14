using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopMachine.Topping.Dto;

namespace Topping.Core.Logic.Threads
{
    public class GameTimer : IDisposable, IBackgroundTask
    {
        public VGameState GameServerState { get; set; }

        System.Threading.Timer timer = null;
        System.Threading.Timer timerRooms = null;
        bool stop = false;

        public static GameTimer _instance = null;

        public static GameTimer Instance
        {
            get
            {
                return _instance;
            }
        }

        public static void InitializeInstance(VGameState gs)
        {
            _instance = new GameTimer();
            _instance.GameServerState = gs;

            System.Threading.Thread thread = new System.Threading.Thread(_instance.StartService);
            thread.Start();
        }


        public void StopService()
        {
            stop = false;
        }

        public void StartService()
        {
            // If Local WE DO NOT !
            if (System.Configuration.ConfigurationManager.AppSettings["IsLocal"] == null
                || System.Configuration.ConfigurationManager.AppSettings["IsLocal"] == "0")
            {
                timer = new System.Threading.Timer(TimerProc);
                timer.Change(60000, 0);
            }

            timerRooms = new System.Threading.Timer(TimerRoomsProc);
            timerRooms.Change(5000, 0);


        }



        private void TimerRoomsProc(object state)
        {
            if (!stop)
            {
                try
                {
                    if (GameServerState != null)
                    {
                        DateTime dtm = DateTime.Now.AddMinutes(-5);
                        foreach (VRoom r in GameServerState.Rooms.ToArray())
                        {
                            if (r.GameStatus == RoomStatus.WaitingForStart && r.GameStartTime < DateTime.Now)
                            {
                                r.GameStatus = RoomStatus.Started;
                                r.finalRoomDto.ReinitGame();  
                                //    r.finalRoomDto.PlayedRounds.Clear();
                                //    r.finalRoomDto.Playersummaries.Clear();
                                 

                                MessageDto m = new MessageDto();
                                m.From = "System";
                                m.MessageType = MessageType.RoomStatusChange;
                                m.ToRoom = r.RoomId;
                                m.Room = r.GetDto();
                                SendMessage(m);

                                MessageDto mm = new MessageDto();
                                mm.From = "System";
                                mm.MessageType = MessageType.RoomGameStart;
                                mm.Text = "La partie est générée";
                                mm.ToRoom = r.RoomId;
                                mm.Room = r.GetDto();
                                SendRoomMessage(r, mm);
                            }
                        }
                    }
                }
                finally
                {
                    ;
                }

                if (!stop) timerRooms.Change(5000, 0);
            }
            else
            {
                timerRooms.Dispose();
            }
        }


        private void SendRoomMessage(VRoom r, MessageDto m)
        {
            RoomMessageProcessor.Instance.AddMessage(m);
        }

        private void SendMessage(MessageDto m)
        {
            MessageProcessor.Instance.AddMessage(m);
        }

        private void TimerProc(object state)
        {
            if (!stop)
            {
                try
                {
                    if (GameServerState != null)
                    {
                        DateTime dtm = DateTime.Now.AddMinutes(-5);
                        foreach (VRoom r in GameServerState.Rooms.ToArray())
                        {
                            if (r.GameStatus != RoomStatus.Empty && r.LastAccess < dtm)
                            {
                                if (r.GameStatus >= RoomStatus.Started)
                                {
                                    r.FinishGame();
                                }

                                r.Clean();
                            }
                        }

                        dtm = DateTime.Now.AddMinutes(-1);
                        foreach (Player p in GameServerState.Players.Values.ToArray())
                        {
                            if (p.LastAccess < dtm)
                            {
                                try
                                {
                                    MessageDto m;
                                    if (p.CurrentRoom > 0)
                                    {
                                        VRoom r = GameServerState.Rooms[p.CurrentRoom];
                                        r.FinishGame(p.Pseudo);
                                        r.LeaveRoom(p.Pseudo);
                                        p.CurrentRoom = -1;

                                        m = new MessageDto();
                                        m.From = p.Pseudo;
                                        m.MessageType = MessageType.RoomLeave;
                                        m.ToRoom = r.RoomId;
                                        m.Room = r.GetDto();
                                        SendMessage(m);
                                    }

                                    GameServerState.RemovePlayer(p);
                                    m = new MessageDto();
                                    m.From = p.Pseudo ;
                                    m.MessageType = MessageType.Logoff;
                                    SendMessage(m);

                                   p.Dispose();
                                }
                                catch { ;}
                            }
                        }
                    }
                }
                finally
                {
                    ;
                }

                if (!stop) timer.Change(60000, 0);
                else
                {
                    timer.Dispose();
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            stop = true;
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }

            if (timerRooms != null)
            {
                timerRooms.Dispose();
                timerRooms = null;
            }
        }

        #endregion
    }
}
