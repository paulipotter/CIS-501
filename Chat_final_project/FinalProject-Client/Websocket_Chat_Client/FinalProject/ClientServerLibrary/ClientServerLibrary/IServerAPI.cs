using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerLibrary
{
    /// <summary>
    /// The interface for Clients to access the server over the internet.
    /// The Client should NEVER code as if a packet reached the server. The server will notify clients (with info) when they need to be updated.
    /// </summary>
    public interface IServerAPI
    {
        /// <summary>
        /// Ask the server to add a user to a group chat.
        /// 
        /// *Merits a SendChatID (or SendError) reply from the Server*
        /// </summary>
        /// <param name="username">The user to be added to the chat.</param>
        /// <param name="id">The ID of the chat to add the user to.</param>
        void AddChat(string username, int id);

        /// <summary>
        /// Ask the server to add a new (mutual) contact.
        /// 
        /// *Merits a SendNewContact (or SendError) reply from the Server*
        /// </summary>
        /// <param name="contactName">The user to add as a (mutual) contact.</param>
        void AddContact(string contactName);

        /// <summary>
        /// Ask the server to remove a contact (for both users).
        /// 
        /// *Merits a DeleteContact (or SendError) reply from the Server*
        /// </summary>
        /// <param name="contactName">The name of the contact to remove.</param>
        void DeleteContact(string contactName);

        /// <summary>
        /// Attempt a log-in with credentials.
        /// 
        /// *Merits a SendLoginReply reply from the Server, then (after a short delay) a SendContactList reply from the server*
        /// </summary>
        /// <param name="username">Your known or desired username.</param>
        /// <param name="password">Your known or desired password.</param>
        void LogIn(string username, string password);

        /// <summary>
        /// Notify the server that the client is logging out. The server can also handle abrupt connection interrupts.
        /// 
        /// *No reply to the client. However other clients may be sent a SendUserStatus packet*
        /// </summary>
        void LogOut();

        /// <summary>
        /// Attempts to open a new chat with a user.
        /// 
        /// *Merits a SendChatID (or SendError) reply from the Server*
        /// </summary>
        /// <param name="username">The user to be added to the chat.</param>
        void OpenChat(string username);

        /// <summary>
        /// Requests a contact list from the server.
        /// 
        /// *Merits a SendContactList (or SendError) reply from the Server*
        /// </summary>
        void RequestContactList();

        /// <summary>
        /// Requests the status of a user, who must be on that person's contact list.
        /// 
        /// *Merits a SendUserStatus (or SendError) reply from the Server*
        /// </summary>
        /// <param name="username">The user to request the status of.</param>
        void RequestContactStatus(string username);

        /// <summary>
        /// Sends a message in a group chat.
        /// 
        /// *Merits a SendMessage (or SendError) reply from the Server*
        /// </summary>
        /// <param name="message">The content of the message (and nobody better try any injection!).</param>
        /// <param name="chatID">The ID of the chat to send the message to.</param>
        void SendMessageToChat(string message, int chatID);
    }
}
