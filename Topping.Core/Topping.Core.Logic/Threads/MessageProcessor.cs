using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopMachine.Topping.Dto;

namespace Topping.Core.Logic.Threads
{
    public class MessageProcessor : IBackgroundTask
    {
        public VGameState GameServerState { get; set; }
        private Queue<MessageDto> Messages = new Queue<MessageDto>(); 
        System.Threading.Timer timer = null;

        public static MessageProcessor _instance = null;

        public static MessageProcessor Instance
        {
            get
            {
                return _instance;
            }
        }

        public static void InitializeInstance(VGameState gs)
        {
            _instance = new MessageProcessor();
            _instance.GameServerState = gs;

            System.Threading.Thread thread = new System.Threading.Thread(_instance.StartService);
            thread.Start();
        }


        bool state = true; 
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
            } else 
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


        public void ProcessMessage(MessageDto Message)
        {
            try
            {
                switch (Message.MessageType)
                {
                    case MessageType.Private:
                        if (GameServerState.Players.ContainsKey(Message.To))
                        {
                            Player p = GameServerState.Players[Message.To];

                            if (Message.MessageModule == MessageModule.Chat)
                            {
                                if (p != null)
                                {
                                    GameServerState.Players[Message.To].AddMessage(Message);
                                    GameServerState.Players[Message.From].AddMessage(Message);
                                }
                            }
                            else
                            {
                                GameServerState.Players[Message.To].AddMessage(Message);
                            }
                        }
                        else
                        {
                            MessageDto mm = new MessageDto();
                            mm.To = Message.From;
                            mm.MessageType = MessageType.Error;
                            mm.Text = "L'utilisateur n'est pas connecté";
                            try
                            {
                                GameServerState.Players[mm.To].AddMessage(mm);
                            }
                            catch
                            { ; }
                          
                        }
                        break;
                    case MessageType.Public:
                        foreach (Player p in GameServerState.Players.Values.ToArray())
                        {
                           if (Message.MessageModule == MessageModule.Chat)
                            {
                                Message.ToRoom = -1; 
                            }

                            if (p != null)
                            {
                                if (p.CanChat && p.ChatActivated)
                                {
                                    p.AddMessage(Message);
                                }
                            }
                        }
                        break;
                    default:
                        foreach (Player p in GameServerState.Players.Values.ToArray())
                        {
                            if (p != null)
                            {
                                p.AddMessage(Message);
                            }
                        }
                        break;


                }
            }
            catch (Exception ee)
            {
              //  NLog.LogManager.GetLogger("wcf").ErrorException("ProcessMessage", ee);
                throw;
            }
        }
    }
}
