using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using CMWA.Packager;


namespace CMWA.Packager.Tools.Bytes
{

    internal class ByteAccessor
    {
        public byte[] ReadDirectFile(string filename)
        {
            string fn = FileUtility.GetFile(filename, LocationType.CommonFiles);
            if (File.Exists(fn))
            {
                int len = (int)new FileInfo(fn).Length;
                Byte[] b = new Byte[len];
                FileStream fs = File.OpenRead(fn);
                fs.Read(b, 0, len);
                fs.Close();
                return b;
            }

            return new byte[0]; 
        }

        public StreamReader GetStreamReader(string filename, LocationType type)
        {
            string fn = FileUtility.GetFile(filename, type);
            if (File.Exists(fn))
            {
                return new StreamReader(fn);
            }

            return null;
        }

        public StreamWriter GetStreamWriter(string filename, LocationType type)
        {
            string fn = FileUtility.GetFile(filename, type);
            return new StreamWriter(fn);
        }
      

        public void UpdateDirectFile(string filename, byte[] p, int pos, LocationType type)
        {
            string fn = FileUtility.GetFile(filename, type);
            if (File.Exists(fn))
            {
                FileStream fs = File.OpenWrite(fn);
                fs.Position = pos;
                fs.Write(p, 0, p.Length);
                fs.Close();
            }
            else
            {
                FileStream fs = File.Create(fn);
                fs.Position = pos;
                fs.Write(p, 0, p.Length);
                fs.Close();
            }
        }

        public byte[] ReadFileFromConfiguration(string key)
        {
            /*Package rf = PackageManager.Instance.GetFileRegistered(key);
            if (rf != null)
            {
                string fn = FileUtility.GetFile(rf.Filename, rf.LocationType);
                if (!rf.Registered)
                {
                    return new cpt().ReadFile(fn, "", 0);
                }

                return new cpt().ReadFile(fn, key, 1);
            }*/

            return new byte[0]; 
        }

        public MemoryStream ReadStreamFromConfiguration(string key)
        {
            /*Package rf = PackageManager.Instance.GetFileRegistered(key);
            
            if (rf != null)
            {
                string fn = FileUtility.GetFile(rf.Filename, rf.LocationType);
                if (!rf.Registered)
                {
                    return new MemoryStream(new cpt().ReadFile(fn, "", 0));
                }

                return new MemoryStream(new cpt().ReadFile(fn, key, 1));
            }*/

            
            return new MemoryStream();
        }
    }
}
