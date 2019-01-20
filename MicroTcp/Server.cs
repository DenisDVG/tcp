using MicroTcp.BLL;
using MicroTcp.BLL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroTcp
{
    public class Server
    {
        public static List<TcpListener> _tcpServers;
        public static List<Client> _clients;
        //private TcpListener _server;
        //StreamWriter _sWriter;
        //StreamReader _sReader;
        public static int _startsPortNumber = 5555;
        public static int _endPortNumber = 5555;
        static void Main()
        {
            _tcpServers = new List<TcpListener>();
            _clients = new List<Client>();
            _endPortNumber = _startsPortNumber;
            StartNewTcpServerTread();
            //StartNewTcpServer();

        }
        private static void StartNewTcpServerTread()
        {
            Thread t = new Thread(StartNewTcpServer);
            t.Start();
        }

        private static void StartNewTcpServer()
        {
            var ipPoint = new IPEndPoint(IPAddress.Parse($"127.0.0.1"), _endPortNumber);
            ++_endPortNumber;
            //Thread th = new Thread(StartNewTcpServer);
            //th.Start(ipPoint);

            var server = new TcpListener(ipPoint);
            server.Start();
            _tcpServers.Add(server);
            LoopClients(server);

        }

        public static void LoopClients(TcpListener server)
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(client);
                
            }
        }

        public static void HandleClient(object obj)
        {
            TcpClient tcpClient = (TcpClient)obj;
            StreamWriter sWriter = new StreamWriter(tcpClient.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(tcpClient.GetStream(), Encoding.ASCII);
            Boolean bClientConnected = true;
            String messageJson = null;
            IPEndPoint endPoint = (IPEndPoint)tcpClient.Client.LocalEndPoint;
            if (endPoint.Port != _startsPortNumber)
            {
                var client = new Client
                {
                    Port = _endPortNumber - 1,
                    TcpClient = tcpClient
                };
                _clients.Add(client);
            }

            while (bClientConnected)
            {

                messageJson = sReader.ReadLine();
                if (string.IsNullOrWhiteSpace(messageJson) )
                {

                    continue;
                }
                var message = JsonConvert.DeserializeObject<Message>(messageJson);
                if (message == null)
                {
                    continue;
                }
                Console.WriteLine("From Client; " + messageJson);

                if ("conected" == message.Text)
                {
                    
                }
                if ("start" == message.Text)
                {

                    StartNewTcpServerTread();
                    SentToClient($"conect to:{_endPortNumber}", sWriter);
                }
                if (message.ToPort != 0)
                {
                    var messageRecipient = _clients.FirstOrDefault(x => x.Port == message.ToPort);
                    if(messageRecipient == null)
                    {
                        continue;
                    }
                    var sWriterRecipient = new StreamWriter(messageRecipient.TcpClient.GetStream(), Encoding.ASCII);
                    SentToClient(message.Text, sWriterRecipient);
                }
            }
        }

        private static void SentToClient(string message, StreamWriter sWriter)
        {
            sWriter.WriteLine(message);
            sWriter.Flush();
        }
    }

    //public class TcpServer
    //{
    //    private TcpListener _server;
    //    private Boolean _isRunning;
    //    StreamWriter _sWriter;
    //    StreamReader _sReader;
    //    public TcpServer(IPEndPoint localEP)
    //    {
    //        _server = new TcpListener(localEP);
    //        _server.Start();
    //        _isRunning = true;
    //        LoopClients();
    //    }

    //    public void LoopClients()
    //    {
    //        while (_isRunning)
    //        {
    //            TcpClient client = _server.AcceptTcpClient();
    //            Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
    //            t.Start(client);
    //        }
    //    }

    //    public void HandleClient(object obj)
    //    {
    //        TcpClient client = (TcpClient)obj;
    //        _sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
    //        _sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
    //        Boolean bClientConnected = true;
    //        String sData = null;

    //        while (bClientConnected)
    //        {
    //            sData = _sReader.ReadLine();
    //            Console.WriteLine("From Client; " + sData);
    //            if("it is conected" == sData)
    //            {

    //            }
    //            if ("it is start" == sData)
    //            {
    //                SentToClient("Start N port");
    //            }
    //        }
    //    }

    //    private void SentToClient(string message)
    //    {
    //        _sWriter.WriteLine(message);
    //        _sWriter.Flush();
    //    }
    //}
}
