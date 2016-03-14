using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopMachine.Topping.Dawg
{
    public class Node : IDisposable     
    {
        // ramification en profondeur
        public Node Child;
        
        private static int index = 0;
        public int id;

        public uint IndexChild = 0;
        // la lettre representant le noeud
        public char Letter;
        // si la node est un noeud final
        public bool Final;
          // ramification en hauteur
        public Node Next;
        public Node Parent;

        public List<Node> Parents;

        public Node()
        {
            index++;
            id = index;
            Parents = new List<Node>();
        }    

        // si la node est une feuille donc plus de ramification en profondeur
        public bool IsFeuille() 
        {
            return Child == null;
        }

        /// <summary>
        /// converti un node en uint 
        /// char (5 bits),blank(1 bit),last(1 bit),final(1 bit),ptr child(24 bits)  
        /// </summary>
        /// <returns></returns>
        public uint ConvertUintToNode() 
        {
            uint result = 0;
           
        
            result = (uint)((Convert.ToByte(Letter) - 64));
            result = (result << 1);
            result = (result << 1) | (uint)(Convert.ToByte(Next == null));
            result =  (result << 1) | (uint)(Convert.ToByte(Final));
            
            result = result << 24 | IndexChild;

            return result;




        }

        public int deletechildren(List<Levelbuilder> lst )
        {
           int cpt = _deletechildren(lst, this,0);
           return cpt;
         
           
        }
       
        public void addParents(Node p) 
        {
            if (p != null) 
            {               
                this.Parents.Add(p);
                
            }  
        
        }

        public int _deletechildren (List<Levelbuilder> lst,Node current,int cpt) 
       {
           if (current != null && current.Letter != char.MaxValue)
           {
               cpt++;
                             
               cpt = _deletechildren(lst,current.Child,cpt);
               cpt = _deletechildren(lst, current.Next,cpt);
               
               lst.Remove(lst.Where(x=> x.node == current).First());
               current.Dispose();

              
           }

           return cpt;
       }

        public string GetPrefix() 
        {
            string result = string.Empty;
            Node current; 
            foreach (var item in Parents)
            {
                string temp = string.Empty;
                current = item;

                while (current != null)
                {
                    temp = current.Letter + temp;
                    current = current.Parent;
                }

                result += temp + Environment.NewLine;
            }
            
           
            return result;
        } 


        public void Dispose()
        {
           
            Child = null;
            Parent = null;
            Letter = char.MaxValue;
            id = 0;
        }
    }
}
