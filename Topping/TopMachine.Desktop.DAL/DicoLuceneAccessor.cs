using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Index;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Search;

namespace TopMachine.Topping.DAL
{
    public class DicoPair
    {
        public int docId { get; set; }
        public string word { get; set; }
        public bool isNew { get; set; }
    }
        

    public class DicoAccessor
    {
        public static void Find()
        {
            ;
        }

        public List<DicoPair> GetDicoIds()
        {
            try
            {
                List<DicoPair> dicos = new List<DicoPair>();
                Query query2 = new TermQuery(new Term("Content", "Dico"));
                Hits hits = GetIndexSearcher().Search(query2);

                int len = hits.Length();

                for (int i = 0; i < len; i++)
                {
                    Document doc = hits.Doc(i);
                    dicos.Add(
                           new DicoPair
                           {
                               docId = int.Parse(doc.Get("DicoId")),
                               word = doc.Get("Key"),
                               isNew = bool.Parse(doc.Get("Ods6")) || bool.Parse(doc.Get("Ods6Modif"))
                           }
                        );
                }



                return dicos;
            }
            finally
            {
                src.Close();
                src = null;
            }
        }

        
        IndexSearcher src = null;

        public List<Dico> GetDicos(List<DicoPair> dicoIds)
        {
            try
            {
                List<Dico> dicos = null;
                List<int> ids = (from dp in dicoIds select dp.docId).ToList();
                return GetDicos(ids);
            }
            finally
            {
                src.Close();
                src = null;
            }
        }

        public List<Dico> GetDicos(List<int> dicoIds)
        {
            List<Dico> dicos = new List<Dico>();

            foreach (int dicoId in dicoIds)
            {
                Query query = new TermQuery(new Term("DicoId", dicoId.ToString()));
                Query query2 = new TermQuery(new Term("Content", "Dico"));
                BooleanQuery bq = new BooleanQuery();
                bq.Add(query, BooleanClause.Occur.MUST);
                bq.Add(query2, BooleanClause.Occur.MUST);

                Hits hits = GetIndexSearcher().Search(bq);
                int len = hits.Length();

                for (int i = 0; i < len; i++)
                {
                    Document doc = hits.Doc(i);
                    dicos.Add(GetDico(doc));
                }

            }

            return dicos;
        }

        public List<Dico> GetWord(string word, bool close)
        {
            try
            {

				//Query query = new TermQuery(new Term("Mot", word));
				Query query = new TermQuery(new Term("Definition", word));
				//Query query2 = new TermQuery(new Term("Content", "Mot"));
				//Query query2 = new TermQuery(new Term("Content", "Definition"));

				BooleanQuery bq = new BooleanQuery();
                bq.Add(query, BooleanClause.Occur.MUST_NOT);
                //bq.Add(query2, BooleanClause.Occur.MUST);

                IndexSearcher src = GetIndexSearcher();
                Hits hits = src.Search(bq);
                int len = hits.Length();

                List<int> dicoIds = new List<int>();
                for (int i = 0; i < len; i++)
                {
                    Document doc = hits.Doc(i);
                    int d = int.Parse(doc.Get("DicoId"));
                    if (!dicoIds.Contains(d))
                    {
                        dicoIds.Add(d);
                    }
                }



                return GetDicos(dicoIds);
            }
            finally
            {
                if (close)
                {
                    src.Close();
                    src = null; 
                }
            }
        }


        public List<Dico> GetWords(List<string> words)
        {
            try
            {
                List<Dico> dicos = new List<Dico>();
                foreach (string s in words)
                {
                    dicos.AddRange(GetWord(s, false));
                }

                return dicos.Distinct().ToList();
            }
            finally
            {
                src.Close();
                src = null;
            }
        }

        public Dico GetDico(Document doc)
        {
            return new Dico
            {
                Definition = doc.Get("Definition"),
                Autres = doc.Get("Detail"),
                ID = int.Parse(doc.Get("DicoId")),
                Genre = doc.Get("Genre"),
                isInvariable = bool.Parse(doc.Get("Invariable")),
                Key = doc.Get("Key"),
                Mot = doc.Get("Mot"),
                isODS5 = bool.Parse(doc.Get("Ods6")) || bool.Parse(doc.Get("Ods6Modif"))
            };
        }


        public IndexSearcher GetIndexSearcher()
        {
            if (src == null)
            {
                string dir = CMWA.Packager.FileUtility.GetFile("index", CMWA.Packager.LocationType.PersonalFiles);

                if (System.IO.Directory.Exists(dir))
                {
                    src = new IndexSearcher(dir);
                }
            }

            return src;

        }

        public IndexWriter GetIndexWriter(bool ForceRecreation)
        {
            string dir = CMWA.Packager.FileUtility.GetFile("index", CMWA.Packager.LocationType.PersonalFiles);

            if (!ForceRecreation
                && System.IO.Directory.Exists(dir)
                && new System.IO.DirectoryInfo(dir).GetFiles().Length > 1)
                return new IndexWriter(dir, new StandardAnalyzer(), false);
            else
            {
                IndexWriter iw = new IndexWriter(dir, new StandardAnalyzer(), true);
                return iw;
            }
        }


        public void WriteDicoDto(Dico dto, IndexWriter w)
        {
            global::Lucene.Net.Documents.Document d = new global::Lucene.Net.Documents.Document();
            d.Add(new Field("Content", "Dico", Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
            d.Add(new Field("Definition", dto.Definition == null ? "" : dto.Definition, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
            d.Add(new Field("DicoId", dto.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
            d.Add(new Field("Genre", dto.Genre, Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
            d.Add(new Field("Invariable", dto.isInvariable.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
            d.Add(new Field("Key", dto.Key, Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
            d.Add(new Field("Mot", dto.Mot, Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
            d.Add(new Field("Ods6", dto.isODS5.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
            w.AddDocument(d);

        }

        public void WriteMotDto(Mot dto, IndexWriter w)
        {
            global::Lucene.Net.Documents.Document d = new global::Lucene.Net.Documents.Document();
            d.Add(new Field("Content", "Mot", Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
            d.Add(new Field("Mot", dto.Mot1, Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
            d.Add(new Field("DicoId", dto.DicoId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
            d.Add(new Field("Type", dto.Type.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
            w.AddDocument(d);

        }
    }
}
