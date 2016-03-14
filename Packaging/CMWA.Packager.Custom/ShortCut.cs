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

namespace CMWA.Packager.Custom
{

    [CustomPackageItemEditor(typeof(ShortCutEditor))]
    public class ShortCut : IPackageItem
    {
        [XmlIgnore]
        public IPackageItem Parent { get; set; }
        
        public string Key { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public bool IsTranslation { get; set; }


        [EditorAttribute()]
        public List<ShortCut> Items { get; set; }

        public string Version { get; set; }
        public DateTime VersionDate { get; set; }

        public ShortCut()
        {
            Items = new List<ShortCut>();

            Version = "1.0.0";
            VersionDate = DateTime.Now;
        }

        #region IPackageItem Members


        public void Export(string srcPath, string targetPath)
        {
            return; // throw new NotImplementedException();
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
                target = target.GetPackageItem(filePath, path, start + 1, target);
                if (target == null)
                {
                    return null;
                }
            }

            return target;

        }
        #endregion

        #region IPackageItem Members


        public Stream GetStream(string rootPath)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
