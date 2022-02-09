using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;
//https://itq.eu/net-4-5-websocket-client-without-a-browser/
namespace websocketchat
{
    public class TestWebSocketHandler : WebSocketHandler
    {
        private static WebSocketCollection clients = new WebSocketCollection();

        private string id;

       
        public override void OnOpen()

        {
            try
            {
                this.id = this.WebSocketContext.QueryString["id"];
                clients.Add(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public override void OnMessage(string message)

        {
            try
            {
                clients.Broadcast(string.Format("{0}", message));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public override void OnClose()

        {
            try
            {
                // Send disconnect signal here
                Console.WriteLine(this.id);
                clients.Broadcast("Remove Player " + this.id);
                clients.Remove(this);
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}