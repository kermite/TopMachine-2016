using System;
using System.Collections;
using System.Text;
using System.Data;
using System.IO;
using CMWA.Packager.Tools.Bytes;
using CMWA.Packager;


namespace TopMachine.Topping.Dawg
{

	public class SortRallonges : IComparer
	{
		int IComparer.Compare(Object x, Object y)
		{
			WordContainer b1 = (WordContainer)x;
			WordContainer b2 = (WordContainer)y;

			int c = string.Compare(b1.BaseWord, b2.BaseWord);
			if (c != 0) return c;

			if ((b1.Prefix.Length == b2.Prefix.Length) && b1.Suffix.Length == b2.Suffix.Length)
			{
				return string.Compare(b1.Word, b2.Word);
			}

			if (b1.Prefix.Length == 0 && b2.Prefix.Length == 0)
			{
				return b1.Suffix.Length - b2.Suffix.Length;
			}

			if (b1.Prefix.Length == 0 || b2.Prefix.Length == 0)
			{
				return b1.Prefix.Length == 0 ? -1 : 1;
			}

			if (b1.Suffix.Length == 0 && b2.Suffix.Length == 0)
			{
				return b1.Prefix.Length - b2.Prefix.Length;
			}

			if (b1.Suffix.Length == 0 || b2.Suffix.Length == 0)
			{
				return b1.Suffix.Length == 0 ? -1 : 1;
			}

			if (b1.Word.Length != b2.Word.Length)
			{
				return b1.Word.Length - b2.Word.Length;
			}

			int tot1 = b1.Word.Length - b1.BaseWord.Length;
			int tot2 = b2.Word.Length - b2.BaseWord.Length;

			int c1 = tot1 * 10000 + b1.Prefix.Length * 100 + b1.Suffix.Length;
			int c2 = tot2 * 10000 + b2.Prefix.Length * 100 + b2.Suffix.Length;

			if (c1 != c2)
			{
				return c1 - c2;
			}

			return string.Compare(b1.Word, b2.Word);
		}
	}


	public class Sort71 : IComparer
	{
		int IComparer.Compare(Object x, Object y)
		{
			WordContainer xx = (WordContainer)x;
			WordContainer yy = (WordContainer)y;

			if (xx.Additional.Length == yy.Additional.Length)
			{
				return xx.Sort.CompareTo(yy.Sort);
			}

			return xx.Additional.Length.CompareTo(yy.Additional.Length);
		}
	}

	public class SortWords : IComparer
	{
		int IComparer.Compare(Object x, Object y)
		{
			WordContainer xx = (WordContainer)x;
			WordContainer yy = (WordContainer)y;

			if (xx.Word.Length == yy.Word.Length)
			{
				return xx.Word.CompareTo(yy.Word);
			}

			return -xx.Word.Length.CompareTo(yy.Word.Length);
		}
	}

	public class DicoUtils
	{


		#region IDisposable Members

		public void Dispose()
		{
			if (dico != null)
			{
				dico.Dispose();
				dico = null;
			}
		}

		#endregion


		dawgDictionary dico;
		string language = "";


		public static DicoUtils _instance = null;

		public static DicoUtils Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DicoUtils("FR");
				}
				return _instance;
			}
		}
		public static dawgDictionary GetDicoPath(string path)
		{
			dawgDictionary dico = null;
			cpt cptTools = new cpt();
			var b = cptTools.ReadFile(path, "", 0);

			dico = new dawgDictionary();
			dico.Load(b);

			return dico;
		}

		public dawgDictionary getDico()
		{
			return dico;
		}

		public string getLanguage()
		{
			return language;
		}

		public DicoUtils(string language)
		{
			if (this.language != language)
			{
				this.language = language;
				string path;


				string baseFile = DicoTools.GetDicoName(language, 0);

				byte[] b = PackageManager.Instance.Project.GetBytes("TopMachineDico\\Data\\" + baseFile);

				dico = new dawgDictionary();
				dico.Load(b);
			}
		}

		public dawgDictionary GetDico(string language)
		{
			dawgDictionary dico = null;
			if (this.language != language)
			{
				this.language = language;
				string path;


				string baseFile = DicoTools.GetDicoName(language, 0);

				byte[] b = PackageManager.Instance.Project.GetBytes("TopMachineDico\\Data\\" + baseFile);

				dico = new dawgDictionary();
				dico.Load(b);
			}

			return dico;
		}

		public static void SetLanguage(string ln)
		{
			_instance = new DicoUtils(ln);
		}

		public static DicoUtils GetDicoUtils(string ln)
		{
			return new DicoUtils(ln);
		}

		public ArrayList DoVoisins(string text)
		{
			ArrayList wcs = new ArrayList();

			char[] c = text.ToCharArray();
			int len = c.Length;

			DoVoisinsType1(text, c, len, wcs);
			DoVoisinsType2(text, c, len, wcs);
			DoVoisinsType3(text, c, len, wcs);
			DoVoisinsType4(text, c, len, wcs);
			DoVoisinsType5(text, c, len, wcs);
			return wcs;
		}

		private void DoVoisinsType5(string text, char[] c, int len, ArrayList wcs)
		{
			SortedList sl = new SortedList();
			for (int x = 0; x < len; x++)
			{
				char[] n = new char[len + 1];

				int pos = 0;

				for (int z = 0; z < len; z++)
				{
					if (z != x)
					{
						n[pos++] = c[z];
					}
				}

				ArrayList arl = new ArrayList();
				dico.SearchMask(new string(n), arl);
				foreach (string s in arl)
				{
					string neww = s.Substring(0, s.IndexOf((char)0));
					if (!sl.Contains(neww) && neww != text)
					{
						sl.Add(neww, neww);
						WordContainer wc = new WordContainer();
						wc.Word = neww;
						wc.BaseWord = text;
						wc.Type = 5;
						wc.X = x;
						wc.Y = 0;
						AddRaccords(wc, neww);
						wcs.Add(wc);
					}
				}
			}
		}

		private void DoVoisinsType4(string text, char[] c, int len, ArrayList wcs)
		{
			SortedList sl = new SortedList();
			for (int x = 0; x <= len; x++)
			{
				char[] n = new char[len + 1];

				int pos = 0;

				for (int z = 0; z < len; z++)
				{
					if (z == x)
					{
						n[pos++] = '?';
					}

					n[pos++] = c[z];
				}

				if (x == len)
				{
					n[pos] = '?';
				}

				ArrayList arl = new ArrayList();
				dico.SearchMask(new string(n), arl);
				foreach (string s in arl)
				{
					string neww = s.Substring(0, s.IndexOf((char)0));
					if (!sl.Contains(neww) && neww != text)
					{
						sl.Add(neww, neww);
						WordContainer wc = new WordContainer();
						wc.Word = neww;
						wc.BaseWord = text;
						wc.Type = 4;
						wc.X = x;
						wc.Y = 0;
						AddRaccords(wc, neww);
						wcs.Add(wc);
					}
				}
			}
		}

		private void DoVoisinsType3(string text, char[] c, int len, ArrayList wcs)
		{
			SortedList sl = new SortedList();
			for (int x = 0; x < len; x++)
			{
				char[] n = text.ToCharArray();
				n[x] = '?';

				ArrayList arl = new ArrayList();
				dico.SearchMask(new string(n), arl);
				foreach (string s in arl)
				{
					string neww = s.Substring(0, s.IndexOf((char)0));
					if (!sl.Contains(neww) && neww != text)
					{
						sl.Add(neww, neww);
						WordContainer wc = new WordContainer();
						wc.Word = neww;
						wc.BaseWord = text;
						wc.Type = 3;
						wc.X = x;
						wc.Y = 0;
						AddRaccords(wc, neww);
						wcs.Add(wc);
					}
				}
			}
		}

		private void DoVoisinsType2(string text, char[] c, int len, ArrayList wcs)
		{
			// Déplace Une lettre dans le mot 
			char[] n = new char[len];
			for (int x = 0; x < len - 1; x++)
			{
				for (int y = x + 1; y < len; y++)
				{
					for (int z = 0; z < len; z++)
					{
						n[z] = c[z];
					}

					char cc = n[x]; n[x] = n[y]; n[y] = cc;

					string nn = new string(n);
					if (nn != text)
					{
						if (dico.SearchWord(nn) == 1)
						{
							WordContainer wc = new WordContainer();
							wc.Word = nn;
							wc.BaseWord = text;
							wc.Type = 2;
							wc.X = x;
							wc.Y = y;
							AddRaccords(wc, nn);
							wcs.Add(wc);
						}
					}
				}
			}

		}

		private void DoVoisinsType1(string text, char[] c, int len, ArrayList wcs)
		{
			// Déplace Une lettre dans le mot 
			for (int x = 0; x < len; x++)
			{
				for (int y = 0; y < len; y++)
				{
					if (x == y) continue;

					char[] n = new char[len];
					int pos = 0;
					int cpos = 0;

					for (int z = 0; z < len; z++)
					{
						if (pos == x) { pos++; continue; }

						if (z == y)
						{
							n[cpos] = c[x];
							cpos++;
						}

						n[cpos] = c[pos];
						cpos++;
						pos++;

					}

					if (n[len - 1] == '0') n[len - 1] = c[x];

					string nn = new string(n);
					if (nn != text)
					{
						if (dico.SearchWord(nn) == 1)
						{
							WordContainer wc = new WordContainer();
							wc.Word = nn;
							wc.BaseWord = text;
							wc.Type = 1;
							wc.X = x;
							wc.Y = y;
							AddRaccords(wc, nn);
							wcs.Add(wc);
						}
					}
				}
			}

		}

		public ArrayList DoRallonges(string text)
		{
			ArrayList wcs = new ArrayList();

			for (int x = 0; x < 21; x++)
			{
				string prefix = new string('?', x);
				for (int y = 0; y < 21; y++)
				{
					string suffix = new string('?', y);
					string search = prefix + text + suffix;

					if (search.Length < 16)
					{
						ArrayList arl = new ArrayList();
						dico.SearchMask(search, arl);
						foreach (string mot in arl)
						{
							WordContainer wc = new WordContainer();
							int pos = mot.IndexOf(text);
							wc.Prefix = mot.Substring(0, pos);
							wc.Suffix = mot.Substring(pos + text.Length);
							wc.BaseWord = text;
							wc.Word = mot;
							AddRaccords(wc, text);
							wcs.Add(wc);
						}
					}
				}

			}

			SortRallonges st = new SortRallonges();
			wcs.Sort(st);
			return wcs;

		}

		public void AddRaccords(WordContainer wc, string mot)
		{
			ArrayList arl = new ArrayList();

			arl = SearchMask("?" + mot);
			foreach (WordContainer s in arl)
			{
				wc.Before += s.Word.Substring(0, 1);
			}

			arl = new ArrayList();
			arl = SearchMask(mot + "?");
			foreach (WordContainer s in arl)
			{
				wc.After += s.Word.Substring(s.Word.Length - 1, 1);
			}
		}

		public ArrayList DoWordsRack(string rack)
		{
			System.Collections.ArrayList arl = new System.Collections.ArrayList();

			if (rack.IndexOf('?') != -1)
			{
				dico.Search7(rack, arl, true);
			}
			else
			{
				dico.Search7(rack, arl, false);
			}
			return SortList(arl);
		}

		private ArrayList SortList(ArrayList arl)
		{
			ArrayList wcs = new ArrayList();
			SortedList sl = new SortedList();
			foreach (string s in arl)
			{
				string t = s.ToUpper();
				if (sl.Contains(t)) continue;

				sl.Add(t, t);

				t = s;
				t = s.Substring(0, s.IndexOf((char)0));
				WordContainer wc = new WordContainer();
				wc.Sort = t;
				string[] ss = t.Split('.');
				wc.Additional = ss[0];
				wc.Word = ss[1];
				AddRaccords(wc, ss[1]);
				wcs.Add(wc);
			}

			Sort71 st = new Sort71();
			wcs.Sort(st);
			return wcs;
		}

		public ArrayList Do7Plus1(string text)
		{
			System.Collections.ArrayList arl = new System.Collections.ArrayList();

			if (text.IndexOf('?') != -1)
			{
				dico.Search7pl1(text, arl, true);
			}
			else
			{
				dico.Search7pl1(text, arl, false);
			}

			return SortList(arl);
		}

		public ArrayList SearchWords(string text)
		{
			System.Collections.ArrayList arl = new System.Collections.ArrayList();

			if (text.IndexOf('?') != -1)
			{
				dico.SearchAnyWord(text, arl, true);
			}
			else
			{
				dico.SearchAnyWord(text, arl, false);
			}

			ArrayList wcs = new ArrayList();
			SortedList sl = new SortedList();
			foreach (string s in arl)
			{
				string t = s.ToUpper();
				if (sl.Contains(t)) continue;

				sl.Add(t, t);

				t = s.Substring(0, s.IndexOf((char)0));
				WordContainer wc = new WordContainer();
				wc.Sort = t;
				wc.Word = t;
				AddRaccords(wc, t);
				wcs.Add(wc);
			}

			SortWords st = new SortWords();
			wcs.Sort(st);
			return wcs;
		}

		public ArrayList SearchWordsSmaller(string text)
		{
			System.Collections.ArrayList arl = new System.Collections.ArrayList();

			if (text.IndexOf('?') != -1)
			{
				dico.SearchAnyWordSmaller(text, arl, true);
			}
			else
			{
				dico.SearchAnyWordSmaller(text, arl, false);
			}

			ArrayList wcs = new ArrayList();
			SortedList sl = new SortedList();
			foreach (string s in arl)
			{
				string t = s.ToUpper();
				if (sl.Contains(t)) continue;

				sl.Add(t, t);

				//t = s.Substring(0, s.IndexOf((char)0));
				WordContainer wc = new WordContainer();
				wc.Sort = t;
				wc.Word = t;
				AddRaccords(wc, t);
				wcs.Add(wc);
			}

			SortWords st = new SortWords();
			wcs.Sort(st);
			return wcs;
		}


		public ArrayList SearchMask(string text)
		{
			System.Collections.ArrayList arl = new System.Collections.ArrayList();

			dico.SearchMask(text, arl);

			ArrayList wcs = new ArrayList();
			SortedList sl = new SortedList();
			foreach (string s in arl)
			{
				string t = s.ToUpper();
				if (sl.Contains(t)) continue;

				sl.Add(t, t);

				t = s;
				t = s.Substring(0, s.IndexOf((char)0));
				WordContainer wc = new WordContainer();
				wc.Sort = t;
				wc.Word = t;
				wcs.Add(wc);
				AddRaccords(wc, t);
			}

			SortWords st = new SortWords();
			wcs.Sort(st);
			return wcs;
		}

		public ArrayList SearchWordsPlus(string text)
		{
			System.Collections.ArrayList arl = new System.Collections.ArrayList();

			dico.SearchAnyWordLonger(text, arl);

			ArrayList wcs = new ArrayList();
			SortedList sl = new SortedList();
			foreach (string s in arl)
			{
				string additional = "";
				foreach (char c in s)
				{
					if (char.IsLower(c))
					{
						additional += char.ToUpper(c);
					}
				}

				char[] cc = additional.ToCharArray();
				int l = cc.Length;
				for (int x = 0; x < l - 1; x++)
				{
					for (int y = x + 1; y < l; y++)
					{
						if (cc[x] > cc[y])
						{
							char tmpc = cc[x]; ;
							cc[x] = cc[y];
							cc[y] = tmpc;
						}
					}
				}

				additional = "";
				foreach (char c in cc)
				{
					additional += c;
				}

				string t = additional + "." + s.ToUpper();
				if (sl.Contains(t)) continue;
				sl.Add(t, t);

				t = s;
				t = s.Substring(0, s.IndexOf((char)0));
				WordContainer wc = new WordContainer();
				wc.Additional = additional;
				wc.Sort = t;
				wc.Word = t;
				AddRaccords(wc, t);
				wcs.Add(wc);
			}

			Sort71 st = new Sort71();
			wcs.Sort(st);
			return wcs;
		}

	}
}
