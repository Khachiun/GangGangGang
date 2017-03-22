using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.Universal.Extentions
{
    public static class Network
    {
        public static NetworkStream WriteLine(this NetworkStream stream, string line)
        {
            line += "\n\r";
            foreach (var c in line)
            {
                var bytes = BitConverter.GetBytes(c);
                bytes = bytes.SubArray(0, bytes.Length - 1);
                foreach (var b in bytes)
                {
                    stream.WriteByte(b);
                }
            }
            return stream;
        }

        public static string[] ReadAvalibleLines(this NetworkStream stream)
        {
            string str;

            byte[] data = new byte[1024];
            using (MemoryStream ms = new MemoryStream())
            {

                int numBytesRead;
                while (stream.DataAvailable && (numBytesRead = stream.Read(data, 0, data.Length)) > 0)
                {
                    ms.Write(data, 0, numBytesRead);
                }
                str = Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length);
            }
            return str.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
