using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TopMachine.Training.DAL.fdbo;

namespace TopMachine.Training.DAL.fdbo
{
    public class WordsSnapshot
    {
        
        public List<string> WordsToPlay;
        public List<string> WordsMissed;
        public long totalWords;
        public float totalFound;
        public float totalLost;

        private static string GetFilePath(string PathList) 
        {
            ListAccessor la = new ListAccessor();
            var f = la.GetConnectionString(PathList);
            FileInfo fi = new FileInfo(f);
            FileInfo fi2 = new FileInfo(PathList);
            return fi.DirectoryName + "\\" + fi2.Name;


        
        }
        public static bool isExist(string PathList) 
        {

            return System.IO.File.Exists(GetFilePath(PathList));
        }

        public void load(string pathList) 
        {
            WordsSnapshot clone = new TopMachine.Desktop.Overall.ObjectSerializer<WordsSnapshot>().DeserializeFromFile(GetFilePath(pathList));

            totalWords = clone.totalWords;
            totalFound = clone.totalFound;
            totalLost = clone.totalLost; 

            WordsToPlay = clone.WordsToPlay;
            WordsMissed = clone.WordsMissed;

            
        }

        public bool save(string pathList) 
        {
            var result = new TopMachine.Desktop.Overall.ObjectSerializer<WordsSnapshot>().SerializeToFile(this, GetFilePath(pathList));
           return result != null;
        }

    }
}
