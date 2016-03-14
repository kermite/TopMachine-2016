using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CMWA.Packager.Custom
{
    public partial class ShortCutEditor : UserControl, ICustomPackageItemEditor
    {
        ShortCut currentItem = null;
        ShortCut rootItem = null;
        PackageFile file = new PackageFile();
        PackageProject project = new PackageProject();
        Type baseType = null;

        public ShortCutEditor()
        {
            InitializeComponent();
        }

        public void SetObject(Type basetype, PackageProject prj, PackageFile o)
        {
            project = prj; 
            file = o;
            rootItem = (ShortCut)o.Object;
            LoadTreeFromProject(rootItem); 
        }

        public void LoadTreeFromProject(ShortCut prj)
        {
            rootItem = prj;
            treeView2.Nodes.Clear();
            foreach (ShortCut currentItem in rootItem.Items)
            {
                AddItemInTree(currentItem, null);
            }
        }

        private void AddItemInTree(ShortCut p, TreeNode parent)
        {
            TreeNode node = new TreeNode();
            SetItemNodeValues(node, p); 

            if (parent == null)
            {
                treeView2.Nodes.Add(node);
            }
            else
            {
                parent.Nodes.Add(node); 
            }

            foreach (ShortCut np in p.Items)
            {
                AddItemInTree(np, node); 
            }
        }


        public void SetItemNodeValues(TreeNode node, ShortCut p)
        {
            node.Text = p.Key;
            node.Tag = p; 
        }


        private ShortCut CreateNewObject()
        {
            return new ShortCut();
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (treeView2.SelectedNode != null)
            {
                ShortCut nav = CreateNewObject();
                nav.Key = "New Key";
                currentItem.Items.Add(nav);
                AddItemInTree(nav, treeView2.SelectedNode);
            }
            else
            {
                ShortCut nav = CreateNewObject();
                nav.Key = "New Key";
                currentItem.Items.Add(nav);
                AddItemInTree(nav, null);
            }

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (treeView2.SelectedNode != null && currentItem  != null)
            {
                if (MessageBox.Show("Are you sure you want to remove this file from the collection ?", "Confirmation", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    TreeNode node = treeView2.SelectedNode;
                    ShortCut p = (ShortCut)node.Tag;
                    currentItem.Items.Remove(p);
                    treeView2.Nodes.Remove(node);
                }
            }
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            currentItem = (ShortCut)e.Node.Tag;
            propertyGrid2.SelectedObject = (IPackageItem)e.Node.Tag;
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShortCut nav = new ShortCut();
            nav.Key = "New Key"; 
            rootItem.Items.Add(nav);
            AddItemInTree(nav, null);
        }

        private void propertyGrid2_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            switch (e.ChangedItem.Label)
            {
                case "Key":
                    TreeNode node = treeView2.SelectedNode;
                    IPackageItem p = currentItem;
                    node.Text = p.Key;
                    node.Tag = p; 
                    break;
            }
        }

        private void copyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Clipboard.SetText(currentNode.FullPath);
        }

        TreeNode currentNode = null;
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            currentNode = e.Node;
        }


    }
}
