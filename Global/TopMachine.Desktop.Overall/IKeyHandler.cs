using System;
using System.Collections.Generic;
using System.Text;

namespace TopMachine.Desktop.Overall
{
    public interface IKeyHandler
    {
        bool HandleKey(System.Windows.Forms.KeyEventArgs e);
    }

    public class KeyHandlers
    {
        #region Static 
        private static KeyHandlers _instance = null;

        public static KeyHandlers Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KeyHandlers();
                }

                return _instance; 
            }
        }

        #endregion

        public Dictionary<string, IKeyHandler> handlers = new Dictionary<string, IKeyHandler>();

        public void  AddHandler(string key, IKeyHandler handler)
        {
            if (handlers.ContainsKey(key))
            {
                handlers[key] = handler;
            }
            else
            {
                handlers.Add(key, handler); 
            }

        }

        public void RemoveHandler(string key, IKeyHandler handler)
        {
            if (handlers.ContainsKey(key))
            {
                handlers.Remove(key); 
            }
           
        }


        public bool HandleKey(System.Windows.Forms.KeyEventArgs e)
        {
            foreach (IKeyHandler kh in handlers.Values)
            {
                if (kh.HandleKey(e) == true) break;
            }

            return true;
        }


    }
}
