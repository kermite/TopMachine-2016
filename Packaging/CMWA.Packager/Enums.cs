using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMWA.Packager
{
    public enum LocationType
    {
        ProgramFiles = 0,
        CommonFiles = 1,
        PersonalFiles = 2,
        CustomFiles = 3
    }

    public enum FileType
    {
        File = 0,
        Assembly = 1, 
        Image = 2, 
        Resource = 3,
        PackageDefinition = 4,
        RootFiles = 100,
        RootZip = 101, 
        RootModule = 102,
        Folder = 200
    }

}
