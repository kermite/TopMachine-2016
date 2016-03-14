using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

using TopMachine.Topping.Dawg;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Engine.GUI.Board;
using System.Diagnostics;


namespace TopMachine.Topping.Engine.GUI.RackManager
{
    public class AutoRackManager : IRackManager
    {
        CRack rack;
        ShuffleBag bag;
        GameCfg gc;

        public CRack Rack
        {
            get { return rack; }
        }

        public ShuffleBag Bag
        {
            get { return bag; }
        }

        #region Constructos
        public AutoRackManager(GameCfg gc)
        {
            this.gc = gc;
            init();
        }

        public AutoRackManager(GameCfg gc, CRack Track, ShuffleBag Tbag)
        {
            this.gc = gc;
            init();
            copy(Track, Tbag);

        }
        #endregion


        private void init()
        {
            rack = new CRack(gc.GridConfig.cm);
            bag = new ShuffleBag(gc.GridConfig.cm);
            //rack.init(gc.GridConfig.cm);
        }

        public void copy(CRack Track, ShuffleBag Tbag)
        {
            rack.copy(Track);
            bag.copy(Tbag);
            rack.AlignTiles();
        }

        public void addFormatTirage(string formatTirage, bool restart)
        {

            if (restart) RejetTirage();

            ShuffleBag Copybag = new ShuffleBag(gc.GridConfig.cm);
            Copybag.copy(bag);

            for (int i = 0; i < formatTirage.Length; i++)
            {
                switch (formatTirage[i])
                {
                    case '-':
                        RejetTirage();
                        break;
                    case '+':
                        if (!restart)
                        {
                            for (int j = 0; j < i; j++)
                            {   // uniquement dans le rack
                                RemoveLetter(formatTirage[j]);
                            }
                            bag.copy(Copybag);
                        }
                        break;
                    default:
                        AddLetter(formatTirage[i]);
                        break;

                }
            }

        }

        public void ChooseTirage(bool joker, int intMinLettres)
        {
            byte l=0;
            bool didJoker = false;
            Debug.WriteLine("");
            Debug.Write("Choose Tirage ");
            for (int i = 0; (!IsBagEmpty()) && (i < intMinLettres); i++)
            {
                if (joker)
                {
                    if (IsCorrectJoker())
                    {
                        didJoker = true;
                        AddLetter(0);
                        if (nbTilesRack() == intMinLettres) break;
                        continue;
                    }
                    else
                    {
                        if (this.nbTilesBag() != this.bag.joker) //this.bag.tbag.tiles[0])
                        {
                            l = 0;
                            while (l == 0)
                            {
                                l = ChooseRandomBag();
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    l = ChooseRandomBag();
                }

                AddLetter(l);
            }

            Debug.WriteLine(" End Tirage");
        }

        public void RemoveLetter(char c)
        {
            byte l;
            l = CharToByte(c);
            bag.replacetile(l);
            rack.remove(l);
        }

        public void AddLetter(char c)
        {
            byte l;
            l = CharToByte(c);
            AddLetter(l);
        }

        public void AddLetter(byte l)
        {
            bag.taketile(l);
            rack.add(l);

        }

        private byte CharToByte(char c)
        {
            return (byte) gc.GridConfig.GetCharFromAscii(c);
        }

        private bool IsJoker(char c)
        {
            if (c == '?') return true;
            else return false;
        }

        public bool RejetTirage()
        {// enleve les lettres du rack et remet les lettres dans le sac
            rack.AlignTiles();
            for (int x = 0; x < rack.ntiles(); x++)
            {
                byte c = (byte) rack.GetRackTile(x);
                bag.replacetile(c);
            }
            rack.empty();

            if (IsEndGame())
            {
                return true;
            }
            return false;
        }
        private int getMinCombinaison(int turn)
        {
            int min;

            int minlet = gc.Config.Grid == 0 ? 15 : 30;

            if (turn <= minlet)
            {

                min = 2; //2
            }
            else
            {
                min = 1;//1
            }

            return min;
        }
        private bool isLenghtOk(int len, int minrack)
        { // verifie si le nombre de lettre est ok
            if (len == minrack)
            {
                return true;
            }
            if (len < minrack && IsBagEmpty())
            {
                return true;
            }
            return false;
        }


        public  bool IsCorrectRack(ShuffleBag bag, CRack rack, int minRack, int turn)
        {
            // verifie si le rack correspond aux regles etablies 

            int vRack, cRack, jRack;
            int min;
            int cBag, vBag, jBag;
            int vTotal, cTotal, jTotal;

            min = getMinCombinaison(turn);

            rack.GetTotalTiles(out vRack, out cRack, out jRack);

            // Si le nombre de consonnes ou de voyelles ne suffit pas
            if (vRack < min || cRack < min)
            {
                    bag.GetTotalTiles(out vBag, out cBag, out jBag);
                    vTotal = vBag + vRack + jBag;
                    cTotal = cBag + cRack + jBag;

                    if ((vTotal == 1 && vRack == 1) || (cTotal == 1 && cRack == 1))
                    {
                        return true;
                    }

                    if (jRack > 0)
                    {
                        bag.GetTotalTiles(out vBag, out cBag, out jBag);
                        vTotal = vBag + vRack;
                        cTotal = cBag + cRack;

                        int grandTotal = (vTotal >= min ? 1 : 0) + (cTotal >= min ? 1 : 0);

                        if (grandTotal == 2)
                        {
                            return false;
                        }

                        return true;
                    }

                    return false;
            }

            return true;
        }


        public bool IsCorrectRack(int intMaxLettres, int turn)
        {
            return IsCorrectRack(bag, rack, intMaxLettres, turn);
        }


        public bool IsBagEmpty(ShuffleBag bag)
        {

            if (bag.ntiles() == 0) return true;
            return false;
        }

        public bool IsBagEmpty()
        {
            return IsBagEmpty(bag);
        }

        public bool IsCorrectJoker()
        {
            if (rack.isin(0) == 0 && bag.isIn(0) > 0)
                return true;
            return false;
        }


        public int nbTilesRack()
        {
            return rack.ntiles();
        }

        public string GetTirage(int intMaxLettres)
        {
            // formate le tirage 
            string ss = "";
            rack.AlignTiles();

            for (int x = 0; x < rack.ntiles(); x++)
            {

                char c = System.Convert.ToChar(rack.GetRack(x));
                if (c > 0)
                {
                    ss += new string(c, 1);
                }

            }
            return ss;

        }

        public int nbTilesBag()
        {
            return bag.ntiles();
        }


        public void CreateRack(char[] CurrentRackChars, int CurrentRackLength, CRack rack)
        {

            for (int x = 0; x < CurrentRackLength; x++)
            {
                int cc =  gc.GridConfig.GetCharFromAscii((int)CurrentRackChars[x]);
                rack.add((byte)cc);
            }
        }

        public byte ChooseRandomBag()
        {
            return (byte) bag.select_random();
        }


        //public  string GetBag()
        //{
        //    string s = "";
        //    for (int x = 0; x < bag.ntiles(); x++)
        //    {
        //        char c = (char)(bag.GetTileAtPosition(x) + 64);
        //        s += c;
        //    }
        //    Console.Write(s);
        //    return s;
        //}

        public void copyTo(CRack Track, ShuffleBag Tbag)
        {

            Tbag.copy(bag);
            Track.copy(rack);
            Track.AlignTiles();
        }

        public  bool IsEndGame()
        { // teste si on est arrive a la fin du jeu => plus de voyelle ou plus de consonne
            int vBag, vRack, cBag, cRack, jBag, jRack;
            int vTotal, cTotal, jTotal;

            gc.Bag.GetTotalTiles(out vBag, out cBag, out jBag);
            gc.Rack.GetTotalTiles(out vRack, out cRack, out jRack);


            vTotal = vBag + vRack;
            cTotal = cBag + cRack;
            jTotal = jBag + jRack;

            if (gc.Bag.ntiles() + gc.Rack.ntiles() == 1)
            {
                if (gc.Bag.isIn(25) > 0 || gc.Rack.isin(25) > 0)
                {
                    return true;
                }

                if (gc.Bag.isIn(0) > 0 || gc.Rack.isin(0) > 0)
                {
                    return true;
                }

            }

            if (vTotal == 0 || cTotal == 0)
            {
                if (jTotal == 0)
                {
                    return true;
                }
            }

            return false;

        }
    }
}
