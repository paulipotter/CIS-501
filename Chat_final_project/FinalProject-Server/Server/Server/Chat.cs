using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Chat
    {
        private int ID;
        private List<Account> users;
        private List<string> messages;

        public Chat(int ID)
        {
            this.users = new List<Account>();
            this.messages = new List<string>();
            this.ID = ID;
        }

        public int GetID
        {
            get
            {
                return ID;
            }
        }

        public List<Account> GetUsers
        {
            get
            {
                return users;
            }
        }

        public List<string> GetMessages
        {
            get
            {
                return messages;
            }
        }
    }
}
