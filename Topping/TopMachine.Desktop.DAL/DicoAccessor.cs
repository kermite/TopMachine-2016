using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Data.Objects;
using CMWA.Packager;

namespace TopMachine.Topping.DAL
{
    public class DicoOldAccessor
    {
        DicoFREntities entity = null;

        public DicoOldAccessor()
        {
            string conn = "metadata=res://*/DicoModel.csdl|res://*/DicoModel.ssdl|res://*/DicoModel.msl;provider=System.Data.SQLite;provider connection string=\"data source={0}\";";
            string db = PackageManager.Instance.Project.GetFileName("TopMachineData\\Databases\\DicoFR");

            string str = string.Format(conn, db);
            entity = new DicoFREntities(str);
        }

        public void AttachDico(Dico dico)
        {
        }

        public void Save()
        {
            entity.SaveChanges();
            entity.AcceptAllChanges(); 
        }

        public ObjectQuery<Dico>  GetDicoEntity()
        {
            
            return entity.Dicoes; 
        }

        public List<Dico> GetWord(string word)
        {
            IQueryable<Mot> qWord =  entity.Mots.Where(p => p.Mot1 == word.ToUpper());
             List<Dico> dicos = new List<Dico>(); 

            foreach(int id in (from w in qWord select w.DicoId).Distinct())
            {
                dicos.Add(entity.Dicoes.Where(p=>p.ID == id).FirstOrDefault()); 
            }

            return dicos; 

        }


        public List<Dico> GetWords(List<string> words)
        {
            IQueryable<Mot> qWord = entity.Mots.Where(p => words.Contains(p.Mot1));
            List<Dico> dicos = new List<Dico>();

            foreach (int id in (from w in qWord select w.DicoId).Distinct())
            {
                dicos.Add(entity.Dicoes.Where(p => p.ID == id).FirstOrDefault());
            }

            return dicos;

        }




    }
}
