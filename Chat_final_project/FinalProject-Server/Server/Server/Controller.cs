using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServerLibrary;
using System.IO;
using Newtonsoft.Json;

namespace Server
{
    class Controller : PacketHandler
    {
        private Model m;
        private static int nextChatID = 1;
        public event EventHandler<String> ModelUpdated;

        public Controller(Model m)
        {
            this.m = m;
        }

            
        public void handleLoginPacket(string packet, IClient client, Dictionary<string, string> tempAccounts)
        {
            string[] args = JsonConvert.DeserializeObject<string[]>(packet);

            if (args.Length < 3)
            {
                return;
            }

            string sender = args[2];
            string username = args[3];
            string password = args[4];

            /*
            MAKING AN ACCOUNT
            */
            Account newGuy = null;
            if (!m.GetAccounts.TryGetValue(username, out newGuy))
            {
                // if we reach this block then we are makign a new account with this unique username
                m.GetAccounts.Add(username, new Account(username, password));
                Account newAccount = m.GetAccounts[username];
                m.GetOnlineAccounts.Add(newAccount, tempAccounts[sender]);
                //newAccount.IsOnline = true;
                //client.SendMessage(tempAccounts[sender], -1, username); // make them this new account
                //return;
            }
            else // if here then we log into an existing account
            {
                try {
                    newGuy = m.GetAccounts[username];
                    m.GetOnlineAccounts.Add(newGuy, tempAccounts[sender]);
                }
                catch (Exception ex) // triggers if attempt to add dupe name
                {
                    client.SendLoginReply(tempAccounts[sender], false);
                }
            }

            newGuy = m.GetAccounts[username];
            newGuy.IsOnline = true;

            /*
            LOGGING INTO AN ACCOUNT
            */

            // see if login works
            bool didLogin = newGuy.IsPassword(password);

            // if you didn't let them know
            if (!didLogin)
            {
                client.SendLoginReply(tempAccounts[sender], false);
            }
            else
            {
                client.SendMessage(tempAccounts[sender], -1, username); // make them the new person
                // reflect this change in the model
                newGuy.IsOnline = true;
                //adds you to global chat channnel
                m.GetChats[0].GetUsers.Add(newGuy);

                client.SendChatID(m.GetOnlineAccounts[newGuy], 0);

                // send the newly online account their friends list
                client.SendContactList(m.GetOnlineAccounts[newGuy], newGuy.GetContactsAsString());

                // notify all the friends that person x is online
                foreach (Account contact in newGuy.GetContacts)
                {
                    if (contact.IsOnline)
                    {
                        // notify friend that newGuy is online
                        string key;
                        if (m.GetOnlineAccounts.TryGetValue(contact, out key))
                        {
                            client.SendUserStatus(key, username, true);
                        }
                    }

                    // notify newGuy of their friend's status
                    client.SendUserStatus(m.GetOnlineAccounts[newGuy], contact.Username, contact.IsOnline);
                }
            }

            ModelUpdated?.Invoke(this, packet);
        }
        
        public void handleLogoutPacket(string packet, IClient client, Dictionary<string, string> tempAccounts, string nextTempAccount)
        {
            string[] args = JsonConvert.DeserializeObject<string[]>(packet);

            if (args.Length < 3)
            {
                return;
            }

            string username = args[2];

            // check account is logged in & mapped
            Account account = null;
            if (m.GetAccounts.TryGetValue(username, out account)) // exists?
            {
                string value;
                if (m.GetOnlineAccounts.TryGetValue(account, out value)) // & online
                {
                    // if here we can clean model for logged out person
                    m.GetChats[0].GetUsers.Remove(account);
                    // next two lines reverse-map the id back to a temp key for awaiting login
                    tempAccounts.Add(nextTempAccount, m.GetOnlineAccounts[account]);
                    client.SendMessage(m.GetOnlineAccounts[account], -1, nextTempAccount);

                    m.GetOnlineAccounts.Remove(account); // remove from online
                    account.IsOnline = false; // set offline

                    // notify all friends of offline status
                    foreach (Account contact in account.GetContacts)
                    {
                        if (contact.IsOnline)
                        {
                            string key;
                            if (m.GetOnlineAccounts.TryGetValue(contact, out key))
                            {
                                client.SendUserStatus(key, username, false);
                            }
                        }
                    }

                    // announce offline status to all chats this user was in 
                    foreach (KeyValuePair<int, Chat> pair in m.GetChats.ToList())
                    {
                        
                        Chat chat = pair.Value;
                        if (chat.GetUsers.Contains(account)) // if user is in this chat
                        {
                            try
                            {
                                chat.GetUsers.Remove(account);
                                foreach (Account user in chat.GetUsers) // notify all members of chat
                                {
                                    string userID;
                                    if (m.GetOnlineAccounts.TryGetValue(user, out userID)) // make sure they are online
                                    {
                                        client.SendMessage(userID, chat.GetID, account.Username + " has left the chat. # of people in this room: " + chat.GetUsers.Count.ToString());
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                // could not remove user as he doesn't exist..
                                continue;
                            }
                        }
                    }
                }
            }

            ModelUpdated?.Invoke(this, packet);

        }

        public void handleQueryPacket(string packet, IClient client)
        {
            bool didModelChange = false;

            string[] args = JsonConvert.DeserializeObject<string[]>(packet);

            if (args.Length < 3)
            {
                return;
            }

            if (args[0].Equals("C_AddChat"))
            {
                Console.WriteLine("called: C_AddChat");

                //string sender = args[2];
                string personToAdd = args[3];
                int chatID = Int32.Parse(args[4]);

                Account sender = m.GetAccounts[args[2]];

                // friends list check
                if (!sender.GetContacts.Exists(f => f.Username.Equals(personToAdd)))
                {
                    client.SendError(m.GetOnlineAccounts[sender], "person is not in your friends list");
                    return;
                }
                try
                {
                    // not already in chat check
                    if (m.GetChats[chatID].GetUsers.Exists(f => f.Username.Equals(personToAdd)))
                    {
                        client.SendError(m.GetOnlineAccounts[sender], "this user is already in the chat");
                        return;
                    }
                
                    // if both pass add the person and notify clients
                    string newPersonID = m.GetOnlineAccounts[m.GetAccounts[personToAdd]];
                    client.SendChatID(newPersonID, chatID); // notify new person

                    // then send that person all old messages
                    foreach (string message in m.GetChats[chatID].GetMessages)
                    {
                        client.SendMessage(newPersonID, chatID, message);
                    }

                    m.GetChats[chatID].GetUsers.Add(m.GetAccounts[personToAdd]); // update the model
                    didModelChange = true;

                    // then notify other members of the chat that a new person joined..
                    foreach (Account person in m.GetChats[chatID].GetUsers)
                    {
                        string userID;
                        if (m.GetOnlineAccounts.TryGetValue(person, out userID)) // make sure they are online
                        {
                            client.SendMessage(userID, chatID, personToAdd + " has joined the chat. # of people in this room: " + m.GetChats[chatID].GetUsers.Count.ToString());
                        }
                    }
                }
                catch (Exception ex) // catch all, namely a KeyNotFoundException
                {
                    client.SendError(m.GetOnlineAccounts[sender], "could not add user to chat (does this chat exist?)");
                }


            }
            else if (args[0].Equals("C_AddContact"))
            {
                Console.WriteLine("called: C_AddContact");

                Account sender = m.GetAccounts[args[2]];
                string newContact = args[3];

                // 1st make sure name is a valid contact & not the sender's name
                if (sender.Username.Equals(newContact))
                {
                    client.SendError(m.GetOnlineAccounts[sender], "you can not add yourself, silly");
                    return;
                }

                Account newContactAcct = null;
                if (!m.GetAccounts.TryGetValue(newContact, out newContactAcct))
                {
                    client.SendError(m.GetOnlineAccounts[sender], "that person does not exist");
                    return;
                }

                // check they are not already on your contact list
                if (sender.GetContacts.Contains(newContactAcct))
                {
                    client.SendError(m.GetOnlineAccounts[sender], "this person is already a friend");
                    return;
                }

                sender.GetContacts.Add(newContactAcct); // update model
                newContactAcct.GetContacts.Add(sender); // update model
                didModelChange = true;

                client.SendNewContact(m.GetOnlineAccounts[sender], newContact); // tell the sender
                client.SendNewContact(m.GetOnlineAccounts[newContactAcct], sender.Username); // tell the new friend

                client.SendUserStatus(m.GetOnlineAccounts[sender], newContact, newContactAcct.IsOnline); // should be true
                client.SendUserStatus(m.GetOnlineAccounts[newContactAcct], sender.Username, sender.IsOnline); // should be true

            }
            else if (args[0].Equals("C_DeleteContact"))
            {
                Console.WriteLine("called: C_DeleteContact");

                Account sender = m.GetAccounts[args[2]];
                string contact = args[3]; // the person to remove

                // make sure not the sender's name
                if (sender.Username.Equals(contact))
                {
                    client.SendError(m.GetOnlineAccounts[sender], "you can not delete yourself, silly");
                    return;
                }

                // make sure person exists
                Account rmContactAcct = null;
                if (!m.GetAccounts.TryGetValue(contact, out rmContactAcct))
                {
                    client.SendError(m.GetOnlineAccounts[sender], "that person does not exist");
                    return;
                }

                // check they are on your contact list
                if (!sender.GetContacts.Contains(rmContactAcct))
                {
                    client.SendError(m.GetOnlineAccounts[sender], "you are not friends with this person");
                    return;
                }

                // if all past checks passed, then we can remove the contacts (mutually)
                // 1st in the model

                // then notify both clients
                sender.GetContacts.Remove(rmContactAcct); // update model
                rmContactAcct.GetContacts.Remove(sender);
                didModelChange = true;

                client.DeleteContact(m.GetOnlineAccounts[sender], rmContactAcct.Username);
                client.DeleteContact(m.GetOnlineAccounts[rmContactAcct], sender.Username);

            }
            else if (args[0].Equals("C_LogIn"))
            {
                throw new ArgumentException(); // handled by special delegate
            }
            else if (args[0].Equals("C_LogOut"))
            {
                throw new ArgumentException(); // handled by special delegate
            }
            else if (args[0].Equals("C_OpenChat"))
            {
                Console.WriteLine("called: C_OpenChat");

                Account sender = null;
                if (m.GetAccounts.TryGetValue(args[2], out sender))
                {
                    string senderID = m.GetOnlineAccounts[sender];
                    Account personToAdd = null;
                    if (m.GetAccounts.TryGetValue(args[3], out personToAdd))
                    {
                        try
                        {
                            string personToAddID = m.GetOnlineAccounts[personToAdd];
                            if (sender.GetContacts.Contains(personToAdd))
                            {
                                // add person in model
                                Chat chat = new Chat(nextChatID);
                                chat.GetUsers.Add(sender);
                                chat.GetUsers.Add(personToAdd);
                                m.GetChats.Add(nextChatID, chat);
                                didModelChange = true;

                                // notify both people that they are members of this new chat
                                client.SendChatID(senderID, nextChatID);
                                client.SendChatID(personToAddID, nextChatID);

                                client.SendMessage(senderID, nextChatID, personToAdd.Username + " has joined the chat. # of people in this room: 2");
                                client.SendMessage(personToAddID, nextChatID, personToAdd.Username + " has joined the chat. # of people in this room: 2");

                                nextChatID++;
                            }
                            else
                            {
                                client.SendError(senderID, "You cannot add a person who is not on your friends list");
                            }
                        }
                        catch (Exception ex) // exception means person is not online or can't be found
                        {
                            client.SendError(senderID, "Unable to start a chat with that person at this time (are the online?)");
                        }
                        
                    }
                    else
                    {
                        client.SendError(senderID, "could not find that person, please try again");
                    }
                }
            }
            else if (args[0].Equals("C_RequestContactList"))
            {
                Console.WriteLine("called: C_RequestContactList");

                Account sender = null;
                if (m.GetAccounts.TryGetValue(args[2], out sender)) // if sender is valid
                {
                    string onlineID;
                    if (m.GetOnlineAccounts.TryGetValue(sender, out onlineID)) // if sender is online / mapped
                    {
                        client.SendContactList(onlineID, sender.GetContactsAsString());
                    }
                }

            }
            else if (args[0].Equals("C_RequestContactStatus"))
            {
                Console.WriteLine("called: C_RequestContactStatus");

                Account sender = null;
                if (m.GetAccounts.TryGetValue(args[2], out sender)) // if sender is valid
                {
                    string onlineID;
                    if (m.GetOnlineAccounts.TryGetValue(sender, out onlineID)) // if sender is online / mapped
                    {
                        Account accountToCheck = null;
                        if (m.GetAccounts.TryGetValue(args[3], out accountToCheck)) // if account to check is valid
                        {
                            if (sender.GetContacts.Contains(accountToCheck)) // if both people are friends
                            {
                                client.SendUserStatus(onlineID, accountToCheck.Username, accountToCheck.IsOnline);
                            }
                        }
                    }
                }

            }
            else if (args[0].Equals("C_SendMessageToChat"))
            {
                Console.WriteLine("called: C_SendMessageToChat");

                string message = args[3];
                int chatID = Int32.Parse(args[4]);

                Account sender = null;
                if (m.GetAccounts.TryGetValue(args[2], out sender)) // if sender is valid
                {
                    Chat chat = null;
                    if (m.GetChats.TryGetValue(chatID, out chat)) // if chat exists
                    {
                        if (chat.GetUsers.Contains(sender)) // if sender is a member of said chat
                        {
                            // update model, then send relay message to all people in said chat
                            chat.GetMessages.Add(message);
                            didModelChange = true;

                            foreach (Account account in chat.GetUsers) // notify all members of chat
                            {
                                string onlineKey;
                                if (m.GetOnlineAccounts.TryGetValue(account, out onlineKey))
                                {
                                    client.SendMessage(onlineKey, chatID, message);
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                return;
            }

            Console.WriteLine(">>> didModelChange? " + didModelChange);
            // TEST OBSERVER
            if (didModelChange)
            {
                ModelUpdated?.Invoke(this, packet);
            }
            
        }
    }
}
