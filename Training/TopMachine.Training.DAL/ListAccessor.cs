using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Data.Objects;
using CMWA.Packager;
using CMWA.Packager.Custom;
using System.Data.SQLite;

namespace TopMachine.Training.DAL
{
    public class ListAccessor : IDisposable
    {
        TrainingEntities entity = null;
        string connectionKey;

        public string GetConnectionString(string ndb)
        {
            string conn = "data source={0}";
            string str = string.Format(conn, ndb);

            return str;
        }

        public string GetConnectionString()
        {
            string conn = "data source={0}";
            string ndb = PackageManager.Instance.Project.GetFileName(connectionKey);
            string str = string.Format(conn, ndb);

            return str;
        }

        public ListAccessor()
        {

        }

        public ListAccessor(string db)
        {
            connectionKey = db;
            ReinitializeEntity();
        }

        public void initializeEntity(string ndb)
        {
       
            if (entity != null)
            {
                entity.Dispose();
            }

            string conn = "metadata=res://*/TrainingLists.csdl|res://*/TrainingLists.ssdl|res://*/TrainingLists.msl;provider=System.Data.SQLite;provider connection string=\"data source={0}\";";
            string str = string.Format(conn, ndb);
            entity = new TrainingEntities(str);
        }

        public void ReinitializeEntity(string db)
        {
            connectionKey = db;
            ReinitializeEntity();
        }

        public void ReinitializeEntity()
        {
            if (entity != null)
            {
                entity.Dispose();
            }

            string conn = "metadata=res://*/TrainingLists.csdl|res://*/TrainingLists.ssdl|res://*/TrainingLists.msl;provider=System.Data.SQLite;provider connection string=\"data source={0}\";";
            string ndb = PackageManager.Instance.Project.GetFileName(connectionKey);
            string str = string.Format(conn, ndb);
            entity = new TrainingEntities(str);
        }

        public void Save()
        {
            entity.SaveChanges();
        }

        public void AddWord(Word w)
        {
            entity.AddToWords(w);
        }

        public Config GetConfig()
        {
            return entity.Configs.FirstOrDefault();
        }

        public void DeletePackage(Package p)
        {
            Package parent = (Package)PackageManager.Instance.Project.GetPackageItem("TrainingLists\\Lists");
            

            string tgtfn = PackageManager.Instance.Project.GetFileName("TrainingLists\\Lists\\" + p.Key);
            System.IO.File.Delete(tgtfn);

            parent.Items.Remove(p);
            PackageManager.Instance.Project.SaveProject("TrainingLists");
        }

        public Config CreateConfig(ListConfig lc, GameConfig gc)
        {

            string srcfn = PackageManager.Instance.Project.GetFileName("TrainingLists\\Templates\\TrainingEmpty");

            Package p = (Package)PackageManager.Instance.Project.GetPackageItem("TrainingLists\\Lists");

            string newKey = "TrainingLists\\Lists\\" + lc.Name;
            Package tgt = (Package)PackageManager.Instance.Project.GetPackageItem(newKey);

            if (tgt != null)
            {
                return null;
            }

            tgt = new Package();

            tgt.Description = lc.Name;
            tgt.Filename = Guid.NewGuid().ToString() + ".db";
            tgt.FileType = FileType.File;
            tgt.LocationType = LocationType.PersonalFiles; 
            tgt.Key = lc.Name;

            p.Items.Add(tgt);

            string tgtfn = PackageManager.Instance.Project.GetFileName(newKey);

            try
            {
                System.IO.File.Copy(srcfn, tgtfn, false);
            }
            catch
            {
                p.Items.Remove(tgt);
                return null;
            }


            PackageManager.Instance.Project.SaveProject("TrainingLists");

            ReinitializeEntity(newKey);


            Config tc = new Config();
            tc.XMLConfig = new TopMachine.Desktop.Overall.ObjectSerializer<ListConfig>().Serialize(lc);
            tc.XMLPlay = new TopMachine.Desktop.Overall.ObjectSerializer<GameConfig>().Serialize(gc);
            entity.AddToConfigs(tc);
            entity.SaveChanges();

            return tc;


        }

        public void CreateSession(Session s)
        {
            entity.AddToSessions(s);
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

            string query = "SELECT  Status, COUNT(Rack) AS Counter, SUM(Lost) AS Lost, SUM(Succeeded) AS Succeeded, SUM(Total) AS Total FROM Words GROUP BY Status";

            SQLiteConnection connec = new SQLiteConnection(ConnectionString);
            SQLiteCommand cmd = new
                SQLiteCommand(query, connec);
            connec.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                WordStatistic ws = new WordStatistic
                {
                    Status = (int) reader["Status"],
                    Counter = (long)reader["Counter"],
                    Lost = (long)reader["Lost"],
                    Succeeded = (long)reader["Succeeded"],
                    Total = (long)reader["Total"]
                };

                ws.Words = new List<string>();
                int status = ws.Status;

                ws.Words = (from w in entity.Words where w.Status == status select w.Rack).ToList();

                list.Add(ws);
                
            }

            connec.Dispose();

            return list;
        }


        public Word GetWord(string rack)
        {
            return entity.Words.Where(p => p.Rack == rack).FirstOrDefault(); 
        }

        public void DeleteWords()
        {
            SQLiteConnection connec = new SQLiteConnection(GetConnectionString());
            SQLiteCommand cmd = new
                SQLiteCommand("Delete from Words", connec);
            connec.Open();
            cmd.ExecuteNonQuery();
            connec.Dispose();

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
