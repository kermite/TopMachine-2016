using System;
using System.Collections.Generic;
using System.Text;

namespace TopMachine.Topping.Dawg
{

    public class tbag 
    {
        public int[] tiles = new int[DicoConstants.LETTERS];
        public int[] maptiles;
        public int[] maptilesF;
        public int ntiles;
        public int totaltiles;
    };

    public class CBag : IDisposable 
    {
        public Random rand;
        public tbag tbag;
        public CBoardMaker cm; 

        public void copy(CBag src)
        {
            for (int x = 0; x < DicoConstants.LETTERS; x++)
            {
                tbag.tiles[x] = src.tbag.tiles[x]; 
            }

            tbag.ntiles = src.tbag.ntiles;
            tbag.totaltiles= src.tbag.totaltiles;

            tbag.maptiles = new int[tbag.totaltiles];
            tbag.maptilesF = new int[tbag.totaltiles];


           for (int x = 0; x < tbag.totaltiles; x++)
            {
                tbag.maptiles[x] = src.tbag.maptiles[x];
                tbag.maptilesF[x] = src.tbag.maptilesF[x]; 
            }
        }

        public CBag(CBoardMaker cm)
        {
            rand = new Random();
            tbag = new tbag();
            this.cm = cm;
            init();
        }


        public void clear()
        {
            for (int x = 0; x < DicoConstants.LETTERS; x++)
            {
                tbag.tiles[x] = 0;
            }

            for (int i = 0; i < tbag.totaltiles; i++)
            {
                tbag.maptiles[i] = 0;
                tbag.maptilesF[i] = 0;
            }
        }

        private void init()
        {
            int i;
            tbag.ntiles = 0;

            int c = 0;

            for (i = 0; i < cm.NbLetters; i++)
            {
                tbag.ntiles += cm.Tiles_numbers[i];
            }

            tbag.totaltiles = tbag.ntiles;

            tbag.maptiles = new int[tbag.totaltiles];
            tbag.maptilesF = new int[tbag.totaltiles];

            for (i = 0; i < cm.NbLetters; i++)
            {
                tbag.tiles[i] = cm.Tiles_numbers[i];
                for (int x = 0; x < cm.Tiles_numbers[i]; x++)
                {
                    tbag.maptiles[c] = i;
                    tbag.maptilesF[c] = 1;
                    c++;
                }
            }

            //for (int x = 0; x < 500; x++)
            //{
            //    int c1 = rand.Next() % cm.TotalTiles;
            //    int c2 = rand.Next() % cm.TotalTiles;

            //    if (c1 < cm.TotalTiles && c2 < cm.TotalTiles)
            //    {
            //        int a = tbag.maptiles[c1];
            //        tbag.maptiles[c1] = tbag.maptiles[c2];
            //        tbag.maptiles[c2] = a;
            //    }
            //}
        }

        public void Unload()
        {
        }

        public int isIn(int c)
        {
            return tbag.tiles[c];
        }


        public int ntiles()
        {
            return tbag.ntiles;
        }

        private int GetTileAtPosition(int x)
        {
            int pos = 0;
            int found = 0;
            while (true)
            {
                if (tbag.maptilesF[pos] == 1)
                {
                    if (x == found) return tbag.maptiles[pos];
                    found++;
                }
                pos++;
            }
            return -1;
        }


        private int taketile(int t)
        {
            if (isIn(t) > 0)
            {
                tbag.tiles[t]--;
                tbag.ntiles--;

                for (int x = 0; x < tbag.totaltiles; x++)
                {
                    if (tbag.maptiles[x] == t && tbag.maptilesF[x] == 1)
                    {
                        tbag.maptilesF[x] = 0;
                        break;
                    }
                }
            }
            else
            {
                return 1;
            }
            return 0;
        }

        public void
        GetTotalTiles(out int vowel,out int consonant,out int joker)
        {
	        int i;
  	        int v = 0;
  	        int c = 0;
            int j = 0;
           // -1 : eviter de comptabilise le joker pour voyelle et consonne
	        for(i=0; i < cm.TotalTiles - 1; i++)
           {
	          if (tbag.maptilesF[i] == 1)
	          {
  		        if (tbag.maptiles[i] == DicoConstants.JOKER_TILE)
			        j ++;
		        else {
			        v += cm.Tiles_vowels[tbag.maptiles[i]];
			        c += cm.Tiles_consonants[tbag.maptiles[i]]; 
		        }
	          }
	        }
	        joker = j;
	        vowel = v;
	        consonant = c;

        }

        private int check()
        {
            int c = 0;
            for (int x = 0; x < tbag.totaltiles; x++)
            {
                if (tbag.maptilesF[x] == 1) c++;
            }
            return c;
        }

        private int replacetile(int t)
        {
            tbag.tiles[t]++;
            tbag.ntiles++;

            for (int x = 0; x < tbag.totaltiles; x++)
            {
                if (tbag.maptiles[x] == t && tbag.maptilesF[x] == 0)
                {
                    tbag.maptilesF[x] = 1;
                    break;
                }
            }

            return 1;

        }


        public int select_random()
        {
            int i, n;

            do
            {
                n = rand.Next(tbag.ntiles + 1); //  TILES_TOTAL;
            } while (n >= tbag.ntiles);


            int x = 0;
            int found = 0;
            while (true)
            {
                if (tbag.maptilesF[x] == 1)
                {
                    if (n == found)
                    {
                        return tbag.maptiles[x];
                    }
                    found++;
                }
                x++;
            }
        }

        public void Dispose()
        {
            tbag = null;
            cm = null;
        }
    }
}
