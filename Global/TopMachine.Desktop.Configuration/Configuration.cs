using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using TopMachine.Desktop.Overall;

namespace TopMachine.Desktop.Configuration
{
    [DataContract]
    public class Configuration
    {
        public static Configuration _instance = null; 

        public static Configuration Instance
        {
            get 
            {
                if (_instance == null)
                {
                   _instance = ReadConfiguration(); 
                }

                return _instance; 
            }
        }

        [DataMember]
        public Urls Urls { get; set; }

        public Configuration()
        {
            Urls = new Urls(); 
        }

        public static Configuration ReadConfiguration() 
        {
            Configuration cfg = null;
            try
            {
                string fn = AppDomain.CurrentDomain.BaseDirectory + "\\TopMachine.config";

                if (System.IO.File.Exists(fn))
                {
                    string xml = System.IO.File.ReadAllText(fn);
                    cfg = new TopMachine.Desktop.Overall.ObjectSerializer<Configuration>().Deserialize(xml);
                }

            }
            catch (Exception)
            {
            }

            return cfg == null ? new Configuration() : cfg;
        }

        public void WriteConfiguration()
        {
            try
            {
                string fn = AppDomain.CurrentDomain.BaseDirectory + "\\TopMachine.config";
                string xml = new TopMachine.Desktop.Overall.ObjectSerializer<Configuration>().Serialize(this);
                System.IO.File.WriteAllText(fn, xml);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    [DataContract]
    public class Urls
    {
        [DataMember]
        public string TopMachineWebSite { get; set; }
        [DataMember]
        public string ToppingService    { get; set; }
    }

}
