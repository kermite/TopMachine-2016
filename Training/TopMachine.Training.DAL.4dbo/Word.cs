//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Db4objects.Db4o.Config.Attributes;
using System.Xml;
using System.IO;

namespace TopMachine.Training.DAL.fdbo
{
    public partial class Word
    {
        #region Primitive Properties

        public virtual string Rack
        {
            get;
            set;
        }
    
        public virtual int Lost
        {
            get;
            set;
        }
    
        public virtual int Succeeded
        {
            get;
            set;
        }
    
        public virtual int Total
        {
            get;
            set;
        }

        public virtual int Status
        {
            get;
            set; 
        }
    
        public virtual string WordsXML
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        ///  recupere toutes les solutions pour un tirage
        /// </summary>
        /// <returns></returns>
        public List<string> getWordsSolution() 
        {
            List<string> LstResult = new List<string>();
            string mot = "init";
            using (XmlReader reader = XmlReader.Create(new StringReader(WordsXML)))
            {

                while (mot != string.Empty)
                {
                    reader.ReadToFollowing("string");
                    mot = reader.ReadInnerXml();

                    if (mot != string.Empty)
                    {
                        LstResult.Add(mot);
                    }

                }
            }

            return LstResult;
        }

        public static string GetHeader() 
        {

            return "Tirage;Reussi;Rate;Status;Total;Solutions";
        }

        public override string ToString()
        {
            string result = string.Empty;

            string words = string.Empty;

            foreach (var item in getWordsSolution())
            {
                words += item + " ";
            }

            result = Rack + ";" + Succeeded + ";"+ Lost + ";" + Status + ";" + Total + ";" + words; 

            return result;
        }
    }
}
