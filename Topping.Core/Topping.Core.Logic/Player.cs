using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopMachine.Topping.Dto;

namespace Topping.Core.Logic
{
    public class Player : IDisposable
    {
        public string Pseudo { get; set; }
        public DateTime LastAccess { get; set; }

        public bool CanChat { get; set; }
        public bool ChatActivated { get; set; }
        public int TotalMessages { get; set; }
        public int SizeMessages { get; set; }
        public int CurrentRoom { get; set; }
        public Guid PlayerGuid { get; set; }
        public Player() 
        {
            CanChat = true;
        }


        private Queue<MessageDto> messages = new Queue<MessageDto>(); 

        public Player(string pseudo)
        {
            Pseudo = pseudo;
            LastAccess = DateTime.Now;
            CurrentRoom = -1;
            CanChat = true; 
        }

        private static object syncRoot = new Object();
        public void AddMessage(MessageDto m)
        {
            m.To = Pseudo;
            lock (syncRoot)
            {
                messages.Enqueue(m); 
            }
        }

        public List<MessageDto> GetMessages()
        {
            lock (syncRoot)
            {
                List<MessageDto> m = messages.ToList();
                messages.Clear();
                return m;
            }
        }


        #region IDisposable Members

        public void Dispose()
        {
            messages.Clear();
            messages = null;
        }

        #endregion
    }
}
