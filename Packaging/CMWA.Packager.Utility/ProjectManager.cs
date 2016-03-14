using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CMWA.Packager.Tools.Bytes;

namespace CMWA.Packager.Utility
{
    public partial class ProjectManager : Form
    {
        PackageProject currentProject = new PackageProject();

        public ProjectManager()
        {
            InitializeComponent();

            foreach (string s in CMWA.Packager.Utility.Properties.Settings.Default.PackageFileTypes)
            {
                ToolStripMenuItem mi = new ToolStripMenuItem();
                mi.Text = s.Split(new char[] { ',' })[0];
                mi.Tag = s;
                mi.Click += new EventHandler(mi_Click);
                btnAddPackage.DropDownItems.Add(mi);

                ListViewGroup grp = new ListViewGroup(mi.Text);
                grp.Name = mi.Text;
                listPackages.Groups.Add(grp);
            }


        }

        void mi_Click(object sender, EventArgs e)
        {
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fn = openFileDialog1.FileName;
                string path = currentProject.Path;

                if (fn.StartsWith(path))
                {
                    fn = fn.Substring(path.Length); 
                }
                
                ToolStripMenuItem mi = (ToolStripMenuItem)sender; 

                
                PackageFile pf = new PackageFile();
                pf.Key = Path.GetFileNameWithoutExtension(fn);
                pf.OriginalFile = fn;
                pf.TypeName = mi.Tag.ToString(); 

                currentProject.Packages.Add(pf);

                ListViewItem vi = new ListViewItem();
                vi.Text = pf.Key;
                vi.Tag = pf;
                vi.Group = listPackages.Groups[pf.TypeName.Split(new char[] { ',' })[0]];
                listPackages.Items.Add(vi);


                listPackages.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<TabPage> tp = new List<TabPage>();
                foreach (TabPage p in tabControl1.TabPages)
                {
                    tp.Add(p);
                }

                foreach (TabPage p in tp)
                {
                    tabControl1.TabPages.Remove(p);
                    p.Dispose();
                }

                currentProject = PackageProject.OpenProject(openFileDialog1.FileName, true);
                CreateList();
            }
        }

        private void CreateList()
        {
            listPackages.Items.Clear();
            foreach (PackageFile pf in currentProject.Packages)
            {
                ListViewItem vi = new ListViewItem();
                vi.Text = pf.Key;
                vi.Tag = pf;
                vi.Group = listPackages.Groups[pf.TypeName.Split(new char[] { ',' })[0]];
                listPackages.Items.Add(vi);
            }

            listPackages.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
        }


        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentProject != null)
            {
                currentProject.SaveProject(true);
            }
        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FilterIndex = 1;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currentProject.FileName = saveFileDialog1.FileName;
                currentProject.SaveProject(true);
            }
        }
        private void listPackages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPackages.SelectedItems.Count > 0)
            {
                PackageFile pf = (PackageFile)listPackages.SelectedItems[0].Tag;
                if (tabControl1.TabPages[pf.Key] == null)
                {
                    pf.GetOrCreateObject(currentProject.Path);
                    foreach (Attribute att in pf.Object.GetType().GetCustomAttributes(true))
                    {
                        if (att is CustomPackageItemEditorAttribute)
                        {
                            CustomPackageItemEditorAttribute attEditor = (CustomPackageItemEditorAttribute)att;
                            UserControl editor = (UserControl)attEditor.Editor.GetConstructor(new Type[0]).Invoke(new Type[0]);
                            ((ICustomPackageItemEditor)editor).SetObject(pf.Object.GetType(), currentProject,  pf);
                            TabPage tab = new TabPage();
                            tab.Tag = pf;
                            tab.Name = pf.Key;
                            tab.Text = pf.Key;

                            tabControl1.TabPages.Add(tab);
                            tabControl1.SelectedTab = tab;

                            ((UserControl)editor).Dock = DockStyle.Fill;
                            tab.Controls.Add((UserControl)editor);
                        }
                    }
                }
                else
                {
                    tabControl1.SelectedTab = tabControl1.TabPages[pf.Key];
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            currentProject.SaveProject(true);
            saveFileDialog1.FileName = "SelectAFolder.txt";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                statusAction.Text = "Exporting...";
                statusPB.Visible = true;
                timer1.Enabled = true;

                string fn = Path.GetDirectoryName(saveFileDialog1.FileName);
                currentProject.Export(fn);

                statusAction.Text = "Exporting done...";
                statusPB.Visible = false;
                timer1.Enabled = false;

            }
            
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            statusPB.Value = (statusPB.Value + 10) % 100;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string fn = openFileDialog1.FileName; 
                new cpt().WriteFile(fn, fn + ".dat", "", 0);
            }
        }
    }
}
