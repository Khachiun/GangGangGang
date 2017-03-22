using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Czaplicki.Universal.IO
{
    class BYTE
    {
        public bool Send(byte[] data, string host, int port)
        {
            //TcpClient client = new TcpClient();

            //client.Connect(host, port);

            //NetworkStream stream = client.GetStream();

            //stream.Write(data, 0, data.Length);
            //stream.Flush();

            //while (!stream.DataAvailable) Thread.Sleep(1);

            //stream.Close();
            //stream.Dispose();

            return false;
        }
        public bool Retrive(out byte[] data, int port)
        {
            //TcpListener server = new TcpListener(port);
            //TcpClient client = server.AcceptTcpClient();
            //NetworkStream stream = client.GetStream();

            //while (!stream.DataAvailable) Thread.Sleep(1);

            //List<byte> inData = new List<byte>();

            //int i;
            //while ((i = stream.ReadByte()) != -1)
            //{
            //    inData.Add((byte)i);
            //}

            //data = inData.ToArray();
            data = null;
            return false;
        }
    }
}
