using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace CMWA.Packager
{
    [Serializable]
    public class PackageManager
    {
        #region Properties
        public PackageProject Project { get; set; }
        #endregion


        private static PackageManager _instance = null;
        
        public static PackageManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PackageManager();
                }

                return _instance; 
            }
        }

        public static void SetProject(PackageProject project)
        {
            Instance.Project = project;
        }

        public static void LoadProject(string fn)
        {
            Instance.Project =  PackageProject.OpenProject(fn, true);
        }


    }



}
