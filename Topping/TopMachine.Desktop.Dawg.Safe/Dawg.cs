using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TopMachine.Topping.Dawg;
using System.Diagnostics;

namespace TopMachine.Topping.Dawg
{
    // 78% de compression
    public class Dawg
    {
        public Node Root;
        public int nbWords;
        public int nbNodes;
        public int nbNodesInit;

        public Dawg()
        {
            Root = null;
            nbWords = 0;
            nbNodes = 0;
            nbNodesInit = 0;
        }

        private Node SearchNode(Node current, char c)
        {
            if (current == null) return null;

            if (current.Letter == c)
            {
                return current;
            }
            else
            {
                return SearchNode(current.Next, c);

            }
        }

        public void AddWord(string word)
        {
            Node current = Root;
            Node prec = null;
            Node n = null;
            foreach (char c in word)
            {
                n = SearchNode(current, c);

                if (n == null)
                {

                    if (current == null)
                    {
                        current = CreateNewNode(c, false);
                        if (Root == null)
                        {
                            Root = current;
                            prec = current;
                        }
                        else
                        {
                            prec.Child = current;
                            current.Parent = prec;
                            current.addParents(prec);
                        }
                    }
                    else
                    {
                        if (current.Next != null)
                        {  // decalage 

                            while (current.Next != null)
                            {
                                current = current.Next;
                            }

                        }
                        current.Next = CreateNewNode(c, false);
                        current.Next.Parent = current.Parent;
                        current.Next.addParents(current.Parent);
                        current = current.Next;

                    }
                    prec = current;
                    current = current.Child;

                }
                else
                {
                    prec = n;
                    current = n.Child;

                }

            }

            if (prec != null)
            {
                nbWords++;
                prec.Final = true;
            }



        }

        public Node CreateNewNode(char letter, bool final)
        {
            Node n = new Node();

            n.Letter = letter;
            n.Final = final;

            if (final) { nbWords++; }

            nbNodes++;
            nbNodesInit++;
            return n;

        }

        private void DisplayWordDisplay(Node current, string strCurrent, List<Node> lstNodeUsed)
        {
            if (current == null)
            {
                return;
            }
            else
            {
                Node prec = null;
                if (current != null)
                {
                    if (!lstNodeUsed.Contains(current))
                    {
                        lstNodeUsed.Add(current);
                    }
                    strCurrent += current.Letter;
                    if (current.Final)
                    {

                        Console.WriteLine(strCurrent);


                    }
                    DisplayWordDisplay(current.Child, strCurrent, lstNodeUsed);
                    prec = current;
                    current = current.Child;
                }

                if (prec != null)
                {
                    if (prec.Next != null)
                    {
                        strCurrent = strCurrent.Remove(strCurrent.Length - 1);
                        DisplayWordDisplay(prec.Next, strCurrent, lstNodeUsed);
                    }
                }
            }


        }

        private List<string> addWordDisplay(List<string> lst, Node current, string strCurrent, List<Node> lstNodeUsed)
        {
            if (current == null)
            {
                return null;
            }
            else
            {
                Node prec = null;
                if (current != null)
                {
                    if (!lstNodeUsed.Contains(current))
                    {
                        lstNodeUsed.Add(current);
                    }
                    strCurrent += current.Letter;
                    if (current.Final)
                    {
                        // if (lst.Count > 300) return null;
                        lst.Add(strCurrent);

                    }
                    addWordDisplay(lst, current.Child, strCurrent, lstNodeUsed);
                    prec = current;
                    current = current.Child;
                }

                if (prec != null)
                {
                    if (prec.Next != null)
                    {
                        strCurrent = strCurrent.Remove(strCurrent.Length - 1);
                        addWordDisplay(lst, prec.Next, strCurrent, lstNodeUsed);
                    }
                }
            }
            return lst;

        }
        public List<string> getWords()
        {
            List<string> lst = new List<string>();
            string word = string.Empty;


            List<Node> lstNodeUsed = new List<Node>();
            addWordDisplay(lst, Root, word, lstNodeUsed);

            return lst;


        }

        public string getStat()
        {
            string result = string.Empty;

            result = " nombre de noeuds de départ : " + nbNodesInit + Environment.NewLine;
            result += " nombre de noeuds après optimisation : " + nbNodes + Environment.NewLine;
            double ratio = 1 - (double)nbNodes / nbNodesInit;
            result += " pourcentage de compression : " + string.Format("{0:0.0%}", ratio);

            return result;

        }

        public void DisplayTree()
        {

            string word = string.Empty;

            List<Node> lstNodeUsed = new List<Node>();
            DisplayWordDisplay(Root, word, lstNodeUsed);


        }

        #region Optimisation
        public void Optimisation()
        {
            Node current = Root;
            var lst = CreerLevelBuilder();
            //_Optimisation(Root, Root);

            var lst2 = lst.OrderBy(x => x.level).ToList();
            int maxlevel = lst2[lst2.Count() - 1].level;

            Node branch, branch2;

            // 2 mots de longueurs differentes peuvent etre optimise ce qui engendre que pour chaque level il faut passer en revue les autres levels
            // exemple aas et abacosts => h et i represente des mots de longueurs differentes
            int cptlevelCurrent_h;//, cptlevelCurrent_i;
            for (int h = 0; h <= maxlevel; h++)
            {
                var lstLevel_h = lst2.Where(x => x.level == h).ToList();
                cptlevelCurrent_h = lstLevel_h.Count();

                int j = 0, k;
                while (j < cptlevelCurrent_h)
                {
                    k = j + 1;
                    while (k < cptlevelCurrent_h)
                    {
                        branch = lstLevel_h[j].node;
                        branch2 = lstLevel_h[k].node;

                        // si le branch2 est un noeud qui est une reference d'un next alors ne rien faire
                        if ((branch2.Parent == null || branch2.Letter == branch2.Parent.Child.Letter) &&
                            _Optimisation(branch, branch2, lst2, lstLevel_h, k))
                        {
                            cptlevelCurrent_h--;

                            if ((j + k) % 300 == 0)
                            {

                                Console.WriteLine("Niveau : " + h + " noeuds : " + nbNodes);

                            }
                        }
                        else
                        {
                            k++;
                        }

                    }

                    j++;




                }


            }





        }

        private bool _Optimisation(Node branch, Node branch2, List<Levelbuilder> lst, List<Levelbuilder> lstLevel, int k)
        {
            if (branch2.Letter != char.MaxValue && IsOptimisationNode(branch, branch2))
            {

                foreach (var parent in branch2.Parents)
                {
                    parent.Child = branch;
                    branch.addParents(parent);
                }


                // supprimer toutes les branches qui se rapporte a se noeud 


                int cptDelete = lstLevel[k].node.deletechildren(lst);

                nbNodes -= cptDelete;
                lstLevel.Remove(lstLevel[k]);
                branch2.Dispose();
                branch2 = null;

                return true;


            }

            return false;
        }
        #endregion

        #region Creation d'une liste representant toutes les branches de l'arbre
        private List<Levelbuilder> CreerLevelBuilder()
        {
            List<Levelbuilder> lstResult = new List<Levelbuilder>();

            Node current = Root;

            while (current != null)
            {
                lstResult.Add(new Levelbuilder { node = current, level = 0 });
                current = current.Next;
            }

            _creerLevelBuilder(lstResult, 0);
            return lstResult;

        }
        private void _creerLevelBuilder(List<Levelbuilder> lstResult, int level)
        {
            var lst = lstResult.Where(x => x.level == level).ToList();

            int levelCurrent = level;
            if (lst.Count() > 0)
            {
                levelCurrent++;
                foreach (var item in lst)
                {
                    if (item.node.Child != null)
                    {
                        lstResult.Add(new Levelbuilder { node = item.node.Child, level = levelCurrent });
                        Node current = item.node.Child.Next;
                        while (current != null)
                        {
                            lstResult.Add(new Levelbuilder { node = current, level = levelCurrent });
                            current = current.Next;
                        }

                    }
                }


                _creerLevelBuilder(lstResult, levelCurrent);
            }



        }
        #endregion


        /// <summary>
        ///  verifier si les deux noeuds sont identique pour pouvoir en supprimer un des deux et rattacher les deux branches a ce noeud
        /// </summary>
        /// <returns></returns>
        public bool IsOptimisationNode(Node n1, Node n2)
        {
            if (n1 == null && n2 == null)
            {
                return true;
            }
            else
                if (n1 == null || n2 == null || n1 == n2 || n1.Final != n2.Final)
                {
                    return false;
                }
            // pas d'autre noeud next que n1 et n2 
            if (n1.IsFeuille() && n2.IsFeuille() && n1.Letter == n2.Letter && n1.Next == null && n2.Next == null)
            {
                return true;
            }
            else if (n1.Letter == n2.Letter)
            {
                return IsOptimisationNode(n1.Next, n2.Next) && IsOptimisationNode(n1.Child, n2.Child);
            }
            else if (n1.Letter != n2.Letter || n1.Final != n2.Final)
            {
                return false;
            }
            else
            {
                return IsOptimisationNode(n1.Child, n2.Child);

            }

        }


        private void _getallNodesToWritefile(Node r, List<Node> lstResult, bool forcedOrder)
        {
            if (r != null)
            {
                if (!lstResult.Contains(r) || forcedOrder)
                {

                    r.id = lstResult.Count + 1;
                    lstResult.Add(r);
                    _getallNodesToWritefile(r.Next, lstResult, true);
                    _getallNodesToWritefile(r.Child, lstResult, false);

                    if (r.Child != null)
                    {
                        r.IndexChild = (uint)r.Child.id;


                    }
                }
            }
        }

        public List<Node> getallNodesToWritefile()
        {

            List<Node> lstResult = new List<Node>();

            _getallNodesToWritefile(Root, lstResult, false);
            if (lstResult.Count != nbNodes)
            {
                nbNodes = lstResult.Count();
            }

            return lstResult;

        }

        public byte[] StructureToByteArray(Dict_header header)
        {



            byte[] arr = new byte[36];

            IntPtr ptr = Marshal.AllocHGlobal(36);

            Marshal.StructureToPtr(header, ptr, true);

            Marshal.Copy(ptr, arr, 0, 36);

            Marshal.FreeHGlobal(ptr);

            return arr;

        }


        private List<byte> getHeader()
        {
            List<byte> lstResult;

            Dict_header header = new Dict_header();
            header.edgesused = (uint)nbNodes;
            header.nwords = (uint)nbWords;
            header.root = 0;
            header.edgessaved = (uint)nbNodes;


            lstResult = new List<byte>(header.ident);

            //// 2 bytes to unused

            lstResult.Add(0);
            lstResult.Add(0);

            byte[] result = BitConverter.GetBytes(header.root);
            lstResult = lstResult.Concat(result).ToList();

            result = BitConverter.GetBytes(header.nwords);
            lstResult = lstResult.Concat(result).ToList();

            result = BitConverter.GetBytes(header.edgesused);
            lstResult = lstResult.Concat(result).ToList();

            result = BitConverter.GetBytes(header.nodesused);
            lstResult = lstResult.Concat(result).ToList();

            result = BitConverter.GetBytes(header.nodessaved);
            lstResult = lstResult.Concat(result).ToList();

            result = BitConverter.GetBytes(header.edgessaved);
            lstResult = lstResult.Concat(result).ToList();

            return lstResult;


        }

        public byte[] getByteArray()
        {

            List<Node> lstN = getallNodesToWritefile();
            return _getByteArray(lstN);

        }

        private byte[] _getByteArray(List<Node> lst)
        {
            List<byte> lstB = getHeader(); ;

            byte[] lstBResult = new byte[lstB.Count + (lst.Count * 4) + 4];
            byte[] nodeCurrent;

            int currentIndex = 0;
            foreach (var item in lstB)
            {
                lstBResult[currentIndex] = item;
                currentIndex++;
            }



            var root = new Node();

            root.IndexChild = 1;
            var r = BitConverter.GetBytes(root.ConvertUintToNode());
            foreach (var nodeByte in r)
            {
                lstBResult[currentIndex] = nodeByte;
                currentIndex++;
            }




            foreach (var item in lst)
            {


                nodeCurrent = BitConverter.GetBytes(item.ConvertUintToNode());

                foreach (var nodeByte in nodeCurrent)
                {
                    lstBResult[currentIndex] = nodeByte;
                    currentIndex++;
                }

            }


            return lstBResult;

        }





    }
}
