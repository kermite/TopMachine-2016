using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMWA.Packager
{
    public interface ICustomPackageItemEditor
    {
        void SetObject(Type baseType, PackageProject prj, PackageFile o);
    }
}
