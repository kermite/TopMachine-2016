
using System;
using System.IO;
using System.Collections;
using TopMachine.Topping.Dawg;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Engine.GameController.Search;


namespace TopMachine.Topping.Engine.GameController.RoundManager
{
    /// <summary>
    /// Summary description for CAutoRoundManager.
    /// </summary>
    public class CExplosiveRoundPlusManager : CRoundManager
    {

        private ArrayList rounds;

        public class RoundsComparer : IComparer
        {
            // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
            int IComparer.Compare(object x, object y)
            {
                return (((RatingRound)x).scoreAll < ((RatingRound)y).scoreAll) ? 0 : 1;
            }

        }

        bool isOds5 = false;
        dawgDictionary dico;


        bool doCollages = false;
        bool doScrabble = false;
        bool doAppuis = false;
        bool doRaccords = false;
        bool doRack = false;


        public class RatingRound
        {
            public bool NoResults = false;

            public bool Valid = true;
            public bool rejet = false;
            public int nbSolutions = 0;
            public int nbTirages = 0;
            public float scoreAll = 0;
            public float scorerack = 1;
            public float scoresoustop = 0;
            public float scorescrabble = 0;
            public float scoreraccords = 0;
            public float scorecollage = 0;
            public float scorecollagemots = 0;
            public float scoremot = 0;
            public float scoreappui = 0;

            public CRack rack;
            public ShuffleBag bag;
            public CResults results;

            public RatingRound(GridConfig gc)
            {
                rack = new CRack(gc.cm);
                bag = new ShuffleBag(gc.cm);
            }


        }

        public int[] Tiles_points = new int[]
		{
			    /* x A B C D  E F G H I J  K L M N O P Q R S T U V  W  X  Y  Z ? */
			    0,1,3,3,2, 1,4,2,4,1,8,10,1,2,1,1,3,8,1,1,1,1,4,10,10,10,10,0
		};


        public const int RescanResultNumber = 4;
        public const float sousTopRatioDivScrabble = 50;
        public const float sousTopRatioDiv = 20;
        public const float scoreMotRatioMul = 1;

        public const float RackRatioMult = 1;           // Multiplier
        public const float ScrabbleRatioDiv = 50;
        public const float RaccordsRatioMul = (1);
        public const float CollageRatioDiv = ((float)5) / 10;
        public const float CollageMotRatioDiv = 4;
        public const float AppuiRatioDiv = 4;

        public int ScrabbleFrequence = 60;
        public int AppuisFrequence = 80;
        public int CollagesFrequence = 80;
        public int RaccordsFrequence = 80;
        public int RackFrequence = 40;

        private int maxTurn = 30;

        public CExplosiveRoundPlusManager(GameControllers gctls, GameCfg cfg)
            : base(gctls, cfg)
        {

        }

        public override bool NewTirage(IRackManager RackMan)
        {
            bool ok = false;
            try
            {
                ok = NewTiragePv(RackMan);
            }
            catch (Exception ee)
            {
               // NLog.LogManager.GetLogger("wcf").ErrorException("CExplosiveRoundManager : NewTirage", ee);
            }

            return ok;

        }

        private bool NewTiragePv(IRackManager RackMan)
        {   // choisi un tirage de maniere aleatoire

            if (gctls.RackManager.IsEndGame())
            {
                return false;
            }

            Random rnd = new Random();

            ScrabbleFrequence = 60;  //cfg.Config.intScrabbleFrequence;
            CollagesFrequence = 90;  //cfg.Config.intCollagesFrequence;
            AppuisFrequence = 90;  //cfg.Config.intAppuisFrequence;
            RaccordsFrequence = 90; //  cfg.Config.intRaccordsFrequence;
            RackFrequence = 30; //  cfg.Config.intRackFrequence; 

            RatingRound selected = null;


            RatingRound maxRound = null;
            int nbTirages = 0;

            try
            {

                GameConfig.iTurn++;

                rounds = new ArrayList();

                int MaxScore = gc.Config.ExplosiveRating;

                if (isOds5) MaxScore = 1;

                GameConfig.OldRack = new CRack(gc.GridConfig.cm);
                GameConfig.OldRack.copy(GameConfig.Rack);
                GameConfig.OldRack.AlignTiles();

                ShuffleBag OldBag = new ShuffleBag(gc.GridConfig.cm);
                OldBag.cm = gc.GridConfig.cm;
                CRack OldRack = new CRack(gc.GridConfig.cm);
                RackMan.copyTo(OldRack, OldBag);

                int c = rnd.Next(100);

                doCollages = doScrabble = doAppuis = false;

                if (c < CollagesFrequence)
                {
                    doCollages = true;
                }

                c = rnd.Next(100);

                if (c < ScrabbleFrequence)
                {
                    doScrabble = true;
                }

                c = rnd.Next(100);
                if (c < AppuisFrequence)
                {
                    doAppuis = true;
                }

                c = rnd.Next(100);
                if (c < RaccordsFrequence)
                {
                    doRaccords = true;
                }

                c = rnd.Next(100);
                if (c < RackFrequence)
                {
                    doRack = true;
                }

                doAppuis = doCollages = true;


                int tot = rnd.Next(80) + 20;

                System.Collections.Generic.List<string> tirages = new System.Collections.Generic.List<string>();
                for (int y = 0; y < tot; y++)
                {
                    nbTirages++;
                    RackMan.copy(OldRack, OldBag);
                    ProcessTirages(RackMan, GameConfig.Config.MaxLetters, tirages);
                    if (gc.iTurn == 1) break;
                }


                int selectedround = 0;

                selectedround = EvaluateTirages(RackMan, GameConfig.Config.MaxLetters);
                selected = (RatingRound)rounds[selectedround];

                if (maxRound == null)
                {
                    maxRound = selected;
                }


                if (!selected.NoResults)
                {

                    if (selected.scoreAll >= MaxScore)
                    {
                        if (maxRound.scoreAll < selected.scoreAll)
                        {
                            maxRound = selected;
                        }
                    }
                }

                if (maxRound.scoreAll < selected.scoreAll)
                {
                    if (selected.Valid)
                    {
                        maxRound = selected;
                    }
                }


                selected = null;

            }



            catch (Exception ee)
            {
                if (selected == null) return false;
            }

            if (maxRound != null)
            {
                selected = maxRound;
            }

            //if (selected.rejet)
            //{
            GameConfig.OldRack.empty();
            //}
            selected.Valid = true;
            selected.nbTirages = nbTirages;

            // serialize(selected);

            while (selected.results.Results.list.Count > 1)
            {
                selected.results.Results.list.RemoveAt(1);
            }

            CRound tround = (CRound)selected.results.Results.list.GetByIndex(0);


            int playedTot = selected.rack.ntiles();
            int[] played = new int[playedTot];


            //string oldRack = selected.rack.GetRackString();

            for (int x = 0; x < tround.wordlen(); x++)
            {
                int tile = tround.gettile(x);
                if (tround.GetTileOrigin(x) == DicoConstants.FROMRACK)
                {
                    for (int y = 0; y < selected.rack.ntiles(); y++)
                    {
                        if (selected.rack.GetRackTile(y) == tile)
                        {
                            if (played[y] == 0)
                            {
                                played[y] = 1;
                                break;
                            }
                        }
                    }
                }
            }

            int t = 0;

            while (t < playedTot && selected.rack.ntiles() > gc.Config.MaxLetters)
            {
                if (played[t] == 0)
                {
                    int y = selected.rack.GetRackTile(t);
                    selected.rack.remove(y);
                    selected.bag.replacetile(y);
                }
                t++;
            }
            selected.rack.AlignTiles();


            gc.Rack.copy(selected.rack);
            gc.Bag.copy(selected.bag);

            gc.Rack.AlignTiles();
            selected.rack.AlignTiles();

            RackMan.copy(gc.Rack, gc.Bag);
            base.NewTirage();

            // We throw extra tiles in the bag 

            return true;
        }

        private void serialize(RatingRound r)
        {
            int x = 0;

            string s = "";

            if (gc.iTurn == 1)
            {
                s += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            }

            s += "<Round>";
            s += "<Date>" + DateTime.Now.ToLongTimeString() + "</Date>";
            s += "<Number>" + gc.iTurn + "</Number>";
            s += "<DoScrabble>" + doScrabble + "</DoScrabble>";
            s += "<DoAppuis>" + doAppuis + "</DoAppuis>";
            s += "<DoCollages>" + doCollages + "</DoCollages>";


            s += "<RatingRound>";
            s += "<NbTirages>" + r.nbTirages + "</nbTirages>";
            s += "<Valid>" + r.Valid + "</Valid>";
            s += "<Solutions>" + r.nbSolutions + "</Solutions>";
            s += "<ScoreAll>" + r.scoreAll + "</ScoreAll>";
            s += "<ScoreRack>" + r.scorerack + "</ScoreRack>";
            s += "<ScoreSousTop>" + r.scoresoustop + "</ScoreSousTop>";
            s += "<ScoreRaccords>" + r.scoreraccords + "</ScoreRaccords>";
            s += "<ScoreCollage>" + r.scorecollage + "</ScoreCollage>";
            s += "<ScoreCollageMots>" + r.scorecollagemots + "</ScoreCollageMots>";
            s += "<ScoreScrabble>" + r.scorescrabble + "</ScoreScrabble>";
            s += "<ScoreMot>" + r.scoremot + "</ScoreMot>";
            s += "<ScoreAppui>" + r.scoreappui + "</ScoreAppui>";
            s += "<Tirage>" + GetTirage(r.rack) + "</Tirage>";

            CRound tround = (CRound)r.results.Results.list.GetByIndex(0);

            s += "<MotJoue>" + tround.getword() + "</MotJoue>";
            s += "<Position>" + tround.row().ToString() + " - " + tround.column().ToString() + "</Position>";
            s += "<Points>" + tround.points().ToString() + "</Points>";
            s += "<SousTop>" + r.results.sousTop.ToString() + "</SousTop>";
            s += "</RatingRound>";
            s += "</Round>\r\n";


            if (gc.iTurn == 1)
            {
                System.IO.File.WriteAllText("c:\\rounds.xml", s);
            }
            else
            {
                System.IO.File.AppendAllText("c:\\rounds.xml", s);
            }

            /*   StreamWriter sw = new StreamWriter("c:\\rounds_" + gc.iTurn.ToString() + ".xml");
               sw.Write(s);
               sw.Close();*/


        }

        private void serialize(IRackManager RackMan, int sel)
        {
            int x = 0;

            string s = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            s += "<Rounds>";

            s += "<Selected>" + sel.ToString() + "</Selected>";
            s += "<DoScrabble>" + doScrabble + "</DoScrabble>";
            s += "<DoAppuis>" + doAppuis + "</DoAppuis>";
            s += "<DoCollages>" + doCollages + "</DoCollages>";

            foreach (RatingRound r in rounds)
            {
                s += "<RatingRound>";
                s += "<Index>" + (x++) + "</Index>";
                s += "<Valid>" + r.Valid + "</Valid>";
                s += "<Solutions>" + r.nbSolutions + "</Solutions>";
                s += "<ScoreAll>" + r.scoreAll + "</ScoreAll>";
                s += "<ScoreRack>" + r.scorerack + "</ScoreRack>";
                s += "<ScoreSousTop>" + r.scoresoustop + "</ScoreSousTop>";
                s += "<ScoreRaccords>" + r.scoreraccords + "</ScoreRaccords>";
                s += "<ScoreCollage>" + r.scorecollage + "</ScoreCollage>";
                s += "<ScoreScrabble>" + r.scorescrabble + "</ScoreScrabble>";
                s += "<ScoreMot>" + r.scoremot + "</ScoreMot>";
                s += "<ScoreAppui>" + r.scoreappui + "</ScoreAppui>";
                s += "<Tirage>" + GetTirage(r.rack) + "</Tirage>";

                CRound tround = (CRound)r.results.Results.list.GetByIndex(0);

                s += "<MotJoue>" + tround.getword() + "</MotJoue>";
                s += "<Position>" + tround.row().ToString() + " - " + tround.column().ToString() + "</Position>";
                s += "<Points>" + tround.points().ToString() + "</Points>";
                s += "<SousTop>" + r.results.sousTop.ToString() + "</SousTop>";
                s += "</RatingRound>";

            }

            s += "</Rounds>";

            StreamWriter sw = new StreamWriter("c:\\rounds_" + gc.iTurn.ToString() + ".xml");
            sw.Write(s);
            sw.Close();


        }

        private int EvaluateTiragesODS5(IRackManager RackMan)
        {
            int maxItem = 0;
            int item = 0;



            item = 0;
            foreach (RatingRound round in rounds)
            {
                round.Valid = false;
                foreach (CRound r in round.results.Results.list.Values)
                {
                    string s = r.getword();
                    if (dico.SearchWord(s) > 0)
                    {
                        round.Valid = true;
                    }
                }
            }

            return 0;
        }

        private int EvaluateTirages(IRackManager RackMan, int maxLetters)
        {
            float maxScore = 0;
            int maxItem = 0;
            int item = 0;


            foreach (RatingRound round in rounds)
            {
                if (round.results.Results.list.Count > 0)
                {
                    CRound tround = (CRound)round.results.Results.list.GetByIndex(0);

                    RackMan.copy(round.rack, round.bag);
                    EvaluateNumberOfSolutions(round, tround);


                    if (doCollages)
                    {
                        EvaluateCollagesMots(round, tround);
                        EvaluateCollages(round, tround);
                    }

                    if (doAppuis)
                    {
                        EvaluateScoreAppui(round, tround);
                    }

                    if (doScrabble)
                    {
                        EvaluateScrabble(round, tround);
                    }
                    else
                    {
                        if (tround.bonus() > 0)
                        {
                            round.Valid = false;
                            break;
                        }
                    }


                    if (gc.iTurn > 1 && (doScrabble || doCollages || doAppuis))
                    {
                        if (round.scoreAll == 0)
                        {
                            round.Valid = false;
                        }
                    }
                }
                else
                {
                    round.NoResults = true;
                }

            }

            RoundsComparer rc = new RoundsComparer();
            rounds.Sort(rc);

            maxItem = -1;
            item = 0;
            double totItems = 0;
            foreach (RatingRound round in rounds)
            {
                if (round.Valid && !round.NoResults)
                {
                    CRound tround = (CRound)round.results.Results.list.GetByIndex(0);

                    if (doRaccords)
                    {
                        EvaluateRaccords(round, tround);
                    }

                    EvaluateScoreMot(round, tround);

                    if (doRack)
                    {
                        EvaluateRack(round, tround);
                    }

                    if (!doScrabble)
                    {
                        EvaluateSousTop(round, tround);
                    }



                    float ratio = (float) tround.wordlen() / 5;
                    round.scoreAll = round.scoreAll * ratio;


                    totItems += round.scoreAll;
                    if (round.scoreAll > maxScore || maxScore == 0)
                    {
                        maxScore = round.scoreAll;
                        maxItem = item;
                    }
                    if (item == RescanResultNumber)
                    {
                        break;
                    }
                }
                item++;
            }


            if (maxItem == -1)
            {
                item = 0;
                foreach (RatingRound round in rounds)
                {
                    if (!round.NoResults)
                    {
                        CRound tround = (CRound)round.results.Results.list.GetByIndex(0);
                        if (doScrabble)
                        {
                            EvaluateScrabble(round, tround);
                        }
                        EvaluateScoreAppui(round, tround);
                        EvaluateRaccords(round, tround);

                        if (round.scoreAll > maxScore || maxScore == 0)
                        {
                            maxScore = round.scoreAll;
                            maxItem = item;
                        }
                        item++;
                    }
                }
            }
            return maxItem;
        }

        private void EvaluateScoreMot(RatingRound round, CRound tround)
        {
            if (tround.bonus() > 0)
            {
                string s = tround.getword();

                if (s.Length > 4)
                {
                    round.scoremot = 2;

                }

                if (s.Length < 4)
                {
                    round.scoremot = -15;
                }

                round.scoreAll += (float)round.scoremot * scoreMotRatioMul / round.nbSolutions;
            }
        }

        private void EvaluateScoreAppui(RatingRound round, CRound tround)
        {
            string s = tround.getword();


            if (s.Length < 5)
            {
                return;
            }

            int c = 0;
            for (int x = 0; x < s.Length; x++)
            {
                int xx = tround.playedfromrack(x);
                if (xx == 2)
                {
                    c++;
                }
            }

            c = s.Length - c;

            if (c > 1 && c < s.Length - 1)
            {
                round.scoreappui = c;
                round.scoreAll += (float)round.scoreappui * AppuiRatioDiv / round.nbSolutions;
            }
        }

        private void EvaluateCollages(RatingRound round, CRound tround)
        {
            int length = tround.getword().Length;
            string s = tround.getword();

            int count = gc.GameBoard.evalRaccords(tround);
            if ((count > 1 && length > 3) || (s.IndexOfAny(new char[] { 'J', 'K', 'Q', 'X', 'Y', 'Z' }) >= 0 && length > 2))
            {
                round.scorecollage = count;
                round.scoreAll += (float)count / (CollageRatioDiv * round.nbSolutions);
            }
        }

        private void EvaluateCollagesMots(RatingRound round, CRound tround)
        {
            int length = tround.getword().Length;
            string s = tround.getword();

            int count = ((CBoard)gc.GameBoard).evalRaccordsMots(tround);

            if (count > 3)
            {
                round.scorecollagemots = count;
                round.scoreAll += (float)count / (CollageMotRatioDiv * round.nbSolutions);
            }



        }

        private void EvaluateNumberOfSolutions(RatingRound round, CRound tround)
        {

            SortedList sl = new SortedList();

            foreach (CRound cround in round.results.Results.list.Values)
            {
                string s = cround.getword();
                if (!sl.Contains(s))
                {
                    sl.Add(s, s);
                }
            }

            round.nbSolutions = sl.Count;
            sl.Clear();
        }

        private void EvaluateRaccords(RatingRound round, CRound tround)
        {
            string s = tround.getword();
            if (s.Length > 4)
            {
                int count = CompteRaccord(s);
                round.scoreraccords = count;
                round.scoreAll += (float)count * RaccordsRatioMul / round.nbSolutions;
            }
        }

        private void EvaluateScrabble(RatingRound round, CRound tround)
        {
            round.scorescrabble = tround.bonus() / ScrabbleRatioDiv;
            round.scoreAll += (float)round.scorescrabble / round.nbSolutions;
        }

        private void EvaluateScrabbleSousTop(RatingRound round, CRound tround)
        {
            if (tround.bonus() > 0)
            {
                float diff = 100 - ((float)(round.results.sousTop)) / (float)(round.results.maxPoints) * 100;
                round.scoresoustop = diff / sousTopRatioDivScrabble;
                round.scoreAll += (float)round.scoresoustop / round.nbSolutions;
            }
        }

        private void EvaluateSousTop(RatingRound round, CRound tround)
        {
            if (tround.bonus() == 0)
            {
                float diff = 100 - ((float)(round.results.sousTop)) / (float)(round.results.maxPoints) * 100;
                round.scoresoustop = diff / sousTopRatioDiv;
                round.scoreAll += (float)round.scoresoustop / round.nbSolutions;
            }
        }

        private void EvaluateRack(RatingRound round, CRound tround)
        {
            CRack rack = round.rack;

            rack.AlignTiles();

            int vowel, consonant, joker;

            rack.GetTotalTiles(out vowel, out consonant, out joker);

            if (vowel == 3 || vowel == 4)
            {
                round.scorerack++;
            }

            int len = rack.ntiles();
            float pts = 0;
            int highletters = 0;
            for (int x = 0; x < len; x++)
            {
                int t = Tiles_points[rack.GetRackTile(x)];
                pts += t;

                if (t > 3) highletters++;
            }

            pts = pts / len;

            if (highletters > 2)
            {
                round.scorerack--;
            }

            if (pts <= 1)
            {
                round.scorerack++;
            }

            round.scoreAll += round.scorerack * RackRatioMult;

        }

        private bool ProcessTirages(IRackManager RackMan, int maxLetters, System.Collections.Generic.List<string> tirages)
        {
            bool ok = false;
            int x = 0;

            do
            {
                RackMan.ChooseTirage(GameConfig.Config.Joker, maxLetters);

                if (RackMan.IsCorrectRack(maxLetters, GameConfig.iTurn))
                {
                    ok = true;
                }
                else
                {
                    string s = RackMan.GetTirage(maxLetters);
                    x++;
                    RackMan.RejetTirage();
                }
            } while (!ok);


            RatingRound round = new RatingRound(gc.GridConfig);
            RackMan.copyTo(round.rack, round.bag);
            RackMan.copyTo(gc.Rack, gc.Bag);

            string t = round.rack.GetRackString();

            if (tirages.Contains(t))
            {
                return false;
            }


            tirages.Add(t);

            rounds.Add(round);

            gc.Results = new CResults(300);
            SearchResultsLocal results = new SearchResultsLocal(this.gc);
            results.SearchResults();
            round.results = gc.Results;

            return true;

        }


        private int CompteRaccord(string word)
        {
            ArrayList arlTirages = new ArrayList();
            gc.Dictionary.SearchRacc(word, arlTirages);
            return arlTirages.Count;
        }
    }
}
