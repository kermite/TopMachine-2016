using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CMWA.Packager
{
    public class AssemblyTools
    {
        public string GetAssemblyName(string fn)
        {
            try
            {
                Assembly asm = Assembly.LoadFile(fn); //.ReflectionOnlyLoadFrom(fn);
                string an = asm.FullName;
                asm = null;
                return an;
            } catch
            {
                return "NOASSEMBLY";
            }

        }
    }
}
