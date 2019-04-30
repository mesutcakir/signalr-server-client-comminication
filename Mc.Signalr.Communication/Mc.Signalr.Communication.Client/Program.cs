﻿using Mc.Signalr.Communication.Server.Model;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mc.Signalr.Communication.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting client  " + HubSetting.HubUrl);

            var hubConnection = new HubConnection(HubSetting.HubUrl);
            //hubConnection.TraceLevel = TraceLevels.All;
            //hubConnection.TraceWriter = Console.Out;
            IHubProxy myHubProxy = hubConnection.CreateHubProxy(HubSetting.HubName);

            myHubProxy.On<string, string>("addMessage", (name, message) => Console.Write("Recieved addMessage: " + name + ": " + message + "\n"));
            myHubProxy.On("heartbeat", () => Console.Write("Recieved heartbeat \n"));
            myHubProxy.On<HelloModel>("sendHelloObject", hello => Console.Write("Recieved sendHelloObject {0}, {1} \n", hello.Title, hello.Message));

            hubConnection.Start().Wait();

            while (true)
            {
                string key = Console.ReadLine();
                if (key.ToUpper() == "W")
                {
                    myHubProxy.Invoke("addMessage", "client message", " sent from console client").ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Console.WriteLine("!!! There was an error opening the connection:{0} \n", task.Exception.GetBaseException());
                        }

                    }).Wait();
                    Console.WriteLine("Client Sending addMessage to server\n");
                }
                if (key.ToUpper() == "E")
                {
                    myHubProxy.Invoke("Heartbeat").ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                        }

                    }).Wait();
                    Console.WriteLine("client heartbeat sent to server\n");
                }
                if (key.ToUpper() == "R")
                {
                    HelloModel hello = new HelloModel { Title = "Client Send Title", Message = "clientMessage" };
                    myHubProxy.Invoke<HelloModel>("SendHelloObject", hello).ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                        }

                    }).Wait();
                    Console.WriteLine("client sendHelloObject sent to server\n");
                }
                if (key.ToUpper() == "C")
                {
                    break;
                }
            }
        }
    }
}
