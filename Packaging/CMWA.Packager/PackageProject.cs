using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Drawing;

namespace CMWA.Packager
{
    [DataContract]
    [Serializable]
    public class PackageProject : IPackageStream
    {
        [IgnoreDataMember]
        [XmlIgnore]
        public string FileName { get; set; }


        [IgnoreDataMember]
        [XmlIgnore]
        public string Path { get { return System.IO.Path.GetDirectoryName(FileName) + "\\"; } }

        [DataMember]
        public List<PackageFile> Packages { get; set; }

 
        public PackageProject()
        {
            Packages = new List<PackageFile>();
        }

        public void SaveProject(bool savePackages)
        {
            XmlSerializer xs = new XmlSerializer(typeof(PackageProject));
            StreamWriter sr = new StreamWriter(this.FileName);
            xs.Serialize(sr, this);
            sr.Dispose();

            foreach (PackageFile p in Packages)
            {
                p.Save(Path); 
            }
        }

        public void SaveProject(string package)
        {

            foreach (PackageFile p in Packages.Where(p=>p.Key == package))
            {
                p.Save(Path);
            }
        }

        public static PackageProject OpenProject(string fn, bool loadPackages)
        {
            PackageProject prj = new PackageProject();
            prj.FileName = fn; 


            if (System.IO.File.Exists(fn))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(PackageProject));
                    using (StreamReader sr = new StreamReader(fn))
                    {
                        prj = (PackageProject)xs.Deserialize(sr);
                    }
                }
                catch(Exception ee)
                {
                    Console.Write(ee.Message);
                }
            }

            prj.FileName = fn; 
            return prj;
        }

        public void Export(string path)
        {
            System.IO.File.Copy(FileName, path + "\\" + System.IO.Path.GetFileName(FileName), true); 

            foreach (PackageFile pf in Packages)
            {
                pf.Export(Path, path); 
            }
               
            return; 
        }

        public PackageFile GetPackage(string key)
        {
            return Packages.Where(p => p.Key == key).FirstOrDefault();
        }

        public IPackageItem GetPackageItem(string path)
        {
            IPackageItem pi = null;
            string[] p = path.Split(new char[] { '\\' });
            PackageFile pf = Packages.Where(pp => pp.Key == p[0]).FirstOrDefault();

            if (pf != null)
            {
                pi = pf.GetPackageItem(Path, path);

                 var pfDestination = PackageManager.Instance.Project.GetPackage(p[0]);
                if (!File.Exists(Path + @"\" + pfDestination.OriginalFile ) )
                {
                     var pfSource = PackageManager.Instance.Project.GetPackage("Original" + p[0]);
                    // var pfDestination = PackageManager.Instance.Project.GetPackage(p[0]);
                    System.IO.File.Copy(PackageManager.Instance.Project.Path + @"\" + pfSource.OriginalFile, PackageManager.Instance.Project.Path + @"\" + pfDestination.OriginalFile);
                    PackageManager.LoadProject(FileUtility.GetFile("TopMachine.xprj", LocationType.PersonalFiles));
                    pi = PackageManager.Instance.Project.GetPackageItem(path);
                }

                //if (pi == null)
                //{
                //    var pfSource = PackageManager.Instance.Project.GetPackage("Original" + p[0]);
                //    var pfDestination = PackageManager.Instance.Project.GetPackage(p[0]);
                //    System.IO.File.Copy(PackageManager.Instance.Project.Path + @"\" + pfSource.OriginalFile, PackageManager.Instance.Project.Path + @"\" + pfDestination.OriginalFile);
                //    PackageManager.LoadProject(FileUtility.GetFile("TopMachine.xprj", LocationType.PersonalFiles));
                //    pi = PackageManager.Instance.Project.GetPackageItem(path);

                //}
            }

            return pi; 
        }


        public Stream GetStream(string path)
        {

            IPackageItem ip = GetPackageItem(path);

            if (ip != null && ip.Link != null && ip.Link.Length > 0)
            {
                ip = GetPackageItem(ip.Link);
            }

            if (ip == null)
            {
                return null; 
            }

            return ((IPackageStream)ip).GetMemoryStream(Path);
        }

        public MemoryStream GetMemoryStream(string path)
        {

            IPackageItem ip = GetPackageItem(path);

            if (ip != null && ip.Link != null && ip.Link.Length > 0)
            {
                ip = GetPackageItem(ip.Link);
            }

            if (ip == null)
            {
                return null;
            }

            return ((IPackageStream) ip).GetMemoryStream(Path);
        }

        public byte[] GetBytes(string path)
        {
            Stream s = null;

            IPackageItem ip = GetPackageItem(path);

            if (ip != null && ip.Link != null && ip.Link.Length > 0)
            {
                ip = GetPackageItem(ip.Link);
            }

            if (ip == null)
            {
                return null;
            }

            return ((IPackageStream)ip).GetBytes(Path);
        }


        public StreamWriter GetStreamWriter(string path)
        {
            Stream s = null;

            IPackageItem ip = GetPackageItem(path);

            if (ip != null && ip.Link != null && ip.Link.Length > 0)
            {
                ip = GetPackageItem(ip.Link);
            }

            if (ip == null)
            {
                return null;
            }

            return ((IPackageStream)ip).GetStreamWriter(Path);
        }

        public Bitmap GetBitmap(string path)
        {

            IPackageItem ip = GetPackageItem(path);

            if (ip != null && ip.Link != null && ip.Link.Length > 0)
            {
                ip = GetPackageItem(ip.Link);
            }

            if (ip == null)
            {
                return null;
            }

            return ((IPackageStream)ip).GetBitmap(Path);
        }


        public string GetFileName(string path)
        {
            Stream s = null;

            IPackageItem ip = GetPackageItem(path);

            if (ip != null && ip.Link != null && ip.Link.Length > 0)
            {
                ip = GetPackageItem(ip.Link);
            }

            if (ip == null)
            {
                return null;
            }

            return ((IPackageStream)ip).GetFileName(Path);
        }

    }
}
