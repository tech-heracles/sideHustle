using System;
using Xunit;
using AlphaWebCommon.RealTime;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using System.Dynamic;

namespace InfrastructureTests.Common.RealTime
{
    public class MessageToAllTest
    {
        public interface IClientContract
        {
            void broadcastMessage(string name, string message);
        }

        /// <summary>
        /// Hub are Mockable via Dynamic
        /// </summary>
        [Fact]
        public void HubMocableViaDynamic()
        {
            bool sendCalled = false;
            var hub = new  MessageToAllHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            all.broadcastMessage = new Action<string, string>((name, message) =>
            {
                sendCalled = true;
            });
            mockClients.Setup(m => m.All).Returns((ExpandoObject)all);
            hub.Send("TestMessage", "<div>TestTitle</div>");
            Assert.True(sendCalled);
        }
    }
}
