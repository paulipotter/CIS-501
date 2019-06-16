using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    class Account : ISerializable
    {
        private List<Account> contacts;
        private bool isOnline;
        private string username;
        private string password;

        public List<Account> GetContacts
        {
            get
            {
                return contacts;
            }
        }

        public List<string> GetContactsAsString()
        {
            List<string> contactStrings = new List<string>();
            foreach (Account acct in contacts)
            {
                contactStrings.Add(acct.Username);
            }
            return contactStrings;
        }

        public bool IsOnline
        {
            get
            {
                return isOnline;
            }
            set
            {
                this.isOnline = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public void SetPassword(string password)
        {
            this.password = password;
        }

        public bool IsPassword(string guess)
        {
            return guess.Equals(password);
        }

        public Account()
        {
            // MUST BE DEFINED BY DESERIALIZATION CONSTRUCTOR
            contacts = null;
            isOnline = false;
            username = null;
            password = null;
        }

        public Account(string username, string password, params Account[] contacts)
        {
            this.username = username;
            this.password = password;
            this.contacts = contacts.ToList<Account>();
        }

        /// <summary>
        /// Deserialization Constructor, calls base constructor with name.
        /// </summary>
        /// <param name="info">The SerializationInfo created from the save file.</param>
        /// <param name="ctxt">The StreamingContext, this probably won't be needed.</param>
        public Account(SerializationInfo info, StreamingContext ctxt)
        {
            contacts = (List<Account>)info.GetValue("contacts", typeof(List<Account>));
            username = info.GetString("username");
            password = info.GetString("password");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("contacts", contacts);
            info.AddValue("username", username);
            info.AddValue("password", password);
        }
    }
}
