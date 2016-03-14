using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace CMWA.Packager
{
    [Serializable]
    [DataContract]
    public class PackageFile /*:  IPackageItem*/
    {
      /*  [EditorAttribute(typeof(FileNameEditor), typeof(UITypeEditor))]*/
        [DataMember]
        public string OriginalFile { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [XmlIgnore]
        public IPackageItem Object { get; set; }

        public PackageFile()
        {
        }

        public PackageFile(string key, string fn, string type)
        {
            Key = key;
            OriginalFile = fn;
            TypeName = type; 
        }

        #region IPackageItem Members
        public string Key
        {
            get;
            set;
        }

        public bool IsTranslation { get; set; }
        #endregion

        public void GetOrCreateObject(string path)
        {
            if (Object == null)
            {
                Read(path);
            } 
        }

        public void Export(string SourcePath, string TargetPath)
        {
            string src = "";
            string tgt = ""; 

            if (OriginalFile[1] != ':')
            {
                src = SourcePath + OriginalFile;
                tgt = TargetPath + "\\" + OriginalFile;
            }
            else
            {
                src = OriginalFile;
                tgt = TargetPath + Path.GetFileName(OriginalFile); 
            }

            System.IO.File.Copy(src, tgt, true); //, path + "\\" + System.IO.Path.GetFileName(FileName));

            GetOrCreateObject(SourcePath);
            Object.Export(SourcePath, TargetPath); 
        }

        public void Save(string path)
        {
            if (Object != null)
            {
                string fn = null;
                if (OriginalFile[1] != ':')
                {
                    fn = path + OriginalFile;
                }
                else
                {
                    if (OriginalFile.StartsWith(path))
                    {
                        OriginalFile = OriginalFile.Substring(path.Length);
                    }
                }

                XmlSerializer xs = new XmlSerializer(Object.GetType());
                StreamWriter sr = new StreamWriter(fn);
                xs.Serialize(sr, Object);
                sr.Dispose();
            }
        }

        public void Read(string path)
        {

            string fn = null;
            if (OriginalFile[1] != ':')
            {
                fn = path + OriginalFile;
            }
            else
            {
                if (OriginalFile.StartsWith(path))
                {
                    OriginalFile = OriginalFile.Substring(path.Length);
                }
            }


            if (System.IO.File.Exists(fn))
            {
                XmlSerializer xs = new XmlSerializer(Type.GetType(TypeName, false, true));
                StreamReader sr = new StreamReader(fn);
                Object = (IPackageItem) xs.Deserialize(sr);
                sr.Dispose();
            }
            else
            {
                Object = (IPackageItem) Type.GetType(TypeName, false, true).GetConstructor(new Type[0]).Invoke(new Type[0]);
                Object.Key = this.Key; 
            }

        }


        #region IPackageItem Members
        public IPackageItem GetPackageItem(string filePath, string path)
        {
            IPackageItem pi =  null;
            GetOrCreateObject(filePath);
            pi = Object.GetPackageItem(filePath, path.Split(new char[] { '\\' }), 0, null);
            return pi;
        }


        public void AddItem(object item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
