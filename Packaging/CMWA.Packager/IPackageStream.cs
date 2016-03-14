using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.IO;
using System.Drawing;

namespace CMWA.Packager
{
    public interface IPackageStream
    {
        string GetFileName(string rootPath);
        Stream GetStream(string rootPath);
        StreamWriter GetStreamWriter(string rootPath);
        MemoryStream GetMemoryStream(string rootPath);
        Bitmap GetBitmap(string rootPath);
        byte[] GetBytes(string rootPath);
    }
}
