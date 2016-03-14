using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace TopMachine.Topping.Dawg
{
    public class ShuffleBag : IDisposable
    {

        private Random random = new Random();
        private List<int> data;
        public CBoardMaker cm;
      //  private bool _chooseletterRandom = false;

        public int vowel = 0;
        public int consonant = 0;
        public int joker = 0;

        public ShuffleBag(CBoardMaker _cm)
        {
            data = new List<int>();
            cm = _cm;
            InitBag();
        }
        public void clear()
        {
            data.Clear();
            vowel = 0;
            consonant = 0;
            joker = 0; 
        }

        public void copy(ShuffleBag src)
        {
            data.Clear();
            foreach (var item in src.data)
            {
                data.Add(item);
            }
            
            cm = src.cm;
            vowel = src.vowel;
            consonant = src.consonant;
            joker = src.joker;
        }

        public void GetTotalTiles(out int _vowel, out int _consonant, out int _joker)
        {
            _vowel = vowel;
            _consonant = consonant;
            _joker = joker;
        }
        public int ntiles()
        {
            return data.Count;
        }

        public int isIn(int c)
        {
            return data.Where(x => x == c).Count();
            
        }
        public int replacetile(int t) 
        {
            if (t > 26) t = 0; 

            addLetter(t);
            return 1;
        }
        public int taketile(int t)
        {
           // if (data.Contains(t) && _chooseletterRandom) {
            if (data.Contains(t) ) {
            data.Remove(t);
            decreaseCptTypeLetter(t);
            //Debug.Write((char)(t + 64));
            //_chooseletterRandom = false;
            return 1;
            }

            return 0;
        }
        private void InitBag()
        {
            for (int letter = 0; letter < cm.NbLetters; letter++)
            {
                for (int j = 0; j < cm.Tiles_numbers[letter]; j++)
                {
                    data.Add(letter);
                    increaseCptTypeLetter(letter);
                }
            }
        }

        private void decreaseCptTypeLetter(int letter)
        {
            if (letter == DicoConstants.JOKER_TILE)
            {
                joker--;
            }
            else
            {
                vowel -= cm.Tiles_vowels[letter];
                consonant -= cm.Tiles_consonants[letter];
            }

        }
        private void increaseCptTypeLetter(int letter)
        {
            try
            {

            
            if (letter == DicoConstants.JOKER_TILE)
            {
                joker++;
            }
            else
            {
                vowel += cm.Tiles_vowels[letter];
                consonant += cm.Tiles_consonants[letter];
            }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void addLetter(int letter)
        {
            data.Add(letter);
            increaseCptTypeLetter(letter);
            Debug.Write((char)(letter + 64));

        }
        public int select_random()
        {
            return chooseRandom();
        }
        public int chooseRandom()
        {
            //if (!_chooseletterRandom)
            //{
                var pos = random.Next(data.Count - 1);

                int result = data[pos];

                // data.Remove(result);
                // decreaseCptTypeLetter(result);

                //Debug.Write((char)(result + 64) + "(*)");
                //_chooseletterRandom = true;
                return result;
            //}

            //throw new Exception("Magouille");
        }

        public bool empty()
        {
            return vowel + consonant + joker == 0;
        }
        public void Dispose()
        {
            cm = null;
        }

    }
}
