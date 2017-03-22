using Czaplicki.Universal.Network;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Czaplicki.SFMLE.Network
{

    public class SFMLAssetClient
    {
        private string host;
        private int port;

        public SFMLAssetClient(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public bool RetriveTexture(string texureName, out Texture texture)
        {

            byte[] data;
            string message;
            TcpClient client = new TcpClient();

            try
            {
                client.Connect(host, port); // Create Conneting

                NetworkStream stream = client.GetStream(); // getting all tools to manipulate stream
                StreamReader sr = new StreamReader(stream);
                StreamWriter sw = new StreamWriter(stream);

                sw.WriteLine("GET TEXTURE/" + texureName); // Send Request for a texture
                sw.Flush();

                string feedback = sr.ReadLine(); // waits for awnser


                if (feedback.StartsWith("OK ")) // if server sais it OK
                {

                    message = sr.ReadLine(); // read the attashed massage

                    List<byte> inData = new List<byte>();
                    int i;
                    while ((i = stream.ReadByte()) != -1) // read the data byte by byte
                    {
                        inData.Add((byte)i);
                    }


                    client.Close(); // close the connection

                    data = inData.ToArray();
                }
                else
                {
                    //Error form feedback
                    System.Console.WriteLine(feedback);
                    client.Close();
                    texture = null;
                    return false;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                //throw e;
                texture = null;
                return false;
            }



            string[] args = message.Split(' ');

            uint width = uint.Parse(args[0]);
            uint height = uint.Parse(args[1]);


            texture = new Texture(width, height);

            texture.Update(data.ToArray());            
            return true;
        }

    }
    public class SFMLAssetServer : ByteBankServer
    {
        public SFMLAssetServer(int port) : base(port)
        {
        }

        public bool AddTexture(string assetName, Texture textrue)
        {
                string assetMessage = textrue.Size.X + " " + textrue.Size.Y;
                byte[] assetData = textrue.CopyToImage().Pixels;
                return this.AddAsset("TEXTURE/" + assetName, assetMessage, assetData);
        }
    }
}
