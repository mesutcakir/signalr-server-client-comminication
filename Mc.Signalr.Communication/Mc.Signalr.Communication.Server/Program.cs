using Mc.Signalr.Communication.Server.Hubs;
using Mc.Signalr.Communication.Server.Model;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mc.Signalr.Communication.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8089";
            using (WebApp.Start(url))
            {
                IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<ChannelHub>();
                Console.WriteLine("Server running on {0}", url);
                while (true)
                {
                    string key = Console.ReadLine();
                    if (key.ToUpper() == "W")
                    {
                        hubContext.Clients.All.addMessage("server", "ServerMessage");
                        Console.WriteLine("Server Sending addMessage\n");
                    }
                    if (key.ToUpper() == "E")
                    {
                        hubContext.Clients.All.heartbeat();
                        Console.WriteLine("Server Sending heartbeat\n");
                    }
                    if (key.ToUpper() == "R")
                    {
                        var vv = new HelloModel { Title = "Deneme Başlık", Message = "Deneme İçerik" };

                        hubContext.Clients.All.sendHelloObject(vv);
                        Console.WriteLine("Server Sending sendHelloObject\n");
                    }
                    if (key.ToUpper() == "C")
                    {
                        break;
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
