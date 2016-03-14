using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using System.Security.Principal;

namespace TopMachine.Services.WCF.Console
{
    static class Console
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
             using (ServiceHost serviceHost = new ServiceHost(typeof(Topping)))
            {
                serviceHost.Open();
                // The service can now be accessed.
                System.Console.WriteLine("The service is ready.");
                System.Console.WriteLine("The service is running in the following account: {0}", WindowsIdentity.GetCurrent().Name);
                System.Console.WriteLine("Press <ENTER> to terminate service.");
                System.Console.WriteLine();
                System.Console.ReadLine();
                // Close the ServiceHostBase to shutdown the service.
                serviceHost.Close();
            } 
        }

    }
}
