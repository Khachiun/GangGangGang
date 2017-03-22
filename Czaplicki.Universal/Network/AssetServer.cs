using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using Czaplicki.Universal.Extras;
using Czaplicki.Universal.Extentions;

namespace Czaplicki.Universal.Network
{




    /// <summary>
    /// Protocol :
    ///     Commands :
    ///             GET filename
    ///                 r = string OK , byte[] data
    ///             SET
    ///               
    /// </summary>
    public class AssetRetriver
    {
        public static bool RequestAsset(string host, int port, string assetName, out string message, out byte[] data)
        {
            try
            {
                TcpClient client = new TcpClient(host, port);
                NetworkStream stream = client.GetStream();
                StreamReader sr = new StreamReader(stream);
                StreamWriter sw = new StreamWriter(stream);

                sw.WriteLine("GET " + assetName);
                sw.Flush();

                string feedback = sr.ReadLine();

                if (feedback.StartsWith("OK "))
                {

                    message = sr.ReadLine();
                    //read bytes

                    List<byte> inData = new List<byte>();

                    int i;
                    while ((i = stream.ReadByte()) != -1)
                    {
                        inData.Add((byte)i);
                    }

                    data = inData.ToArray();
                    client.Close();
                    return true;
                }
                else
                {
                    System.Console.WriteLine(feedback);
                    data = null;
                    client.Close();
                    message = "ERROR";
                    return false;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                data = null;
                message = "ERROR";
                return false;
            }
        }
    }

    public class ByteBankServer
    {
        protected Dictionary<string, Set<string, byte[]>> assets = new Dictionary<string, Set<string, byte[]>>();

        private TcpListener listner;

        public ByteBankServer(int port)
        {
            listner = new TcpListener(port);
        }

        //DONE
        public bool AddAsset(string assetName, string assetMessage, byte[] AssetData)
        {
            if (!assets.ContainsKey(assetName))
            {
                assets.Add(assetName, new Set<string, byte[]>(assetMessage, AssetData));

                return true;
            }
            return false;
        }
        //DONE
        public bool RemoveAsset(string assetName)
        {
            if (!assets.ContainsKey(assetName))
            {
                assets.Remove(assetName);
                return true;
            }
            return false;
        }


        public void HandelPendingConnection()
        {
            System.Console.WriteLine("Testing for connetion");

            while (listner.Pending())
            {
                System.Console.WriteLine("client found");

                System.Console.WriteLine("establishes connection");
                TcpClient client = listner.AcceptTcpClient();

                NetworkStream stream = client.GetStream();
                StreamReader sr = new StreamReader(stream);
                StreamWriter sw = new StreamWriter(stream);

                try
                {
                    string request = sr.ReadLine();
                    string[] tockens = request.Split(' ');
                    string requestType = tockens[0];
                    string[] requestArgs = tockens.SubArray(1);

                    switch (requestType)
                    {
                        case "GET":
                            Set<string, byte[]> set;
                            if (assets.TryGetValue(requestArgs[0], out set))
                            {
                                byte[] data = set.Value2;

                                //feedback
                                sw.WriteLine("OK FileFound");
                                sw.Flush();

                                //messeage
                                sw.WriteLine(set.Value1);
                                sw.Flush();
                                
                                //data
                                stream.Write(data, 0, data.Length);
                                stream.Flush();
                            }
                            else
                            {
                                sw.WriteLine("ERROR GetRequestExeption:FileNotFound");
                                sw.Flush();
                            }
                            client.Close();
                            break;
                        default:
                            sw.WriteLine("ERROR UnknownRequestType");
                            break;
                    }

                }
                catch (Exception e)
                {

                    System.Console.WriteLine(e);
                }
            }
        }

        public void Start()
        {
            listner.Start();
        }

        //public void Start()
        //{
        //    listner.Start();

        //    while (true)
        //    {
        //        System.Console.WriteLine("Waiting for connection");
        //        TcpClient client = listner.AcceptTcpClient(); // blocking call                

        //        StreamReader sr = new StreamReader(client.GetStream());
        //        StreamWriter sw = new StreamWriter(client.GetStream());
        //        try
        //        {
        //            //client Request
        //            string request = sr.ReadLine();
        //            System.Console.WriteLine(request);
        //            string[] tokens = request.Split(' ');
        //            string command = tokens[0];
        //            string assetName = tokens[1];


        //            if (command == "GET")
        //            {
        //                Set<string, byte[]> set;
        //                if (assets.TryGetValue(assetName, out set))
        //                {
        //                    byte[] data = set.Value2;

        //                    sw.WriteLine("OK FileFound");
        //                    sw.Flush();

        //                    //messeage
        //                    sw.WriteLine(set.Value1);
        //                    sw.Flush();

        //                    NetworkStream stream = client.GetStream();

        //                    stream.Write(data, 0, data.Length);
        //                    stream.Flush();
        //                }
        //                else
        //                {
        //                    sw.WriteLine("ERROR GetRequestExeption:FileNotFound");
        //                    sw.Flush();
        //                }
        //                client.Close();
        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            sw.WriteLine("ERROR UnKnownExteption");
        //        }
        //    }
        //}
        public void Stop()
        {
            listner.Stop();
        }

    }
}
