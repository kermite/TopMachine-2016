using TopMachine.Topping.Dawg;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopMachine.Topping.Dawg
{
    class track
    {
        public int[] _ttiles;
        public int[] _tiles;

        public int[] ttiles
        {
            get { return _ttiles; }
        }

        public int[] tiles
        {
            get { return _tiles; }
        }

        public int ntiles;

        public track()
        {
            _ttiles = new int[DicoConstants.LETTERS];
            _tiles = new int[DicoConstants.LETTERS];
        }
    };

    public class CRack : IDisposable 
    {
        CBoardMaker cm;
        track track;


        public CRack()
        {
            throw new Exception();
        }

        public CRack(CBoardMaker cm)
        {
            this.cm = cm;
            init();
            track = new track();
        }

        public void copy(CRack source)
        {
            track._ttiles = new int[DicoConstants.LETTERS];
            track._tiles = new int[DicoConstants.LETTERS];

            for (int x = 0; x < DicoConstants.LETTERS; x++)
            {
                track._ttiles[x] = source.track._ttiles[x];
                track._tiles[x] = source.track._tiles[x];
            }

            track.ntiles = source.track.ntiles;
        }


        public void init()
        {
            track = new track();
        }


        public void Unload()
        {
        }

        public int empty()
        {
            track = new track();
            return track.ntiles;
        }

        public int ntiles()
        {
            return track.ntiles;
        }


        public int isin(int tile)
        {
            return track._tiles[tile];
        }


        public void remove(int tile)
        {
            track._tiles[tile]--;
            track.ntiles--;
        }


        public void AlignTiles()
        {
            int c = 0;

            track._ttiles = new int[DicoConstants.LETTERS];

            for (int x = 0; x < cm.NbLetters; x++)
            {
                for (int y = 0; y < track._tiles[x]; y++)
                {
                    track._ttiles[c] = x;
                    c++;
                }
            }
        }

        public void
        UnalignTiles()
        {
            track._tiles = new int[DicoConstants.LETTERS];

            for (int x = 0; x < track.ntiles; x++)
            {
                track._tiles[track._ttiles[x]]++;
            }
        }

        public void add(int tile)
        {
            track._tiles[tile]++;
            track.ntiles++;
        }

        public int GetRackTile(int n)
        {
            return track._ttiles[n];
        }

        public char GetRack(int n)
        {
            return (char) cm.Tiles_ascii[track._ttiles[n]];
        }

        public string GetRackString()
        {
            string s = "";

            for (int i = 0; i < ntiles(); i++)
            {
                s += GetRack(i);
            }
            return s;
        }

        public int CheckRack(int n, int nbl)
        {
            int v, c, j;
            GetTotalTiles(out v, out c, out j);
            return ((v >= n) && (c >= n)) ? 1 : 0;
        }

        public void GetTotalTiles(out int vowel, out int consonant, out int joker)
        {
            int i;
            int v = 0;
            int c = 0;

            for (i = 1; i < cm.NbLetters; i++)
            {
                if (track._tiles[i] > 0)
                {
                    v += cm.Tiles_vowels[i] * track._tiles[i];
                    c += cm.Tiles_consonants[i] * track._tiles[i];
                }
            }

            joker = track._tiles[0];
            vowel = v;
            consonant = c;
        }


        public void Dispose()
        {
            track = null; 
        }
    }
}
