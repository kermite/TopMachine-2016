using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using CMWA.Packager;
using CMWA.Packager.Custom;
using TopMachine.Desktop.Overall;
using System.Data.Common;
using System.Data.SqlClient;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using Core.DB.ExtensionPoints;


namespace TopMachine.Training.DAL.fdbo
{
    public class ListAccessor : IDisposable
    {
        string connectionKey;
        public string dbFile = "";
        public IObjectContainer entity = null;

        public string GetConnectionString(string ndb)
        {
            dbFile = PackageManager.Instance.Project.GetFileName(ndb);
            return dbFile;
        }

        public string GetConnectionString()
        {
            dbFile = PackageManager.Instance.Project.GetFileName(connectionKey);
            return dbFile;
        }

        public ListAccessor()
        {
            ;
        }

        public ListAccessor(string db)
        {
            connectionKey = db;
            GetConnectionString(connectionKey);
            ReinitializeEntity();
        }

        public static bool isExistList(string key) 
        {
            string pathFile = PackageManager.Instance.Project.GetFileName(key) ;
            return pathFile != null && System.IO.File.Exists(pathFile);
        }
        public void initializeEntity(string ndb)
        {
            GetConnectionString(ndb);

            if (entity != null)
            {
                entity.Dispose();
            }
            GetObjectContainer();

        }

        public void ReinitializeEntity(string db)
        {
            if (entity != null)
            {
                entity.Dispose();
            } 
            
            connectionKey = db;
            ReinitializeEntity();
            GetObjectContainer();
        }

        public void ReinitializeEntity()
        {
            if (entity != null)
            {
                entity.Dispose();
            }

            GetObjectContainer();

        }


        public string getIndexName(string f)
        {
              return "<" + f + ">" + "k__BackingField";
        }
        public IObjectContainer GetObjectContainer()
        {
            if (entity == null)
            {
                GetConnectionString();

                var cfg = Db4oEmbedded.NewConfiguration();
                cfg.Common.ObjectClass(typeof(Word)).ObjectField(getIndexName("Rack")).Indexed(true);
                cfg.Common.ObjectClass(typeof(Word)).ObjectField(getIndexName("Status")).Indexed(true);
                cfg.Common.ObjectClass(typeof(Word)).ObjectField(getIndexName("Lost")).Indexed(true);
                cfg.Common.ObjectClass(typeof(Word)).ObjectField(getIndexName("Succeeded")).Indexed(true);

                //cfg.(type).ObjectField(fieldName).Indexed(true);
                
                //cfg.Common.IndexClass(typeof(Word));
                //cfg.Common.IndexClass(typeof(Config));

                entity = Db4oEmbedded.OpenFile(cfg, dbFile);

                 //entity = Db4oEmbedded.OpenFile(dbFile);
            }

            return entity;
        }


        public void Save(Word w)
        {
            entity.Store(w);
        }

       
        public void AddWord(Word w)
        {
            entity.Store(w);
        }

        public Config GetConfig()
        {
            return (from Config c in entity select c).FirstOrDefault();
        }

        public void UpdateConfig(Config cfg)
        {
            entity.Store(cfg);
        }

        public void DeletePackage(Package p)
        {
            entity.Dispose();
            entity = null;

            Package parent = (Package)PackageManager.Instance.Project.GetPackageItem("Training4dbo\\Lists");

            string tgtfn = PackageManager.Instance.Project.GetFileName("Training4dbo\\Lists\\" + p.Key);
            System.IO.File.Delete(tgtfn);

            parent.Items.Remove(p);
            PackageManager.Instance.Project.SaveProject("Training4dbo");
        }


        public Config CreateConfig(ListConfig lc, GameConfig gc)
        {

            Package p = (Package)PackageManager.Instance.Project.GetPackageItem("Training4dbo\\Lists");

            string newKey = "Training4dbo\\Lists\\" + lc.Name;
            Package tgt = (Package)PackageManager.Instance.Project.GetPackageItem(newKey);

            if (tgt != null)
            {
                return null;
            }

            tgt = new Package();

            tgt.Description = lc.Name;
            tgt.Filename = Guid.NewGuid().ToString() + ".dbo";
            tgt.FileType = FileType.File;
            tgt.LocationType = LocationType.PersonalFiles; 
            tgt.Key = lc.Name;

            p.Items.Add(tgt);

            string tgtfn = PackageManager.Instance.Project.GetFileName(newKey);


            PackageManager.Instance.Project.SaveProject("Training4dbo");

            ReinitializeEntity(newKey);
            GetObjectContainer();

            Config tc = new Config();
            tc.XMLConfig = new TopMachine.Desktop.Overall.ObjectSerializer<ListConfig>().Serialize(lc);
            tc.XMLPlay = new TopMachine.Desktop.Overall.ObjectSerializer<GameConfig>().Serialize(gc);
            entity.Store(tc);

            return tc;


        }

        public void CreateSession(Session s)
        {

            entity.Store(s);
        }

        public List<WordStatistic> GetWordStatistics(string ndb)
        {
            string ConnectionString = GetConnectionString(ndb);
            return _GetWordStatistics(ConnectionString); 
        }

        public List<WordStatistic> GetWordStatistics() 
        {
            string ConnectionString = GetConnectionString();
            return _GetWordStatistics(ConnectionString); 
        
        }
        private List<WordStatistic> _GetWordStatistics(string ConnectionString)
        {
            
            List<WordStatistic> list = new List<WordStatistic>();

            for (int x = 0; x < 4; x++)
            {
                WordStatistic ws = new WordStatistic();
                ws.Status = x;
                ws.Lost = entity.AsQueryable<Word>().Where(p => p.Status == x && p.Lost > 0).Count();
                ws.Counter = entity.AsQueryable<Word>().Where(p => p.Status == x).Count();
                ws.Succeeded = entity.AsQueryable<Word>().Where(p => p.Status == x && p.Succeeded > 0).Count();

                ws.Total = ws.Counter;

                ws.Words = (from Word w in entity where w.Status == x select w.Rack).ToList();
                list.Add(ws);
            }
            return list;
    
        }

        public List<Word> LoadWords()
        {
            return (from Word w in entity select w).ToList();
        }

        public List<Word> GetWords(string word)
        {
            return (from Word w in entity where w.WordsXML.Contains(word) select w).ToList();
        }

        public List<Word> GetWordsByRack(string rack)
        {
            return (from Word w in entity where w.Rack == rack select w).ToList();
        }

        public Word GetWordByRack(string rack)
        {
            return (from Word w in entity where w.Rack == rack select w).FirstOrDefault(); 
        }

        public void DeleteWord(string word)
        {
            word = word.ToUpper();
            List<Word> words = (from Word w in entity where w.WordsXML.Contains(word) select w).ToList();

            foreach (Word w in words)
            {
                entity.Delete(w);
            }
        }
		public Word GetWord(string rack)
		{
			return (from Word w in entity where w.Rack == rack select w).FirstOrDefault();
		}
		public void DeleteWords()
        {
            List<Word> words = (from Word w in entity select w).ToList();

            foreach (Word w in words)
            {
                entity.Delete(w);
            }
        }

	
		#region IDisposable Members

		public void Dispose()
        {
            if (entity != null)
            {
                entity.Dispose();
            }
        }

        #endregion
    }
}
