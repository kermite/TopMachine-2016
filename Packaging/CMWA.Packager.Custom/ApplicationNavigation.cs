using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace CMWA.Packager.Custom
{
    public enum ApplicationNavigationAction 
    {

        Navigate = 0,
        Link = 1,
        OpenControl = 11, 
        OpenForm = 12, 
        Settings = 13, 
        Administration = 14,
        Quit = 100
    }

    [CustomPackageItemEditor(typeof(ApplicationNavigationEditor))]
    public class ApplicationNavigation : IPackageItem
    {
        public ApplicationNavigationAction Action { get; set; }
        public bool IsTranslation { get; set; }
        public string Image { get; set; }
        public bool Enabled { get; set; }
        public string IsGroup { get; set; }
        public string Link { get; set; }

        public List<ApplicationNavigation> Items { get; set; }

        public string TargetControl { get; set; }
        public string TargetAssembly { get; set; }

        public ApplicationNavigation()
        {
            Items = new List<ApplicationNavigation>();
        }

        #region IPackageItem Members
        public string Key { get; set; }

        [XmlIgnore]
        public IPackageItem Parent { get; set; }

        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPackageItem Members


        public void Export(string srcPath, string targetPath)
        {
           return; // ++ throw new NotImplementedException();
        }

        public IPackageItem GetPackageItem(string filePath, string[] path, int start, IPackageItem parent)
        {
            IPackageItem target = null;

            this.Parent = parent;

            if (path[start] == this.Key && path.Length == start+1)
            {
                return this;
            }

            target = this.Items.Where(pp => pp.Key == path[start+1]).FirstOrDefault();

            if (target != null)
            {
                target = target.GetPackageItem(filePath, path, start+1, target);
                if (target == null)
                {
                    return null;
                }
            }

            return target;

        }

        #endregion


        #region IPackageItem Members


        public System.IO.Stream GetStream(string rootPath)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
