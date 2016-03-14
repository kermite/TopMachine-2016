using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CMWA.Packager.Tools.Bytes;
using System.IO;
using CMWA.Packager.Tools;
using TopMachine.Topping.DAL;
using System.Data.Objects;

namespace TopMachine.Desktop.Dico
{
    public class DicoLoader
    {
        DicoAccessor da = null;
        List<DicoPair> list = new List<DicoPair>();
        string lan = "FR";
        string visName = "";


        public void LoadDico()
        {
        }

        public List<DicoPair> InitWordList(DicoReaderConfig cfg)
        {

            da = new DicoAccessor();
            List<DicoPair> dp = da.GetDicoIds();



            var q = dp.Where(p => p.word.Length >= cfg.LenMin && p.word.Length <= cfg.LenMax);
            q = q.Where(p => p.word.CompareTo(cfg.Start) >= 0);
            
            if (cfg.End != null && cfg.End.Length > 0)
            {
                q = q.Where(p => p.word.CompareTo(cfg.End) <= 0);
            }

            if (cfg.OnlyNew)
            {
                q = q.Where(p => p.isNew == true);
            }

            q.OrderBy(p => p.word);

            list = q.ToList();
            return list;
        }


        public List<TopMachine.Topping.DAL.Dico> LoadWordList(DicoReaderConfig cfg)
        {
            return da.GetDicos(list.Skip(cfg.CurrentPage * cfg.Max).Take(cfg.Max).ToList());
        }


        public void SaveVisibility()
        {
            //  new ByteAccessor().UpdateDirectFile(visName, visibility , 0, LocationType.PersonalFiles);
        }

        public void SaveVisibilityPartial(List<TopMachine.Topping.DAL.Dico> list)
        {
            //da.Save();

        }

    }
}
