using System;
using System.Collections.Generic;
using System.Text;

namespace TopMachine.Topping.Dawg
{
public class CBoardMaker
{
	public int SizeH, SizeV; 
	public int SizeRH, SizeRV; 
	public int []Board_tile_multipliers;
	public int []Board_word_multipliers;
	public int []Tiles_vowels;
	public int []Tiles_consonants;
	public int []Tiles_numbers;
	public int []Tiles_points;
	public int []Tiles_ascii;
	public int TotalTiles;
	public int JokerTitle; 
	public int NbLetters;


    public void SetTiles(int size, int[] vowels, int[] consonants, int[] numbers, 
            int[]points, int []ascii)
	{
		NbLetters =size;
		JokerTitle = size-1;
		TotalTiles = 0;
		Tiles_vowels = new int[size];
		Tiles_consonants = new int[size];
		Tiles_numbers = new int[size];
		Tiles_points = new int[size];
		Tiles_ascii = new int[size];
		for (int x = 0; x < size; x++)
		{
			Tiles_vowels[x] = vowels[x]; 
			Tiles_consonants[x] = consonants[x];
			Tiles_numbers[x] = numbers[x];
			Tiles_points[x] = points[x];
			Tiles_ascii[x] = ascii[x];
			TotalTiles += Tiles_numbers[x]; 
		}
	}

    public void SetBoard(int h, int v, int[] tm, int[] wm)
	{
		SizeH = h; 
		SizeV = v;
		SizeRH = SizeH + 2; 
		SizeRV = SizeV + 2;

		Board_tile_multipliers = new int[SizeRH*SizeRV];
		Board_word_multipliers = new int[SizeRH*SizeRV];
		for (int x = 0; x < SizeRH*SizeRV; x++)
		{
			Board_tile_multipliers[x] = tm[x];  
			Board_word_multipliers[x] = wm[x];
		}

	}

    public char GetTileAsciiChar(char x)
		{
			return (char) Tiles_ascii[x];
		}

        public int GetTileAscii(int x)
		{
			return Tiles_ascii[x];
		}

        public int  GetTileCode(char x)
        {
            char xx = char.ToUpper(x);

            for (int y = 0; y < NbLetters; y++)
            {
                if (Tiles_ascii[y] == xx)
                {
                    return y; 
                }
            }

            return 0;
        }


        public int GetTilePoints(int x)
		{
			return Tiles_points[x];
		}

        public int GetWordMultiplier(int x, int y)
		{
			return Board_word_multipliers[y*SizeRH+x];
		}

		public int GetTileMultiplier(int x, int y)
		{
			return Board_tile_multipliers[y*SizeRH+x];
		}


  }
}
