using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Websocket_Chat_Client
{
    public interface Observer
    {
        // this interface is for the model observer design pattern

        List<Observer> observers { get; set; }


        void Update();
       
    }
}
