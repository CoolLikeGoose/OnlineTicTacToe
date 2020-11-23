using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace TicTacToeServer
{
    class Client
    {
        private TcpClient client;
        private NetworkStream stream;

        public string name;

        public Client(TcpClient client, string name)
        {
            this.client = client;
            this.name = name;
        }

        public void Process()
        {
            stream = client.GetStream();

            try
            {
                while (true)
                {
                    byte[] data = new byte[256];
                    int bytes = stream.Read(data, 0, data.Length);
                    Console.WriteLine(Encoding.Unicode.GetString(data, 0, bytes));
                }
            }
            catch
            {
                Console.WriteLine("Client disconnected");
                stream.Close();
            }
        }

        public char GetResponse()
        {
            string message = "Enter cell number: ";
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);

            data = new byte[64];
            int bytes = stream.Read(data, 0, data.Length);

            return char.Parse(Encoding.Unicode.GetString(data, 0, bytes));
        }

        public void SendInformation()
        {

        }
    }
}
