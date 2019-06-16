using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using ClientServerLibrary;
using WebSocketSharp;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Server
{
    class ChatSocketBehavior : WebSocketBehavior, IClient
    {
        private static List<String> packetLog = new List<String>();
        private static List<IWebSocketSession> sessions = new List<IWebSocketSession>();

        public static event EventHandler<string> OnLoginPacketRecieved; // must be static due to WebSocketServer lib
        public static event EventHandler<string> OnLogoutPacketRecieved;
        public static event EventHandler<string> OnQueryPacketRecieved;

        private static Dictionary<string, string> tempAccounts = new Dictionary<string, string>(); // where string,string := temp_Name, IWebSocketSessionID


        static private int tempNameCounter = 0;

        protected override void OnOpen()
        {
            Console.WriteLine("Opened new session.");
            //base.OnOpen();
            IWebSocketSession newSession = Sessions.Sessions.Aggregate<IWebSocketSession>((i, j) => i.StartTime.Ticks > j.StartTime.Ticks ? i : j);

            sessions.Add(newSession);
            // send message with temp id, where chatID = -1
            string tempName = getNextTempUsername();
            SendMessage(newSession.ID, -1, tempName);
            tempAccounts.Add(tempName, newSession.ID);
        }

        public string getNextTempUsername()
        {
            string tempName = "temp_" + tempNameCounter.ToString();
            tempNameCounter++;
            return tempName;
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine("Closed existing session.");
            //base.OnClose(e);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine(e.Data);

            
            string[] args = JsonConvert.DeserializeObject<string[]>(e.Data);

            if (args.Length < 3)
            {
                return;
            }

            if (args[0].Equals("C_LogIn"))
            {
                OnLoginPacketRecieved?.Invoke(this, e.Data);
            }
            else if (args[0].Equals("C_LogOut"))
            {
                OnLogoutPacketRecieved?.Invoke(this, e.Data);
            }
            else
            {
                OnQueryPacketRecieved?.Invoke(this, e.Data);
            }
            
            //base.OnMessage(e);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            //Console.WriteLine("Error recieved & handled.");
            //base.OnError(e);
        }

        public void BroadcastStatus(string username, bool isOnline)
        {
            string[] args = { "S_BroadcastStatus", DateTime.Now.ToString(), "SERVER", username, isOnline.ToString() };
            string json = JsonConvert.SerializeObject(args);
            base.Sessions.Broadcast(json);
        }

        public void DeleteContact(string clientID, string contactName)
        {
            string[] args = { "S_DeleteContact", DateTime.Now.ToString(), "SERVER", contactName };
            string json = JsonConvert.SerializeObject(args);
            base.Sessions.SendTo(clientID, json);
        }

        public void SendChatID(string clientID, int chatID)
        {
            string[] args = { "S_SendChatID", DateTime.Now.ToString(), "SERVER", chatID.ToString() };
            string json = JsonConvert.SerializeObject(args);
            base.Sessions.SendTo(clientID, json);
        }

        public void SendContactList(string clientID, List<string> contactList)
        {
            //string[] args = { "S_SendContactList", DateTime.Now.ToString(), "SERVER", };
            string[] args = new string[3 + contactList.Count];
            args[0] = "S_SendContactList";
            args[1] = DateTime.Now.ToString();
            args[2] = "SERVER";
            for (int i = 3, j = 0; i < 3 + contactList.Count; i++, j++)
                args[i] = contactList[j];
            string json = JsonConvert.SerializeObject(args);
            base.Sessions.SendTo(clientID, json);
        }

        public void SendError(string clientID, string error)
        {
            string[] args = { "S_SendError", DateTime.Now.ToString(), "SERVER", error };
            string json = JsonConvert.SerializeObject(args);
            base.Sessions.SendTo(clientID, json);
        }

        public void SendLoginReply(string clientID, bool value)
        {
            string[] args = { "S_SendLoginReply", DateTime.Now.ToString(), "SERVER", value.ToString() };
            string json = JsonConvert.SerializeObject(args);
            base.Sessions.SendTo(clientID, json);
        }

        public void SendMessage(string clientID, int chatID, string message)
        {
            string[] args = { "S_SendMessage", DateTime.Now.ToString(), "SERVER", chatID.ToString(), message };
            string json = JsonConvert.SerializeObject(args);
            base.Sessions.SendTo(clientID, json);
        }

        public void SendNewContact(string clientID, string contactName)
        {
            string[] args = { "S_SendNewContact", DateTime.Now.ToString(), "SERVER", contactName };
            string json = JsonConvert.SerializeObject(args);
            base.Sessions.SendTo(clientID, json);
        }

        public void SendUserStatus(string clientID, string username, bool isOnline)
        {
            string[] args = { "S_SendUserStatus", DateTime.Now.ToString(), "SERVER", username, isOnline.ToString() };
            string json = JsonConvert.SerializeObject(args);
            base.Sessions.SendTo(clientID, json);
        }

        public Dictionary<string, string> TempUsernameMap
        {
            get
            {
                return tempAccounts;
            }
        }
    }
}
