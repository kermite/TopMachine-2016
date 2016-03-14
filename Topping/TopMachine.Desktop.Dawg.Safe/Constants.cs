using System;
using System.Collections.Generic;
using System.Text;

namespace TopMachine.Topping.Dawg
{
    public class DicoConstants
    {
        public const int LETTERS = 31;
        public const int DIC_WORD_MAX = 45;
        public const int RES_7PL1_MAX = 200;
        public const int RES_RACC_MAX = 200;
        public const int RES_BENJ_MAX = 200;
        public const int RES_CROS_MAX = 200;
        public const string _COMPIL_KEYWORD_ = "_COMPILED_DICTIONARY_";

        public const int RESULTS_INTERNAL_MAX = 300;

        public const int ROUND_INTERNAL_MAX = 31;

        public const int FROMBOARD = 0x1;
        public const int FROMRACK = 0x2;
        public const int JOKER = 0x4;
        public const int JOKER_TILE = 0;

        public const int VERTICAL = 0;
        public const int HORIZONTAL = 1;

        public const int CROSS_MASK = 0x7FFFFFE;
    }
}
