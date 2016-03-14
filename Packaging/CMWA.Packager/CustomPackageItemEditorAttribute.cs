using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMWA.Packager
{
    public class CustomPackageItemEditorAttribute : Attribute 
    {
        public Type Editor { get; set; }

        public CustomPackageItemEditorAttribute(Type editor)
        {
            Editor = editor; 
        }
    }
}
