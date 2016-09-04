using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    class Model : ISerializable
    {
        private Dictionary<string, Account> accounts = new Dictionary<string, Account>();
        private Dictionary<int, Chat> chats = new Dictionary<int, Chat>();
        private Dictionary<Account, string> onlineAccounts = new Dictionary<Account, string>(); // string is IWebSocketSession

        public Model()
        {
            //accounts = new Dictionary<string, Account>();
            //chats = new Dictionary<int, Chat>();
            //onlineAccounts = new Dictionary<Account, string>();
        }

        public Dictionary<String, Account> GetAccounts
        {
            get
            {
                return accounts;
            }
        }

        public Dictionary<int, Chat> GetChats
        {
            get
            {
                return chats;
            }
        }

        
        public Dictionary<Account, string> GetOnlineAccounts
        {
            get
            {
                return onlineAccounts;
            }
        }
        

        /// <summary>
        /// Deserialization Constructor, calls base constructor with name.
        /// </summary>
        /// <param name="info">The SerializationInfo created from the save file.</param>
        /// <param name="ctxt">The StreamingContext, this probably won't be needed.</param>
        public Model(SerializationInfo info, StreamingContext ctxt)
        {
            accounts = (Dictionary<String, Account>)info.GetValue("accounts", typeof(Dictionary<String, Account>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("accounts", accounts);
        }
    }
}