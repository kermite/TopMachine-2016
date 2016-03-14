using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace TopMachine.Desktop.Overall
{
    public class ObjectSerializer<T>
    {
        public string Serialize(T o)
        {
            XmlSerializer xs = new XmlSerializer(o.GetType());
            using (StringWriter sr = new StringWriter())
            {
                xs.Serialize(sr, o);
                string s = sr.ToString();
                return s;
            }
        }

        public string SerializeToFile(T o, string file)
        {
            XmlSerializer xs = new XmlSerializer(o.GetType());
            using (StreamWriter sr = new StreamWriter(file))
            {
                xs.Serialize(sr, o);
                string s = sr.ToString();
                return s;
            }
        }

        public T DeserializeFromFile(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamReader sr = new StreamReader(file))
            {
                T o = (T)xs.Deserialize(sr);
                return o;
            }
        }


        public T Deserialize(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(xml))
            {
                T o = (T)xs.Deserialize(sr);
                return o;
            }
           
        }
    }
}
