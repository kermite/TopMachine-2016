using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.ServiceModel;

namespace TopMachine.Services.WCF.Console
{
    public class TopMachineWebServices : ServiceBase
    {
        public ServiceHost serviceHost = null;

        public TopMachineWebServices()
        {
            ServiceName = "CMWA.TopMachine.Services";
        }

        public static void Main()
        {
            ServiceBase.Run(new TopMachineWebServices());
        }

        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            serviceHost = new ServiceHost(typeof(Topping));
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
