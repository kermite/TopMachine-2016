using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Drawing;
using CMWA.Packager.Tools.Bytes;

namespace CMWA.Packager.Custom
{

    public class PackageAttribute
    {
        public string Attribute { get; set; }
        public string Value     { get; set; }

        public PackageAttribute()
        {
        }
    }

    [CustomPackageItemEditor(typeof(PackagerForm))]
    public class Package : IPackageItem, IPackageStream
    {
        [XmlIgnore]
        public IPackageItem Parent { get; set; }      
        public string Key { get; set; }
        public bool IsTranslation { get; set; }
        public string Link { get; set; }


        public bool Registered { get; set; }
        public bool Crypted { get; set; }
        public bool Enabled { get; set; }

        public string Filename { get; set; }
        public string Description { get; set; }

        public LocationType LocationType { get; set; }
        public FileType FileType { get; set; }

        [EditorAttribute()]
        public List<Package> Items { get; set; }

       /* [EditorAttribute(typeof(FileNameEditor), typeof(UITypeEditor))] */
        public string OriginalFile { get; set; }

        public string Version { get; set; }
        public DateTime VersionDate { get; set; }

        public List<PackageAttribute> Attributes { get; set; }

        public Package()
        {
            Items = new List<Package>();

            Version = "1.0.0";
            VersionDate = DateTime.Now;

            Registered = false;
            Crypted = false;
            Enabled = true;

            LocationType = Packager.LocationType.CustomFiles;
            FileType = Packager.FileType.File; 

        }

        public void AddPackage(string path, Package p)
        {
            if (p.FileType == Packager.FileType.Assembly)
            {
                string src = ""; 
                if (p.OriginalFile[1] != ':')
                {
                    src = path + p.OriginalFile;
                }
                else
                {
                    src = p.OriginalFile;
                }

                string s = new AssemblyTools().GetAssemblyName(src);
                p.Key = s; 
            }

            Items.Add(p);
        }

        public void AddDictionary(string dico, string desc, string fn, string package, LocationType lt)
        {
            Package cfg = new Package {Description=desc, Key = dico, Filename = fn, LocationType = lt };
            this.Items.Add(cfg);
        }

        #region IPackageItem Members


        public void Export(string srcPath, string targetPath)
        {
            foreach(Package p in Items)
            Export(srcPath, targetPath, "\\", p);
            return; // throw new NotImplementedException();
        }

        public void Export(string srcPath, string targetPath, string extraPath, Package pack)
        {
            if (pack.FileType == Packager.FileType.Folder)
            {
                string p = targetPath + extraPath + pack.Filename;
                if (!Directory.Exists(p))
                {
                    Directory.CreateDirectory(p);
                }

                foreach (Package pp in pack.Items)
                {
                    Export(srcPath, targetPath, extraPath + pack.Filename + "\\", pp);
                }
            }
            else
            {
                if (pack.Filename != null && pack.Filename.Length > 0)
                {
                    string src = "";
                    string tgt = "";

                    if (pack.OriginalFile[1] != ':')
                    {
                        src = srcPath + pack.OriginalFile;
                    }
                    else
                    {
                        src = pack.OriginalFile;
                    }
                    tgt = targetPath + extraPath + pack.Filename;

                    if (pack.Crypted == false)
                    {
                        try
                        {
                            System.IO.File.Copy(src, tgt, true); //, path + "\\" + System.IO.Path.GetFileName(FileName));
                        }
                        catch
                        {
                            ;
                        }
                    }
                    else
                    {
                        cpt c = new cpt();
                        if (pack.Registered == false)
                        {
  
                            c.WriteFile(src, tgt, "", 0);

                            string text =  Encoding.ASCII.GetString(c.ReadFile(tgt, "", 0));
                            Console.Write(text.Length);
                        }
                        else
                        {
                            c.WriteFile(src, tgt, pack.Key,1);
                        }
                    }

                }
            }

        }

        public byte[] GetCryptedBytes(string rootPath)
        {
            string path = GetPath(rootPath);
            if (System.IO.File.Exists(path))
            {

                MemoryStream ms = new MemoryStream();
                byte[] b = File.ReadAllBytes(path);
                return b;

            }

            return null;
        }

        public IPackageItem GetPackageItem(string filePath, string[] path, int start, IPackageItem parent)
        {
            IPackageItem target = null;

            this.Parent = parent;

            if (path[start] == this.Key && path.Length == start + 1)
            {
                return this;
            }

            target = this.Items.Where(pp => pp.Key == path[start + 1]).FirstOrDefault();

            if (target != null)
            {
                target = target.GetPackageItem(filePath, path, start + 1, this);
                if (target == null)
                {
                    return null;
                }
            }

            return target;

        }

        private string GetPath(string rootPath)
        {
           string path = this.Filename;
            
            Package p = (Package) this.Parent;
            while (p.Parent != null)
            {
                path = p.Filename + "\\" + path;
                p = (Package) p.Parent;
            }

           // path = rootPath + "\\" + path;

            path = FileUtility.GetFile(path, LocationType);
            return path;
        }

        public string GetFileName(string rootPath)
        {
            return GetPath(rootPath);
        }

        public Stream GetStream(string rootPath)
        {
            string path = GetPath(rootPath);

            if (System.IO.File.Exists(path))
            {
                return File.OpenRead(path); 
            }

            return null; 
        }

        public StreamWriter GetStreamWriter(string rootPath)
        {
            string path = GetPath(rootPath);

            if (System.IO.File.Exists(path))
            {
                return new StreamWriter(path, false);
            }

            return null;
        }

        public MemoryStream GetMemoryStream(string rootPath)
        {
            string path = GetPath(rootPath);
            if (System.IO.File.Exists(path))
            {
                MemoryStream ms = new MemoryStream(); 
                byte[] b = File.ReadAllBytes(path);
                ms.Write(b, 0, b.Length);
                return ms; 
            }

            return null;
        }

        public Bitmap GetBitmap(string rootPath)
        {
            string path = GetPath(rootPath);
            if (System.IO.File.Exists(path))
            {
                return new Bitmap(path);
            }

            return null;
        }

        public byte[] GetBytes(string rootPath)
        {
            string path = GetPath(rootPath);
            if (System.IO.File.Exists(path))
            {
                if (Crypted)
                {
                    cpt c = new cpt();
                    if (Registered)
                    {
                        return c.ReadFile(path, Key, 1);
                    }
                    else
                    {
                        return c.ReadFile(path, "", 0);
                    }
                }
                else
                {
                    MemoryStream ms = new MemoryStream();
                    byte[] b = File.ReadAllBytes(path);
                    return b;
                }

            }

            return null;
        }


        #endregion

    }

}
