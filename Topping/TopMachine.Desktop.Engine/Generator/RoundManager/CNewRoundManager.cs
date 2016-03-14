
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
    public class CNewRoundManager : CRoundManager
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


        public CNewRoundManager(GameControllers gctls, GameCfg cfg)
            : base(gctls, cfg)
        {
            if (cfg.Config.Name.Contains("NEW"))
            {
                dico = TopMachine.Topping.Dawg.DicoUtils.Instance.GetDico("FRNEW");
                maxTurn = 100;
            }
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
              //  NLog.LogManager.GetLogger("wcf").ErrorException("CExplosiveRoundManager : NewTirage", ee);
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


                int tot = rnd.Next(80) + 20;

                System.Collections.Generic.List<string> tirages = new System.Collections.Generic.List<string>();
                for (int y = 0; y < maxTurn; y++)
                {
                    tirages = new System.Collections.Generic.List<string>();
                    rounds = new ArrayList();
                    nbTirages++;
                    RackMan.copy(OldRack, OldBag);
                    ProcessTirages(RackMan, GameConfig.Config.MaxLetters, tirages);
                    bool ok = EvaluateTiragesODS5(RackMan);
                    if (gc.iTurn == 1) break;
                    if (ok)
                        break;
                }

                selected = (RatingRound)rounds[0];
            }
            catch (Exception ee)
            {
                if (selected == null) return false;
            }


            GameConfig.OldRack.empty();
            selected.Valid = true;
            selected.nbTirages = nbTirages;

            while (selected.results.Results.list.Count > 1)
            {
                selected.results.Results.list.RemoveAt(1);
            }

            CRound tround = (CRound)selected.results.Results.list.GetByIndex(0);

            int playedTot = selected.rack.ntiles();
            int[] played = new int[playedTot];

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

        private bool EvaluateTiragesODS5(IRackManager RackMan)
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
                        return true;
                    }
                }
            }

            return false;
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

        public void Dispose()
        {
            dico.Dispose();
            base.Dispose(); 

        }
    }
}
