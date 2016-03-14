using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopMachine.Topping.Dto;


namespace Topping.Core.Logic.Threads
{
    public class SendGames : IDisposable, IBackgroundTask
    {

        private Queue<string> Files = new Queue<string>();

        System.Threading.Timer timer = null;
        System.Threading.Timer timerRooms = null;
        bool stop = false;

        public static SendGames _instance = null;

        public static SendGames Instance
        {
            get
            {
                return _instance;
            }
        }

        public static void InitializeInstance(VGameState gs)
        {
            _instance = new SendGames();

            try
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SerializedGamesPath"]))
                {

                    string[] files = System.IO.Directory.GetFiles(System.Configuration.ConfigurationManager.AppSettings["SerializedGamesPath"]);

                    foreach (string f in files)
                    {
                        _instance.AddFile(f);
                    }
                }
                System.Threading.Thread thread = new System.Threading.Thread(_instance.StartService);
                thread.Start();
            } catch
            {
            }
        }


        private object syncRoot = new Object();
        public void AddFile(string file)
        {
            lock (syncRoot)
            {
                Files.Enqueue(file);
            }
        }

        public void StopService()
        {
            stop = false;
        }

        public void StartService()
        {
            //timerRooms = new System.Threading.Timer(TimerRoomsProc);
            //timerRooms.Change(10000, 0);
        }



        //private void TimerRoomsProc(object state)
        //{
        //    if (!stop)
        //    {
        //        if (Files.Count > 0)
        //        {
        //            string fn = Files.Dequeue();
        //            try
        //            {
        //                TopMachineAdminServiceClient client = new TopMachineAdminServiceClient();
        //                client.UploadFile(System.IO.Path.GetFileName(fn));
        //                client.Close();
        //            }
        //            catch (Exception ee)
        //            {
        //                AddFile(fn);
        //                NLog.LogManager.GetLogger("wcf").ErrorException("SendGames", ee);
        //            }
        //            finally
        //            {
        //                ;
        //            }
        //        }

        //        if (!stop) timerRooms.Change(5000, 0);
        //    }
        //    else
        //    {
        //        timerRooms.Dispose();
        //    }
        //}




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
