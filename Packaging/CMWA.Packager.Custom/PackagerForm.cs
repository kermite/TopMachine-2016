using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CMWA.Packager.Custom
{
    public partial class PackagerForm : UserControl, ICustomPackageItemEditor
    {
        PackageFile file = new PackageFile(); 
        Package     rootPackage = new Package(); 

        Package currentPackage = new Package();
        PackageProject currentProject = new PackageProject();

        Type baseType = null; 

        #region ICustomPackageItemEditor Members

        public void SetObject(Type baseType, PackageProject prj, PackageFile o)
        {
           currentProject = prj; 
           file = o; 
           rootPackage = (Package) file.Object; 
           AddPackageInTree(rootPackage, null);
        }

        #endregion

        public PackagerForm()
        {
            InitializeComponent();
        }

        private void AddPackageInTree(Package p, TreeNode parent)
        {
            TreeNode node = new TreeNode();
            SetPackageNodeValues(node, p); 

            if (parent == null)
            {
                treeView1.Nodes.Add(node);
            }
            else
            {
                parent.Nodes.Add(node); 
            }

            foreach (Package np in p.Items)
            {
                AddPackageInTree(np, node); 
            }
        }


        public void SetPackageNodeValues(TreeNode node, Package p)
        {
            node.Text = p.Key != null && p.Key.Length > 0 ? p.Key : p.Filename;
            node.Tag = p; 

            switch (p.FileType)
            {
                case FileType.File: node.ImageIndex = 2; break;
                case FileType.Assembly: node.ImageIndex = 0; break;
                case FileType.Image: node.ImageIndex = 3; break;
                case FileType.Resource: node.ImageIndex = 7; break;
                case FileType.PackageDefinition: node.ImageIndex = 4; break;
                case FileType.RootFiles: node.ImageIndex = 6; break;
                case FileType.RootZip: node.ImageIndex = 1; break;
                case FileType.RootModule: node.ImageIndex = 0; break;
                case FileType.Folder: node.ImageIndex = 5; break;
            }

            node.SelectedImageIndex = node.ImageIndex;

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            currentPackage = (Package)e.Node.Tag;
            propertyGrid1.SelectedObject = (Package)e.Node.Tag;
        }


        private void saveCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;

            if (node != null)
            {
                file.Save(currentProject.Path);
            }
        }

        private void saveCurrentAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FilterIndex = 2;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file.OriginalFile = saveFileDialog1.FileName;
                file.Save(currentProject.Path);
            }
        }


        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            switch (e.ChangedItem.Label)
            {
                case "Key":
                case "Filename":
                case "FileType":
                    SetPackageNodeValues(treeView1.SelectedNode, (Package)treeView1.SelectedNode.Tag);  
                    break; 
                case "OriginalFile":
                        string fn = currentPackage.OriginalFile; 
                        if (fn.StartsWith(currentProject.Path))
                        {
                            fn = fn.Substring(currentProject.Path.Length);
                        }
                        currentPackage.OriginalFile = fn; 
                        break;
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && currentPackage != null)
            {
                Package folder = new Package() { Filename = "New folder", FileType = FileType.Folder };
                currentPackage.Items.Add(folder);
                AddPackageInTree(folder, treeView1.SelectedNode); 
            }
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && currentPackage != null)
            {
                if (openFileMultiple.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (string fnn in openFileMultiple.FileNames)
                    {
                        string fn = fnn; 
                        if (fn.StartsWith(currentProject.Path))
                        {
                            fn = fn.Substring(currentProject.Path.Length);
                        }

                        Package folder = new Package() { OriginalFile = fn, Key=Path.GetFileNameWithoutExtension(fn),  Filename = Path.GetFileName(fn), FileType = FileType.Folder };
                        folder.FileType = ResolveExtension(Path.GetExtension(fn));
                        currentPackage.AddPackage(currentProject.Path,  folder);
                        AddPackageInTree(folder, treeView1.SelectedNode); 
                    }
                }
            }

        }

        private FileType ResolveExtension(string fn)
        {
            switch(fn.Substring(1).ToLower())
            {
                case "jpg":
                case "png":
                case "gif":
                case "bmp":
                    return FileType.Image; 
                case "exe":
                case "dll":
                    return FileType.Assembly;
                case "xpck":
                    return FileType.PackageDefinition;
                default:
                    return FileType.File; 
            }
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent != null)
            {
                if (MessageBox.Show("Are you sure you want to remove this file from the package?", "Confirmation", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    TreeNode node = treeView1.SelectedNode;
                    Package p = (Package)node.Tag;
                    Package parent = (Package)node.Parent.Tag;
                    treeView1.Nodes.Remove(node);
                    parent.Items.Remove(p);
                }
            }
        }

        private void ctxtTreeView_Opening(object sender, CancelEventArgs e)
        {
            
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
