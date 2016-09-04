using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Websocket_Chat_Client
{
    public class Model
    {
        //list of Chat contaning chats 
        private List<Chat> chats = new List<Chat>();

        //list of Account containing contacts
        private List<Account> contacts = new List<Account>();

        //string holding the current username
        private string Username;

        /// <summary>
        /// This method gets a list of chats
        /// </summary>
       public List<Chat> GetChat
        {
            get { return chats; }
        }
        /// <summary>
        /// This method gets the contacts lists
        /// </summary>
        public List<Account> GetContacts
        {
            get { return contacts; } 
        }
        /// <summary>
        /// This method gets the current usernaem
        /// </summary>
        public string GetUsername
        {
            get { return Username; }
            set { Username = value; }
        }

        public void ClearAllData()
        {
            chats.Clear();
            contacts.Clear();
            Username = "";
        }
    }
}
