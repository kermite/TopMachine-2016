using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopMachine.Topping.Dto;
using System.Threading;

namespace Topping.Core.Logic.Threads
{
    public class RoomMessageProcessor : IBackgroundTask
    {
        public VGameState GameServerState { get; set; }
        private Queue<MessageDto> Messages = new Queue<MessageDto>();
        public bool state = true;
        System.Threading.Timer timer = null;

        public static RoomMessageProcessor _instance = null;

        public static RoomMessageProcessor Instance
        {
            get
            {
                return _instance;
            }
        }

        public static void InitializeInstance(VGameState gs)
        {
            _instance = new RoomMessageProcessor();
            _instance.GameServerState = gs;

            System.Threading.Thread thread = new System.Threading.Thread(_instance.StartService);
            thread.Start();
        }

        public void StopService()
        {
            state = false;
        }


        public void StartService()
        {
            timer = new System.Threading.Timer(TimerProc);
            timer.Change(1000, 0);
        }


        private void TimerProc(object s)
        {
            if (state)
            {
                while (Messages.Count > 0)
                {
                    lock (syncRoot)
                    {
                        MessageDto msg = Messages.Dequeue();
                        ProcessMessage(msg);
                    }
                }
                timer.Change(1000, 0);
            }
            else
            {
                timer.Dispose();
            }
        }

        private object syncRoot = new Object();
        public void AddMessage(MessageDto item)
        {
            lock (syncRoot)
            {
                Messages.Enqueue(item);
            }
        }

        public void ProcessMessage(MessageDto msg)
        {
            try
            {
                switch (msg.MessageType)
                {

                    case MessageType.Private:
                        if (GameServerState.Players.ContainsKey(msg.To))
                        {
                            Player p = GameServerState.Players[msg.To];
                            if (p != null)
                            {
                                GameServerState.Players[msg.To].AddMessage(msg);
                            }
                        }
                        else
                        {
                            MessageDto mm = new MessageDto();
                            mm.To = msg.From;
                            mm.MessageType = MessageType.Error;
                            mm.Text = "L'utilisateur n'est pas connecté";
                            GameServerState.Players[msg.From].AddMessage(mm);
                        }
                        break;
                    case MessageType.Public:
                        VRoom room = GameServerState.Rooms[msg.ToRoom];
                        foreach (string s in room.GetPlayers())
                        {
                            if (GameServerState.Players.ContainsKey(s))
                            {
                                Player p = GameServerState.Players[s];
                                if (p != null)
                                {
                                    if (p.CanChat && p.ChatActivated)
                                    {
                                        p.AddMessage(msg);
                                    }
                                }
                            }
                            else
                            {
                                room.LeaveRoom(s);
                            }
                        }
                        break;
                    default:
                        VRoom room2 = GameServerState.Rooms[msg.ToRoom];
                        foreach (string s in room2.GetPlayers())
                        {
                            if (GameServerState.Players.ContainsKey(s))
                            {
                                Player p = GameServerState.Players[s];
                                if (p != null)
                                {
                                    p.AddMessage(msg);
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ee)
            {
              //  NLog.LogManager.GetLogger("wcf").ErrorException("ROOM : ProcessMessage", ee);
                throw;
            }
        }
    }
}
