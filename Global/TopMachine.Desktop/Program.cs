using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TopMachine.Desktop;
using Topping.Core.Proxy.Local;
using SingleInstanceApplication;
using System.Reflection;
using TopMachine.Desktop.Overall;
using Topping.Core.Proxy.Local.Client;

namespace TopMachine.Desktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /*  [STAThread]
          static void Main()
          {
              string[] args = Environment.GetCommandLineArgs();

              CheckLogDirectory();
              AppDomain.CurrentDomain.FirstChanceException += new EventHandler<System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs>(CurrentDomain_FirstChanceException);
              Application.EnableVisualStyles();
              Application.SetCompatibleTextRenderingDefault(false);

          }*/

        private static TopMachineMain _mainFrm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            if (!ApplicationInstanceManager.CreateSingleInstance(
                    Assembly.GetExecutingAssembly().GetName().Name,
                    SingleInstanceCallback)) return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            if (args.Length > 1)
            {
                _mainFrm = new TopMachineMain(args[1]);
                Application.Run(_mainFrm);
            }
            else
            {
                _mainFrm = new TopMachineMain();
                Application.Run(_mainFrm);
            }
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                MessageProxy.Proxy.Dispose();
                MemoryCheck.CheckInstance();
                MemoryCheck.DisposeObjects();
            }
            catch (Exception ee)
            {

               // NLog.LogManager.GetLogger("wcf").ErrorException("MemoryCheck Finalization", ee);
            }
        }

        /// <summary>
        /// Single instance callback handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SingleInstanceApplication.InstanceCallbackEventArgs"/> instance containing the event data.</param>
        private static void SingleInstanceCallback(object sender, InstanceCallbackEventArgs args)
        {
            /*if (args == null || _mainFrm == null) return;
            Action<bool> d = (bool x) =>
            {
                _mainFrm.ApendArgs(args.CommandLineArgs);
                _mainFrm.Activate(x);
            };
            _mainFrm.Invoke(d, true);*/

        }

        private static void CheckLogDirectory()
        {
            string fn = CMWA.Packager.FileUtility.GetFile("\\logs", CMWA.Packager.LocationType.PersonalFiles);
            if (!System.IO.Directory.Exists(fn))
            {
                System.IO.Directory.CreateDirectory(fn);
            }
        }

        static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            if (!e.Exception.Message.Contains("Serializer"))
            {
               // NLog.LogManager.GetLogger("wcf").ErrorException("FirstChanceException", e.Exception);
            }
        }

    }
}
