using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TopMachine.Topping.Dto;
using Topping.Core.Proxy.Local.Client;
using Topping.Core.Logic.Client;
using TopMachine.Desktop.Controls.Sound;
using System.Web;
using TopMachine.Desktop.Overall;

namespace TopMachine.Topping.Frontend.Chat
{
    public delegate void ChatEvent(bool activate);

    public partial class ChatControl : UserControl, IMessageHandler
    {
        public event ChatEvent onChatActivate;

        public String[] colors = new String[2] { "Text", "AlternateText" };
        public int currentCol = 0;

        Guid MemoryCheckId;

        public ChatControl()
        {
            InitializeComponent();
            MemoryCheckId = MemoryCheck.Register(this, "Chat Control");
            pnlChat.DocumentText = TopMachine.Topping.Frontend.Properties.Resources.DefaultHTML; 
        }

        bool inRoom = false;
        public void Activate(bool inRoom)
        {
            this.inRoom = inRoom;
            MessageProxy.Proxy.Session.AddMessageHandler("Chat", this, true);
        }

        public void Deactivate()
        {
            if (MessageProxy.Proxy.Session != null)
            {
                MessageProxy.Proxy.Session.RemoveMessageHandler("Chat");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
                e.Handled = true;
            }

            e.Handled = true;
        }

        DateTime lastMessage = DateTime.Now;
        private bool CheckTime()
        {
            if (DateTime.Now.Subtract(lastMessage).TotalSeconds < 3)
            {
                MessageBox.Show("Vous pouvez seulement envoyer un message toutes les 10 secondes. Cliquez sur OK", "Erreur", MessageBoxButtons.OK);
                return false;
            }

            lastMessage = DateTime.Now;
            return true;
        }

        private void SendMessage()
        {

            if (txtMessage.Text.Trim().Length == 0) return;
            // avoid to clear msg if checktime return false
            bool ok = true;
            MessageDto m = new MessageDto();
            m.From = MessageProxy.Proxy.Session.Pseudo;

            string msg = txtMessage.Text.Trim();
            if (msg.ToUpper().StartsWith("/"))
            {
                msg = msg.Substring(1);
                string[] t = msg.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                switch (t[0].ToUpper())
                {
                    case "S":
                        if (t.Length > 2)
                        {
                            m.To = t[1].ToLower();
                            m.Text = String.Join(" ", t, 2, t.Length - 2);
							m.Text = "[" + DateTime.Now + "]" + " " + m.Text;
                            m.MessageType = MessageType.Private;
                            m.MessageModule = MessageModule.Chat;
                            ok = CheckTime(); 
                            if (ok)
                            {
                                MessageProxy.Proxy.SendMessage(m);
                            }
                        }
                        break;
                    case "A":
                        MessageProxy.Proxy.ActivateChat();
                        break;
                    case "D":
                        MessageProxy.Proxy.DeactivateChat();
                        break;
                    case "H":
                        LoadHelp();
                        break;
                    case "CLEAR":
                        pnlChat.DocumentText = TopMachine.Topping.Frontend.Properties.Resources.DefaultHTML; 
                        break;
                }
            }
            else
            {
                if (inRoom)
                {
                    m.MessageType = MessageType.Public;
                    m.Text = msg;
					m.Text = "[" + DateTime.Now + "]" + " " + m.Text;
					m.ToRoom = MessageProxy.Proxy.Session.CurrentRoom.RoomId;
                    ok = CheckTime(); 
                    if (ok)
                    {
                        MessageProxy.Proxy.SendMessageToRoom(m.ToRoom, m);
                    }
                }
                else
                {
                    m.MessageType = MessageType.Public;
                    m.ToRoom = -1;
                    m.Text = msg;
					m.Text = "[" + DateTime.Now + "]" + " " + m.Text;
					ok = CheckTime(); 
                    if (ok)
                    {
                        MessageProxy.Proxy.SendMessage(m);
                    }
                }
            }
            if (ok)
            {
                txtMessage.Text = "";
            }

        }

        private void LoadHelp()
        {
            string text = "/H [Enter]: Affiche cette aide\r\n" +
                "<BR/>/A [Enter]: pour activer le chat\r\n" +
                "<BR/>/D [Enter]: pour désactiver le chat\r\n" +
                "<BR/>/S [Pseudo] [Message] [Enter]: pour envoyer un message privé\r\n" +
                "<BR/>[Message] [Enter] : pour envoyer un message public.\r\n" +
                "<BR/>/CLEAR [Enter] : Efface la fenêtre";
            CreateBlockHTML("system", text); 
        }

        private void CreateBlockHTML(string className, string text)
        {
            HtmlElement el = pnlChat.Document.CreateElement("div");

            el.InnerHtml = "<div class='" + className + "'>" + text + "</div>";
            try
            {
                pnlChat.Document.Body.AppendChild(el);
                pnlChat.Document.Body.ScrollIntoView(false);
            }
            catch { ; }
        }

        private void CreateBlock(string className, string text)
        {
            HtmlElement el = pnlChat.Document.CreateElement("div");

            el.InnerHtml = "<div class='" + className + "'>" + HttpUtility.HtmlEncode(text) + "</div>";
            try
            {
                pnlChat.Document.Body.AppendChild(el);
                pnlChat.Document.Body.ScrollIntoView(false);
            }
            catch { ;}
        }


        delegate bool HandleMessageCallbackDelegate(MessageDto m);

        public bool HandleMessage(MessageDto m)
        {
   /*         return (bool) this.Invoke(new HandleMessageCallbackDelegate(HandleMessageSafe), new object[] { m });
        }
           
        public bool HandleMessageSafe(MessageDto m)
        {*/
            string className = "";
            string text = ""; 
            bool addBlock = false; 
            switch (m.MessageType)
            {
				case MessageType.NoType:
						text = m.From + " : " + m.Text;
						className = "system";
						currentCol = currentCol == 0 ? 1 : 0;
						addBlock = true;
					
					break;
                case MessageType.Public:
                    if (!inRoom)
                    {
                        text = m.From + " : " + m.Text;
                        className = colors[currentCol];
                        currentCol = currentCol == 0 ? 1 : 0; 
                        addBlock = true;
                    }
                    else
                    {
                        if (m.ToRoom == MessageProxy.Proxy.Session.CurrentRoom.RoomId)
                        {
                            text = m.From + " : " + m.Text;
                            className  = colors[currentCol];
                            currentCol = currentCol == 0 ? 1 : 0;
                            addBlock = true;
                        }
                    }
                    break;
                case MessageType.Private:
                    if (m.MessageModule == MessageModule.Chat)
                    {
                        Sound.play(Sound.SoundType.CHAT);
                        text = "[" + m.From + "] : " + m.Text;
                        className  = "Private";
                        addBlock = true;
                    }
                    break;
            }

            if (addBlock)
            {
                CreateBlock(className, text);
            }

            return addBlock;
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '\r':
                    // perform necessary action
                    e.Handled = true;
                    break;
            }

        }

        private void txtMessage_MouseClick(object sender, MouseEventArgs e)
        {
            if (onChatActivate != null)
            {
                onChatActivate(false);
            }
        }

        private void txtMessage_Leave(object sender, EventArgs e)
        {
            if (onChatActivate != null)
            {
                onChatActivate(true);
            }

        }

        private void pnlChat_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            LoadHelp();
        }

    }
}
