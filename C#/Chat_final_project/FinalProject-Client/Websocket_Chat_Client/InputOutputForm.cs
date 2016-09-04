using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientServerLibrary;
using System.Threading;
using Microsoft.VisualBasic;

namespace Websocket_Chat_Client
{
    public partial class InputOutputForm : Form
    {
        
        IServerAPI api;
        Model m;

        private int selectedChat = 0;

        public InputOutputForm(Model m, IServerAPI api)
        {
            InitializeComponent();
            this.m = m;
            this.api = api;
        }

        private bool requestUpdate = false;
        public void OnModelUpdate(object sender, EventArgs args)
        {
            requestUpdate = true; // a thread safe way of getting updates via observer..
        }

        private void UpdateModelThreadSafe()
        {
            // enable all forms
            uxEnableAllButtons(true);

            // update title
            this.Text = "Chat (logged in as '" + m.GetUsername + "')";

            // update contact list
            uxContactList.Items.Clear();
            if (m.GetContacts.Count > 0)
            {
                foreach (Account friend in m.GetContacts)
                {
                    uxContactList.Items.Add(friend);
                }
            }

            // update uxChat
            uxChat.Items.Clear();
            if (m.GetChat.Count > 0)
            {
                foreach (string message in m.GetChat.Find(c => c.GetID == selectedChat).GetMessages)
                {
                    uxChat.Items.Add(message);
                }
            }

            // update chats
            uxChatBox.Items.Clear();
            foreach (Chat chat in m.GetChat)
            {
                uxChatBox.Items.Add(chat);
            }
        }


        /// <summary>
        /// send button should send a messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxSendButton_Click(object sender, EventArgs e)
        {
            if (uxTextbox.TextLength == 0)
                return;
            api.SendMessageToChat(uxTextbox.Text, selectedChat);
            uxTextbox.Clear();
        }

        /// <summary>
        /// adding a contact event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addAContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string contact = Interaction.InputBox("Contact name?");
            api.AddContact(contact);
        }

        /// <summary>
        /// deleting a contact ecent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteAContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string contact = Interaction.InputBox("Contact name?");
            if (contact.Trim().Length == 0)
            {
                MessageBox.Show("Invalid name. Please try again.");
                return;
            }
            api.DeleteContact(contact);
        }

        /// <summary>
        /// logging out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uxLogout();
        }
        /// <summary>
        /// logging in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxLoginStrip_Click(object sender, EventArgs e)
        {
            string user = Interaction.InputBox("Username?", "Login [1 of 3]");
            string pw1 = Interaction.InputBox("Password", "Login [2 of 3]");
            string pw2 = Interaction.InputBox("Confirm Password.", "Login [3 of 3]");
            if (!pw1.Equals(pw2))
            {
                MessageBox.Show("Passwords did not match! Please try again.");
            }
            else
            {
                if (user.Trim().Length == 0 || pw1.Trim().Length == 0)
                {
                    MessageBox.Show("Invalid username or password. Please try again.");
                    return;
                }
                api.LogIn(user, pw1);
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (requestUpdate)
            {
                UpdateModelThreadSafe();
            }
            requestUpdate = false;
        }

        private void createChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m.GetContacts.Count == 0)
            {
                MessageBox.Show("You need a friend to open a chat!");
            }
            else
            {
                string user = Interaction.InputBox("Who would you like to start a chat with?", "Start chat");
                api.OpenChat(user);
            }
        }

        private void InputOutputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            uxLogout();
        }

        private void uxLogout()
        {
            api.LogOut(); // send logout on close
            m.ClearAllData(); // clear model
            uxEnableAllButtons(false); // disable all main buttons
            requestUpdate = true; // request update
        }

        private void uxEnableAllButtons(bool enabled)
        {
            uxChat.Enabled = enabled;
            uxContactList.Enabled = enabled;
            uxSendButton.Enabled = enabled;
            uxTextbox.Enabled = enabled;
            uxChatBox.Enabled = enabled;
        }

        private void uxChatBox_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
            if (uxChat.SelectedItem != null)
            {
                selectedChat = ((Chat)uxChatBox.SelectedItem).GetID;
            }
            else
            {
                selectedChat = 0; // where 0 is general chat
            }
            */
            try
            {
                selectedChat = ((Chat)uxChatBox.SelectedItem).GetID;
                uxCurrentChatLabel.Text = "Current Chat: " + ((Chat)uxChatBox.SelectedItem).ToString();
                requestUpdate = true;
            }
            catch (Exception ex)
            {
                selectedChat = 0; // where 0 is general chat
                uxCurrentChatLabel.Text = "Current Chat: general";
            }
        }

        private void myChatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string user = Interaction.InputBox("Who would you like to add to a chat?", "Add user to chat [1 of 2]");
            string chat = Interaction.InputBox("What chat (give ID) would you like to add them to?", "Add user to chat [2 of 2]");
            if (user.Trim().Length != 0 && chat.Trim().Length != 0)
            {
                try
                {
                    int chatID = Int32.Parse(chat);
                    api.AddChat(user, chatID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid chat ID");
                }
            }
            else
            {
                MessageBox.Show("Invalid username or chat ID");
            }
        }
    }
}
