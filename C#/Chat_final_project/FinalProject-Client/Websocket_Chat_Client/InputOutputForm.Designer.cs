namespace Websocket_Chat_Client
{
    partial class InputOutputForm
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
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.uxTextbox = new System.Windows.Forms.RichTextBox();
            this.uxSendButton = new System.Windows.Forms.Button();
            this.uxChat = new System.Windows.Forms.ListBox();
            this.uxContactList = new System.Windows.Forms.ListBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxLoginStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myChatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.uxChatBox = new System.Windows.Forms.ListBox();
            this.uxContactsLabel = new System.Windows.Forms.Label();
            this.uxChatsLabel = new System.Windows.Forms.Label();
            this.uxCurrentChatLabel = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxTextbox
            // 
            this.uxTextbox.Enabled = false;
            this.uxTextbox.Location = new System.Drawing.Point(5, 404);
            this.uxTextbox.Margin = new System.Windows.Forms.Padding(2);
            this.uxTextbox.Name = "uxTextbox";
            this.uxTextbox.Size = new System.Drawing.Size(440, 64);
            this.uxTextbox.TabIndex = 0;
            this.uxTextbox.Text = "";
            // 
            // uxSendButton
            // 
            this.uxSendButton.Enabled = false;
            this.uxSendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.uxSendButton.Location = new System.Drawing.Point(448, 404);
            this.uxSendButton.Margin = new System.Windows.Forms.Padding(2);
            this.uxSendButton.Name = "uxSendButton";
            this.uxSendButton.Size = new System.Drawing.Size(78, 62);
            this.uxSendButton.TabIndex = 1;
            this.uxSendButton.Text = "Send";
            this.uxSendButton.UseVisualStyleBackColor = true;
            this.uxSendButton.Click += new System.EventHandler(this.uxSendButton_Click);
            // 
            // uxChat
            // 
            this.uxChat.Enabled = false;
            this.uxChat.FormattingEnabled = true;
            this.uxChat.Items.AddRange(new object[] {
            ""});
            this.uxChat.Location = new System.Drawing.Point(140, 55);
            this.uxChat.Margin = new System.Windows.Forms.Padding(2);
            this.uxChat.Name = "uxChat";
            this.uxChat.ScrollAlwaysVisible = true;
            this.uxChat.Size = new System.Drawing.Size(387, 342);
            this.uxChat.TabIndex = 2;
            // 
            // uxContactList
            // 
            this.uxContactList.Enabled = false;
            this.uxContactList.FormattingEnabled = true;
            this.uxContactList.Items.AddRange(new object[] {
            ""});
            this.uxContactList.Location = new System.Drawing.Point(5, 55);
            this.uxContactList.Margin = new System.Windows.Forms.Padding(2);
            this.uxContactList.Name = "uxContactList";
            this.uxContactList.ScrollAlwaysVisible = true;
            this.uxContactList.Size = new System.Drawing.Size(132, 186);
            this.uxContactList.TabIndex = 3;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.contactOptionsToolStripMenuItem,
            this.chatsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip.Size = new System.Drawing.Size(537, 24);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxLoginStrip,
            this.logoutToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // uxLoginStrip
            // 
            this.uxLoginStrip.Name = "uxLoginStrip";
            this.uxLoginStrip.Size = new System.Drawing.Size(112, 22);
            this.uxLoginStrip.Text = "Login";
            this.uxLoginStrip.Click += new System.EventHandler(this.uxLoginStrip_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // contactOptionsToolStripMenuItem
            // 
            this.contactOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAContactToolStripMenuItem,
            this.deleteAContactToolStripMenuItem});
            this.contactOptionsToolStripMenuItem.Name = "contactOptionsToolStripMenuItem";
            this.contactOptionsToolStripMenuItem.Size = new System.Drawing.Size(66, 22);
            this.contactOptionsToolStripMenuItem.Text = "Contacts";
            // 
            // addAContactToolStripMenuItem
            // 
            this.addAContactToolStripMenuItem.Name = "addAContactToolStripMenuItem";
            this.addAContactToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.addAContactToolStripMenuItem.Text = "Add a Contact";
            this.addAContactToolStripMenuItem.Click += new System.EventHandler(this.addAContactToolStripMenuItem_Click);
            // 
            // deleteAContactToolStripMenuItem
            // 
            this.deleteAContactToolStripMenuItem.Name = "deleteAContactToolStripMenuItem";
            this.deleteAContactToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deleteAContactToolStripMenuItem.Text = "Delete a contact";
            this.deleteAContactToolStripMenuItem.Click += new System.EventHandler(this.deleteAContactToolStripMenuItem_Click);
            // 
            // chatsToolStripMenuItem
            // 
            this.chatsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createChatToolStripMenuItem,
            this.myChatsToolStripMenuItem});
            this.chatsToolStripMenuItem.Name = "chatsToolStripMenuItem";
            this.chatsToolStripMenuItem.Size = new System.Drawing.Size(49, 22);
            this.chatsToolStripMenuItem.Text = "Chats";
            // 
            // createChatToolStripMenuItem
            // 
            this.createChatToolStripMenuItem.Name = "createChatToolStripMenuItem";
            this.createChatToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.createChatToolStripMenuItem.Text = "Create Chat";
            this.createChatToolStripMenuItem.Click += new System.EventHandler(this.createChatToolStripMenuItem_Click);
            // 
            // myChatsToolStripMenuItem
            // 
            this.myChatsToolStripMenuItem.Name = "myChatsToolStripMenuItem";
            this.myChatsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.myChatsToolStripMenuItem.Text = "Add Person To Chat";
            this.myChatsToolStripMenuItem.Click += new System.EventHandler(this.myChatsToolStripMenuItem_Click);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // uxChatBox
            // 
            this.uxChatBox.Enabled = false;
            this.uxChatBox.FormattingEnabled = true;
            this.uxChatBox.Items.AddRange(new object[] {
            ""});
            this.uxChatBox.Location = new System.Drawing.Point(4, 276);
            this.uxChatBox.Margin = new System.Windows.Forms.Padding(2);
            this.uxChatBox.Name = "uxChatBox";
            this.uxChatBox.ScrollAlwaysVisible = true;
            this.uxChatBox.Size = new System.Drawing.Size(132, 121);
            this.uxChatBox.TabIndex = 7;
            this.uxChatBox.SelectedValueChanged += new System.EventHandler(this.uxChatBox_SelectedValueChanged);
            // 
            // uxContactsLabel
            // 
            this.uxContactsLabel.AutoSize = true;
            this.uxContactsLabel.Location = new System.Drawing.Point(43, 32);
            this.uxContactsLabel.Name = "uxContactsLabel";
            this.uxContactsLabel.Size = new System.Drawing.Size(49, 13);
            this.uxContactsLabel.TabIndex = 8;
            this.uxContactsLabel.Text = "Contacts";
            // 
            // uxChatsLabel
            // 
            this.uxChatsLabel.AutoSize = true;
            this.uxChatsLabel.Location = new System.Drawing.Point(53, 250);
            this.uxChatsLabel.Name = "uxChatsLabel";
            this.uxChatsLabel.Size = new System.Drawing.Size(34, 13);
            this.uxChatsLabel.TabIndex = 9;
            this.uxChatsLabel.Text = "Chats";
            // 
            // uxCurrentChatLabel
            // 
            this.uxCurrentChatLabel.AutoSize = true;
            this.uxCurrentChatLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxCurrentChatLabel.Location = new System.Drawing.Point(232, 32);
            this.uxCurrentChatLabel.Name = "uxCurrentChatLabel";
            this.uxCurrentChatLabel.Size = new System.Drawing.Size(153, 16);
            this.uxCurrentChatLabel.TabIndex = 10;
            this.uxCurrentChatLabel.Text = "Current Chat: general";
            // 
            // InputOutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 480);
            this.Controls.Add(this.uxCurrentChatLabel);
            this.Controls.Add(this.uxChatsLabel);
            this.Controls.Add(this.uxContactsLabel);
            this.Controls.Add(this.uxChatBox);
            this.Controls.Add(this.uxContactList);
            this.Controls.Add(this.uxChat);
            this.Controls.Add(this.uxSendButton);
            this.Controls.Add(this.uxTextbox);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "InputOutputForm";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputOutputForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox uxTextbox;
        private System.Windows.Forms.Button uxSendButton;
        private System.Windows.Forms.ListBox uxChat;
        private System.Windows.Forms.ListBox uxContactList;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxLoginStrip;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contactOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAContactToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAContactToolStripMenuItem;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ToolStripMenuItem chatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createChatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myChatsToolStripMenuItem;
        private System.Windows.Forms.ListBox uxChatBox;
        private System.Windows.Forms.Label uxContactsLabel;
        private System.Windows.Forms.Label uxChatsLabel;
        private System.Windows.Forms.Label uxCurrentChatLabel;
    }
}

