using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopMachine.Training.FrontEnd
{
    public class Tools
    {
        public enum TypeStatus 
        { 
            NonJoue = 0,
            Trouve_Tous = 1,
            NonTrouve = 2,
            Isole = 3, 
            NonTrouveIsole = 4
        }
    }
}
