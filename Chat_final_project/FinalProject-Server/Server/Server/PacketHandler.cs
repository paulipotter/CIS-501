using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServerLibrary;

namespace Server
{
    interface PacketHandler
    {
        // THESE ARE ALL HANDLED BY DELEGATES, ONLY HERE TO MAKE IT CLEAR WHAT THE DELEGATE DEFINITIONS ARE
        void handleLoginPacket(String packet, IClient client, Dictionary<string, string> tempAccounts);

        void handleLogoutPacket(String packet, IClient client, Dictionary<string, string> tempAccounts, string nextTempAccount);

        void handleQueryPacket(String packet, IClient client);
    }
}
