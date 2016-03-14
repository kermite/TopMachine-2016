using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TopMachine.Desktop.Overall;

namespace TopMachine.Topping.Frontend.Controls
{
    public partial class FriendsList : UserControl
    {

        public static FriendsList Instance
        {
            get;
            set; 
        }

        Guid MemoryCheckId;

        public FriendsList()
        {
            MemoryCheckId = MemoryCheck.Register(this, this.GetType().FullName);
            Instance = this; 
            InitializeComponent();
        }


        public void LoadFriends()
        {
            listFriends.Items.Clear();

            List<string> friends = new List<string>();

            foreach (string s in Properties.Settings.Default.MyFriends)
            {
                friends.Add(s); 
            }

            friends.Sort(); 

            foreach (string s in Properties.Settings.Default.MyFriends)
            {
                listFriends.Items.Add(new ListViewItem(s));
            }

            if (listFriends.Items.Count == 0)
            {
                listFriends.Items.Add(new ListViewItem("Vous n'avez pas d'amis"));

            }

            listFriends.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }



        public int lastX = 0;
        int lastY = 0;
        private void nouvelAmiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string friend = listFriends.GetItemAt(lastX, lastY).Text;

            if (!Properties.Settings.Default.MyFriends.Contains(friend))
            {
                Properties.Settings.Default.MyFriends.Add(friend);
                Properties.Settings.Default.Save();
            }
            LoadFriends();

        }

        private void listFriends_MouseClick(object sender, MouseEventArgs e)
        {
            lastX = e.X;
            lastY = e.Y;
        }

        private void jeLaimePlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string friend = listFriends.GetItemAt(lastX, lastY).Text;

            if (Properties.Settings.Default.MyFriends.Contains(friend))
            {
                Properties.Settings.Default.MyFriends.Remove(friend);
                Properties.Settings.Default.Save();
            }
            LoadFriends();
        }
    }
}
