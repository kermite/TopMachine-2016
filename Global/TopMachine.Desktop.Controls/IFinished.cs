using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopMachine.Desktop.Controls
{
    public delegate void OnFinishedDelegate(); 

    public interface IFinished
    {
        event OnFinishedDelegate OnFinished; 
    }
}
