using Czaplicki.Universal.Casings;
using Czaplicki.Universal.Console;
using Czaplicki.Universal.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Czaplicki.Universal.NetWork
{
    public class TcpNetwork
    {
        #region universal

        //[CConsoleCommand("read")]
        //public static string[] UniversalRead()
        //{
        //    if (Connected)
        //        if (isServer)
        //            return Read_Server();
        //        else
        //            return Read_Client();
        //    else
        //        CConsole.Logg("Error: Connected to a server or hosting");
        //    return null;
        //}
        //[CConsoleCommand("send")]
        //public static void UniversalSend(string message)
        //{
        //    if (Connected)
        //        if (isServer)
        //            Send_Server(message);
        //        else
        //            Send_Client(message);
        //    else
        //        CConsole.Logg("Error: Connected to a server or hosting");

        //}




        #endregion
        #region Client
        static TcpClient tcpClient;

        [CConsoleCommand("c.connect")]
        public static bool ConnectToServer(string ip, int port, string allias)
        {
            try
            {
                Console.CConsole.Logg("Connecting...");
                tcpClient = new TcpClient(ip, port);
                tcpClient.GetStream().WriteLine(allias).Flush();
                Console.CConsole.Logg("Connected!");
                return true;

            }
            catch (Exception e)
            {
                Console.CConsole.Logg(e.Message);
                Console.CConsole.Logg("Connection faild");
                return false;
            }
        }

        [CConsoleCommand("c.disconnect")]
        public static void DisconnectFromServer()
        {
            Console.CConsole.Logg("Disconnecting...");
            tcpClient.Close();
            Console.CConsole.Logg("Disconected!");
        }

        [CConsoleCommand("c.read")]
        private static string[] Read_Client()
        {
            Console.CConsole.Logg("Starting to read from server");
            string[] messages = tcpClient.GetStream().ReadAvalibleLines();
            foreach (var message in messages)
            {
                Console.CConsole.Logg(message);
            }
            Console.CConsole.Logg("Done reading from server");
            return messages;
        }

        [CConsoleCommand("c.send")]
        private static void Send_Client(string message)
        {
            Console.CConsole.Logg("starting to send massage to server");
            tcpClient.GetStream().WriteLine(message);
            Console.CConsole.Logg("Done sending to server");
        }

        #endregion
        #region Server
        static SynchronizedObject<List<TcpClient>> Clients;
        static TcpListener listener;
        static Thread ListenerThread;


        private static void ListenForClients()
        {
            listener.Start();
            try
            {
                while (true)
                {
                    var client = listener.AcceptTcpClient();
                    Clients.Value.Add(client);
                    Console.CConsole.Logg("Client Connected :" + client);
                }
            }
            catch (Exception e)
            {
                Console.CConsole.Error(e.Message);
                listener.Stop();
                return;
            }

        }

        [CConsoleCommand("s.host")]
        public static void InitializeSever(int port)
        {
            Console.CConsole.Logg("Starting server...");
            Clients = (SynchronizedObject<List<TcpClient>>)new List<TcpClient>();
            listener = new TcpListener(IPAddress.Loopback, port);

            ListenerThread = new Thread(ListenForClients);

            Console.CConsole.Logg("Server is up");
        }

        [CConsoleCommand("s.open")]
        private static void AcceptClients()
        {
            Console.CConsole.Logg("Opening doors");
            ListenerThread.Start();
            Console.CConsole.Logg("Doors open : accepting clients");
        }

        [CConsoleCommand("s.close")] // BROKEN
        private static void CloseClientIntake()
        {
            Console.CConsole.Logg("Closeing doors");
            ListenerThread.Abort();
            Console.CConsole.Logg("Doors Closed : not accepting clients any more");
        }

        [CConsoleCommand("s.read")]
        private static string[] Read_Server()
        {
            List<string> returnValue = new List<string>();

            Console.CConsole.Logg("starting to read from clients");
            foreach (var client in Clients.Value)
            {
                var stream = client.GetStream();
                string[] messages = stream.ReadAvalibleLines();

                foreach (var item in messages)
                {
                    Console.CConsole.Logg(item);
                }
                returnValue.AddRange(messages);
            }
            Console.CConsole.Logg("Done reading");
            return returnValue.ToArray();
        }

        [CConsoleCommand("s.send")]
        private static void Send_Server(string message)
        {
            Console.CConsole.Logg("starting to Send to clients");
            lock (Clients.Value)
            {
                foreach (var client in Clients.Value)
                {
                    client.GetStream().WriteLine(message).Flush();
                }
            }
            Console.CConsole.Logg("done sending to clients");
        }
        #endregion
    }
}
