using System;
using TopMachine.Topping.Dawg;
using TopMachine.Desktop.Overall;
using System.Diagnostics; 

namespace TopMachine.Topping.Common.Structures
{

    public class RoundPlayed {

        public string word;
        public string direction;
        public string Point;
        public string time;

        public RoundPlayed(string strword, string strdirection, string strPoint, string strtime)
        {
            word = strword;
            direction = strdirection;
            Point = strPoint;
            time = strtime;
        }

        public RoundPlayed(CRound round,float ftime) 
        {
            
            word = round.getwordwithjoker();
            Point = round.points().ToString();
            direction = convertCoordonnee(round.row(), round.column(), round.dir());
            time = ftime.ToString("0.00");
        }    

        private string convertCoordonnee(int row,int col,int direction)
        {
            string txt = "";
            char c = (char)col;
            char r = (char)row;

            if (direction == 1)
            {
                txt = ((char)(r + 64)).ToString() + ((int)c).ToString();
            }

            if (direction == 0)
            {
                txt = ((int)c).ToString() + ((char)(r + 64)).ToString();
            }
            return txt;
        }
    
    }
    
    public class PlayerRoundDisplay 
    {
        public string roundNumber;
        public RoundPlayed roundPlayer,  roundPc;
        public int negatif=0;
        public int totPlayer = 0;
        public int totGame = 0; 

        public PlayerRoundDisplay(string word, string direction, string point, string time, PlayedRound pr)
        {

            roundNumber = pr.RoundNumber.ToString();

            if (word.Length == 0)
            {
                roundPlayer = null;

            }
            else {

               roundPlayer = new RoundPlayed(word,direction,point,time);
            }

            roundPc = new RoundPlayed(pr.PlacedRound, pr.Time);

            if (pr.Round != null)
                negatif = Convert.ToInt32(roundPlayer.Point) - Convert.ToInt32(roundPc.Point);
        }

        public PlayerRoundDisplay(PlayedRound pr)
        {

            roundNumber = pr.RoundNumber.ToString();

            if (pr.Round == null)
                roundPlayer = null;
            else
            {
                roundPlayer = new RoundPlayed(pr.Round, pr.Time);
            }

            roundPc = new RoundPlayed(pr.PlacedRound, pr.Time);
            
           
            if (pr.Round != null)
                negatif = Convert.ToInt32(roundPlayer.Point) - Convert.ToInt32(roundPc.Point);
        }
    }

    public class PlayedRound : IDisposable 
	{
		public int RoundNumber; 
		public float Time;
		public CRound PlacedRound, Round; 
		public string Rack; 
		public ShuffleBag Bag;
		public CRack CRack;
        public int totGame = 0;
        Guid MemoryCheckId;
        ///// <summary>
        ///// Returns the call that occurred just before the "GetCallingMethod".
        ///// </summary>
        //public static string GetCallingMethod()
        //{
        //    return GetCallingMethod("GetCallingMethod");
        //}

        ///// <summary>
        ///// Returns the call that occurred just before the the method specified.
        ///// </summary>
        ///// <param name="MethodAfter">The named method to see what happened just before it was called. (case sensitive)</param>
        ///// <returns>The method name.</returns>
        //public static string GetCallingMethod(string MethodAfter)
        //{
        //    string str = "";
        //    try
        //    {
        //        StackTrace st = new StackTrace();
        //        StackFrame[] frames = st.GetFrames();
        //        for (int i = 0; i < st.FrameCount - 1; i++)
        //        {
        //            if (frames[i].GetMethod().Name.Equals(MethodAfter))
        //            {
        //                if (!frames[i + 1].GetMethod().Name.Equals(MethodAfter)) // ignores overloaded methods.
        //                {
        //                    str = frames[i + 1].GetMethod().ReflectedType.FullName + "." + frames[i + 1].GetMethod().Name;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception) { ; }
        //    return str;
        //}
        public PlayedRound(GameCfg gc, CRack rack, int tp, int tg) 
        {
            // name of method call this function  : new StackFrame(1).GetMethod().Name
            MemoryCheckId = MemoryCheck.Register(this, "PlayerRound CREATE ");

            totGame = tg;
            Bag = new ShuffleBag(gc.GridConfig.cm);
            Bag.copy(gc.Bag);

            CRack = new CRack(gc.GridConfig.cm);
            CRack.copy(rack);

            Time = gc.CurrentPlay;
            PlacedRound = new CRound(gc.GridConfig.cm);
            PlacedRound.create();
            if (gc.CurrentGameRound != null && gc.CurrentGameRound.Round != null)
            {
                PlacedRound.copy(gc.CurrentGameRound.Round);
            }
            PlacedRound.setpoints(tg);

            Round = new CRound(gc.GridConfig.cm);
            Round.create();
            if (gc.CurrentRound != null)
            {
                Round.copy(gc.CurrentRound.Round);
            }

            Round.setpoints(tp);
            gc.Results = new CResults(10000);

            string oldss = "";
            if (gc.OldRack != null)
            {
                for (int x = 0; x < gc.OldRack.ntiles(); x++)
                {
                    char c = System.Convert.ToChar(gc.OldRack.GetRack(x));
                    if (c > 0) oldss += new string(c, 1);
                }
            }

            string ss = "";
            if (oldss.Length == 0)
                ss = "-";
            else
                ss = oldss + "+";

            for (int x = 0; x < rack.ntiles(); x++)
            {
                char c = System.Convert.ToChar(rack.GetRack(x));

                int cc = rack.GetRackTile(x);
                if (gc.OldRack != null && gc.OldRack.isin(cc) != 0)
                {
                    gc.OldRack.remove(cc);
                }
                else
                {
                    if (c > 0)
                    {
                        ss += new string(c, 1);
                    }
                }

            }

            Rack = ss;
            RoundNumber = gc.iTurn;
        
        }

        public void Dispose()
        {
            if (PlacedRound != null)  PlacedRound.Dispose();
            if(Round != null ) Round.Dispose();
            if (Bag != null) Bag.Dispose();
            if (CRack != null) CRack.Dispose();

            MemoryCheck.Unregister(MemoryCheckId);
        }
    }
}
