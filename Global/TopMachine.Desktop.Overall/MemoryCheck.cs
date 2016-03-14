using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopMachine.Desktop.Overall
{

    public class MemoryCheckItem
    {
        public string Message;
        public IDisposable Object; 
    }

    public class MemoryCheck
    {
        public static MemoryCheck Instance = new MemoryCheck();
        public static Object sync = new Object(); 

        public Dictionary<Guid, MemoryCheckItem> keys = new Dictionary<Guid, MemoryCheckItem>();

        public bool CheckKey(Guid key)
        {
            if (keys != null)
            {
                return Instance.keys.ContainsKey(key);
            }

            return false; 

        }

        public static Guid Register(object o,  string message)
        {

            Guid g = Guid.NewGuid(); 
          //  NLog.LogManager.GetLogger("dispose").Error(message  + " CREATE : " + g.ToString());
            Instance.keys.Add(g, new MemoryCheckItem() { Message = message, Object = o as IDisposable });
          //  NLog.LogManager.GetLogger("dispose").Error("Register : " + Instance.keys[g].Message + " :" + g.ToString() + " " + DateTime.Now.ToLongTimeString());
            return g; 
        }

        public static void Unregister(Guid g)
        {
            lock (sync)
            {
                if (Instance.CheckKey(g))
                {
                    // NLog.LogManager.GetLogger("dispose").Error(Instance.keys[g].Message + " Dispose : " + g.ToString());
                    //NLog.LogManager.GetLogger("dispose").Error("UnRegister : " + Instance.keys[g].Message + " :" + g.ToString() + " " + DateTime.Now.ToLongTimeString());
                    Instance.keys.Remove(g);
                    
                }

               // NLog.LogManager.GetLogger("dispose").Error("Error Register : " + g.ToString() + " " + DateTime.Now.ToLongTimeString());
            }
        }

        public static void CheckInstance()
        {
            lock (sync)
            {
                if (Instance.keys.Count == 0)
                {
                    //NLog.LogManager.GetLogger("dispose").Error("System is Clean");
                }
                else
                {
                    foreach (Guid g in Instance.keys.Keys.ToList())
                    {
                      //  NLog.LogManager.GetLogger("dispose").Error("Not Disposed : " + Instance.keys[g].Message + " :" + g.ToString());
                    }
                }
            }
        }

        public static void AddError(string message)
        {
           // NLog.LogManager.GetLogger("dispose").Error(message);
        }

        public static void DisposeObjects()
        {
            lock (sync)
            {
                foreach (Guid g in Instance.keys.Keys.ToList())
                {

                    try
                    {
                        Instance.keys[g].Object.Dispose();
                    }
                    catch (Exception ee)
                    {

                       // NLog.LogManager.GetLogger("wcf").ErrorException("MemoryCheck Final Dispose error", ee);
                    }
                        
                }
            }

        }

    }
}
