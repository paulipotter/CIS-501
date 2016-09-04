using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Websocket_Chat_Client
{
    public class Chat
    {
        //field that stores the ID of the current chats
        private int ID;
        
        //list that contains the current users of the chats
        private List<Account> users;
        private List<string> messages;

        public Chat(int ID)
        {
            this.ID = ID;
            users = new List<Account>();
            messages = new List<string>();
        }

        /// <summary>
        /// Gets the current ID from the chat
        /// </summary>
        public int GetID
        {
            get { return ID; }
        }

        /// <summary>
        /// Gets the current list of users from the current chat
        /// </summary>
        public List<Account> GetUsers
        {
            get { return users; }
        }

        public List<string> GetMessages
        {
            get { return messages;  }
        }

        public override string ToString()
        {
            if (ID == 0)
            {
                return "general";
            }
            else
            {
                return /*"[" + users.Count + "] " +*/ "Chat #" +  ID.ToString();
            }
        }
    }
}
