using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMWA.Packager.Custom;
using CMWA.Packager;
using System.IO;
using TopMachine.Desktop.Controls;
using TopMachine.Desktop.Overall;
using System.Drawing.Drawing2D;
using TopMachine.Training.FrontEnd;
using TopMachine.Desktop.Configuration;
using TopMachine.Topping.Frontend.Admin;

namespace TopMachine.Desktop
{
    public partial class Navigator : UserControl, IKeyHandler
    {
        ApplicationNavigation nav = null;

        TitledUserControl currentControl = null;
        Dictionary<string, TitledUserControl> controls = new Dictionary<string, TitledUserControl>();

        public Navigator()
        {
            InitializeComponent();
        }



        private void HideAll()
        {
            pnlControls.Controls.Clear();
        }

        public void LoadData()
        {
            nav = (ApplicationNavigation)PackageManager.Instance.Project.GetPackageItem("MainNavigation");

            foreach (ApplicationNavigation item in nav.Items.Where(p => p.Key == "UP").FirstOrDefault().Items)
            {
                Controls.ImageButton button = CreateButton(item);
                button.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
                button.Dock = DockStyle.Right;
                button.Tag = item;
                button.Click += new EventHandler(button_Click);
                panelUP.Controls.Add(button);
            }

            foreach (ApplicationNavigation item in nav.Items.Where(p => p.Key == "DOWN").FirstOrDefault().Items)
            {
                Controls.ImageButton button = CreateButton(item);
                button.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
                button.Dock = DockStyle.Left;
                button.Tag = item;
                button.Click += new EventHandler(button_Click);
                panelDOWN.Controls.Add(button);
            }

            try
            {
                Package pi = (Package)PackageManager.Instance.Project.GetPackageItem("TopMachineData\\ApplicationImages\\ChalkBoard");
                if (pi.Filename != null && pi.Filename.Length > 0)
                {
                    Stream s = PackageManager.Instance.Project.GetStream("TopMachineData\\ApplicationImages\\ChalkBoard");
                    Bitmap bmp = new Bitmap(s);
                    s.Dispose();
                    pnlControls.ShapeImage = bmp;
                    pnlControls.Refresh();
                }
            }
            catch
            {
                ;
            }

        }

        void button_Click(object sender, EventArgs e)
        {

            ApplicationNavigation nav = (ApplicationNavigation)((ImageButton)sender).Tag;

            switch (nav.Action)
            {
                case ApplicationNavigationAction.Administration:
                    AdminForm admineditor = new AdminForm();
                    admineditor.Initialize();
                    admineditor.ShowDialog(this);
                    break;
                case ApplicationNavigationAction.Settings:
                    ConfigEditor editor = new ConfigEditor();
                    editor.Load();
                    editor.ShowDialog(this);
                    break;
                case ApplicationNavigationAction.OpenControl:
                    TitledUserControl ctl = null;

                    if (controls.ContainsKey(nav.Key))
                    {
                        ctl = controls[nav.Key];
                    }
                    else
                    {
                        if (nav.TargetControl != null)
                        {
                            Package pack = (Package)PackageManager.Instance.Project.GetPackageItem(nav.TargetAssembly);
                            string typeName = nav.TargetControl + ", " + pack.Description;
                            Type t = Type.GetType(typeName, true);
                           // t = typeof(TrainingForm);
                            if (t != null)
                            {
                                Object o = t.GetConstructor(new Type[0]).Invoke(new object[0]);
                                if (nav.Action == ApplicationNavigationAction.OpenControl)
                                {
                                    ctl = (TitledUserControl)o;
                                    ctl.Collapsed += new CollapsedEvent(ctl_Collapsed);
                                    pnlControls.Controls.Clear();
                                    controls.Add(nav.Key, ctl);
                                }
                            }
                        }
                    }

                    if (ctl != null)
                    {
                        pnlControls.Controls.Clear();
                        currentControl = ctl;

                        pnlControls.Controls.Add(currentControl);
                        ctl.Dock = DockStyle.Fill;
                        currentControl.Left = pnlControls.Padding.Left;
                        currentControl.Top = pnlControls.Padding.Top;
                        pnlControls.Refresh();//.Invalidate(true);
                    }


                    break;
            }
        }


        private Controls.ImageButton CreateButton(ApplicationNavigation nav)
        {


            Controls.ImageButton button = new Controls.ImageButton();
            button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            button.BevelDepth = 1;
            button.CenterColor = System.Drawing.Color.Silver;
            button.Dome = true;
            button.FocusDrawn = false;


            if (nav.Image != null && nav.Image.Length > 0)
            {
                Stream s = PackageManager.Instance.Project.GetStream(nav.Image);
                Bitmap bmp = new Bitmap(s);
                s.Dispose();
                button.Image = bmp;
            }


            button.Location = new System.Drawing.Point(51, 2);
            button.Name = nav.Key;
            button.OverColor = System.Drawing.Color.Black;
            button.RecessDepth = 0;
            button.Round = 10;
            button.Size = new System.Drawing.Size(43, 43);
            button.TabIndex = 6;
            this.toolTip1.SetToolTip(button, nav.Key);
            button.UseVisualStyleBackColor = false;

            return button;
        }


        private void pnlControls_Resize(object sender, EventArgs e)
        {
            if (currentControl != null)
            {
                currentControl.Left = pnlControls.Padding.Left;
                currentControl.Top = pnlControls.Padding.Top;
            }
        }

        private bool collapsed = false;
        int hup = 0;
        int hdn = 0;

        bool ctl_Collapsed(TitledUserControl ctl)
        {
            this.SuspendLayout();
            if (collapsed == false)
            {
                hup = pnlStart.Height;
                hdn = panelDOWN.Height;

                pnlStart.Visible = false;
                pnlBottom.Visible = false;

            }
            else
            {
                pnlStart.Visible = true;
                pnlBottom.Visible = true;
            }

            this.ResumeLayout();
            this.Refresh();


            collapsed = !collapsed;

            return collapsed;

        }

        #region IKeyHandler Members

        public bool HandleKey(KeyEventArgs e)
        {
            if (currentControl is IKeyHandler)
            {
                ((IKeyHandler)currentControl).HandleKey(e);
            }

            return e.Handled;

        }

        #endregion

        private void pnlControls_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
