using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Network
{
    class StreamHandler
    {
        
        public NetworkStream Stream { get; set; }
        public StreamHandler(NetworkStream stream)
        {
            this.Stream = stream;
        }

        public bool DataAvailable()
        {
            return Stream.DataAvailable;
        }

        public void SendPackage(string Message, params byte[][] data)
        {

            

        }

        public Package RendPackage()
        {
            return default(Package);
        }

        public struct Package
        {
            //public string message;
            //public byte[][] data;
        }
    }
}
