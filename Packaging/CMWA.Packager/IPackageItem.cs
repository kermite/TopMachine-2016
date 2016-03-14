using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.IO;

namespace CMWA.Packager
{
    public interface IPackageItem
    {
        string Key { get; set; }
        bool IsTranslation { get; set; }
        IPackageItem Parent { get; set; }
        string Link { get; set; }

        void Export(string srcPath, string targetPath);
        IPackageItem GetPackageItem(string filePath, string[] path, int start, IPackageItem parent);

    }
}
