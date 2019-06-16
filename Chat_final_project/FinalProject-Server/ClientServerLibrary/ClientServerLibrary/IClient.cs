using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerLibrary
{
    /// <summary>
    /// The interface for Server to implement. This is how it will interface with Clients.
    /// </summary>
   public interface IClient
    {
        /// <summary>
        /// Send the status of a user to all clients. This probably won't be needed as the scope of these messages..
        /// .. should be limited to their contact list.
        /// </summary>
        /// <param name="username">The name of the user who's status is being broadcast.</param>
        /// <param name="isOnline">The value of that user's status.</param>
        void BroadcastStatus(string username, bool isOnline);

        /// <summary>
        /// Notifies a Client that they had a contact deleted server side.
        /// </summary>
        /// <param name="contactName">The name of the contact deleted.</param>
        void DeleteContact(string clientID, string contactName);

        /// <summary>
        /// Notifies a Client that they were added to a chat, and tells the Client the ID of that chat.
        /// </summary>
        /// <param name="clientID">The ID of the client.</param>
        /// <param name="chatID">The chat ID to notify the client about.</param>
        void SendChatID(string clientID, int chatID);

        /// <summary>
        /// Sends a Client a complete contact list.
        /// </summary>
        /// <param name="clientID">The ID of the client.</param>
        /// <param name="contactList">The contact list, which will be serialized.</param>
        void SendContactList(string clientID, List<String> contactList);

        /// <summary>
        /// Sends a Client an error, with a custom message.
        /// </summary>
        /// <param name="clientID">The ID of the client.</param>
        /// <param name="error">The error message text.</param>
        void SendError(string clientID, string error);

        /// <summary>
        /// Sends a Client a login reply, this is a VERY special case where clientID might not be mapped to a username in the model.
        /// </summary>
        /// <param name="clientID">The ID of the client.</param>
        /// <param name="value">The bool value saying if the login was successful.</param>
        void SendLoginReply(string clientID, bool value);

        /// <summary>
        /// Sends a new contact 
        /// </summary>
        /// <param name="clientID">The ID of the client.</param>
        /// <param name="contactName">The name of the new contactName</param>
        void SendNewContact(string clientID, string contactName);

        /// <summary>
        /// Sends a user status (online/offline)
        /// </summary>
        /// <param name="clientID">The ID of the client.</param>
        /// <param name="username">The username of the user in question.</param>
        /// <param name="isOnline">The status of the user in question.</param>
        void SendUserStatus(string clientID, string username, bool isOnline);

        /// <summary>
        /// Sends a message to a CLIENT. REPEAT. This sends the message to a client, and must be called (by the controller)..
        /// .. for each client in the chat. The only reason chatID is a param is because the client needs to know which chat to display..
        /// .. it's recieved message in. ChatID doesn't mean this method should send all people in the chat (because it would need the model's information to do that).
        /// </summary>
        /// <param name="clientID">The ID of the client.</param>
        /// <param name="chatID">The ID of the chat to send the message in.</param>
        /// <param name="message">The content of the message.</param>
        void SendMessage(string clientID, int chatID, string message);
    }
}
