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
    public partial class ListPlayers : UserControl
    {

        public FriendsList friendsList { get; set; }

        Guid MemoryCheckId; 

        public ListPlayers()
        {
            MemoryCheckId = MemoryCheck.Register(this, this.GetType().FullName);
            InitializeComponent();
        }

        public void refreshList(string[] lstplayer) 
        {
            listViewPlayers.Items.Clear();
            
            foreach (string item in lstplayer)
            {
             ListViewItem lvi = new ListViewItem();
             lvi.Text = item;
             listViewPlayers.Items.Add(lvi);      
            }
            listViewPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }


        private void nouvelAmiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string friend = listViewPlayers.GetItemAt(lastX, lastY).Text;

            if (!Properties.Settings.Default.MyFriends.Contains(friend))
            {
                Properties.Settings.Default.MyFriends.Add(friend);
                Properties.Settings.Default.Save();
            }

            //if (FriendsList.Instance != null)
            //{
            //    FriendsList.Instance.LoadFriends();
            //}

        }


        private void jeLaimePlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string friend = listViewPlayers.GetItemAt(lastX, lastY).Text;

            if (Properties.Settings.Default.MyFriends.Contains(friend))
            {
                Properties.Settings.Default.MyFriends.Remove(friend);
                Properties.Settings.Default.Save();
            }

            if (FriendsList.Instance != null)
            {
                FriendsList.Instance.LoadFriends();
            }
        }

        public int lastX = 0;
        int lastY = 0;
        private void listViewPlayers_MouseClick(object sender, MouseEventArgs e)
        {
            lastX = e.X;
            lastY = e.Y;
        }
    
    }

   
}

