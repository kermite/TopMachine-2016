using Topping.Core.Proxy.Local.Client;
using TopMachine.Desktop.Overall;

namespace TopMachine.Topping.Frontend.Chat
{
    partial class ChatControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();

                try
                {
                    Deactivate();
                }
                catch { ; }
            }

            MemoryCheck.Unregister(MemoryCheckId);
            if (MessageProxy.Proxy.Session != null)
            {
                MessageProxy.Proxy.Session.RemoveMessageHandler("Chat");
            } base.Dispose(disposing);

        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlChat = new System.Windows.Forms.WebBrowser();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtMessage);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 385);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(5);
			this.panel1.Size = new System.Drawing.Size(440, 58);
			this.panel1.TabIndex = 1;
			// 
			// txtMessage
			// 
			this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMessage.Location = new System.Drawing.Point(5, 28);
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(430, 24);
			this.txtMessage.TabIndex = 1;
			this.txtMessage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtMessage_MouseClick);
			this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
			this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress);
			this.txtMessage.Leave += new System.EventHandler(this.txtMessage_Leave);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(5, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(430, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "  Votre Message";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlChat
			// 
			this.pnlChat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlChat.Location = new System.Drawing.Point(0, 0);
			this.pnlChat.MinimumSize = new System.Drawing.Size(20, 20);
			this.pnlChat.Name = "pnlChat";
			this.pnlChat.Size = new System.Drawing.Size(440, 385);
			this.pnlChat.TabIndex = 2;
			this.pnlChat.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.pnlChat_DocumentCompleted);
			// 
			// ChatControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlChat);
			this.Controls.Add(this.panel1);
			this.Name = "ChatControl";
			this.Size = new System.Drawing.Size(440, 443);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser pnlChat;
    }
}
