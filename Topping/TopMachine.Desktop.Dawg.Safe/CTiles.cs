using System;
using System.Collections.Generic;
using System.Text;

namespace TopMachine.Topping.Dawg
{
    public class CTiles
    {
        char chartocode(char c, CBoardMaker cm)
        {
            if (c == '?')
            {
                return (char) DicoConstants.JOKER_TILE;
            }
            else
            {
                return (char) cm.GetTileCode(c);
            }

            return (char) 0;
        }



        char
        codetochar(char t, CBoardMaker cm)
        {
            return (char) cm.Tiles_ascii[t - 1];
        }

    }
}
