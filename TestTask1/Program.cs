using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Management;
using System.Net;
using System.Net.Sockets;
//using WebSocketSharp;
//using WebSocketSharp.Server;

namespace TZIDR
{
    class ClientServer
    {
        static void Main(string[] args)
        {
            try
            {
                TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 4444);

                server.Start();
                Console.WriteLine("Server has started on 127.0.0.1:4444.");
                Console.WriteLine("The local End point is  :" + server.LocalEndpoint);
                Console.WriteLine("Waiting for new connection...");
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("A client connected.");

                    NetworkStream stream = client.GetStream();

                    byte[] buffer = new byte[client.ReceiveBufferSize];

                    //---read incoming stream---
                    int bytesRead = stream.Read(buffer, 0, client.ReceiveBufferSize);

                    //---convert the data received into a string---
                    string dataReceived = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received : " + dataReceived);





                    // закрываем сокет
                    stream.Close();
                    client.Close();
                }
                server.Stop();
                Console.ReadLine();


            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }

        }
    }
}