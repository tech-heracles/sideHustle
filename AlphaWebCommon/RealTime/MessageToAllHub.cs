using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AlphaWebCommon.RealTime
{
    public class MessageToAllHub : Hub
    {
        public void Send(string message, string title)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(message, title);
        }
    }
}