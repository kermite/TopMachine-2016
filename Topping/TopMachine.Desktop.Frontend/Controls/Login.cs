using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TopMachine.Desktop.Controls.Sound;
using TopMachine.Desktop.Overall;
using Topping.Core.Proxy.Local.Client;
using TopMachine.Topping.Dto;

namespace TopMachine.Topping.Frontend.Controls
{
    public partial class Login : UserControl
    {
        public delegate bool LoginRequest(Control sender, bool cancel, string user, string password);
        public event LoginRequest OnLoginRequest;

        public bool IsLocal { get; set; }

        Guid MemoryCheckId;
        public Login()
        {
            MemoryCheckId = MemoryCheck.Register(this, this.GetType().FullName);
            InitializeComponent();
        }

        

        private void Login_Load(object sender, EventArgs e)
        {
            if (IsLocal)
            {
                txtMotDePasse.Enabled = false;
                txtPseudo.Text = Properties.Settings.Default.LocalUser;
                if (string.IsNullOrEmpty(txtPseudo.Text)) 
                {
                    txtPseudo.Text = "TopMachine";
                }
               // this.Dispose();
             //  OnLoginRequest(this, true, txtPseudo.Text, txtMotDePasse.Text);
            }
            else
            {
                txtPseudo.Text = Properties.Settings.Default.OnlineUser;
                txtMotDePasse.Text = Properties.Settings.Default.OnlinePassword;

            }
        }
              

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (OnLoginRequest != null)
            {
                OnLoginRequest(this, true, txtPseudo.Text, txtMotDePasse.Text);
                this.Dispose();
            }

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (OnLoginRequest != null)
            {
                if (!OnLoginRequest(this, false, txtPseudo.Text.ToLower(), txtMotDePasse.Text))
                {
                    txtPseudo.BackColor = Color.Orange;
                    txtMotDePasse.BackColor = Color.Orange;
                }
                else
                {
                    if (IsLocal)
                    {
                        Properties.Settings.Default.LocalUser = txtPseudo.Text;
                    }
                    else
                    {
                        Properties.Settings.Default.OnlineUser = txtPseudo.Text;
                        Properties.Settings.Default.OnlinePassword = txtMotDePasse.Text;
                        Sound.play(Sound.SoundType.LOGIN);

						MessageDto m = new MessageDto();
						m.From = txtPseudo.Text;
						m.Text = "[" +DateTime.Now + "]" + " je suis connecté";
						m.MessageModule = MessageModule.Chat;
						m.MessageType = MessageType.NoType;
						MessageProxy.Proxy.SendMessage(m);

					}

                    Properties.Settings.Default.Save();
                    this.Dispose();
                }


            }
        }
    }
}
