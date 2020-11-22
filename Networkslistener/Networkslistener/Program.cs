using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace TicTacToeServer
{
    class Program
    {
        private static TcpListener listener;
        private static TcpClient client;

        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse("192.168.1.69"), 8888);
                listener.Start();
                Console.WriteLine("Server waiting for the connections");

                while (true)
                {
                    client = listener.AcceptTcpClient();

                    Thread listhenThread = new Thread(new ThreadStart(Listen));
                    listhenThread.Start();
                    Console.WriteLine(client.Client.RemoteEndPoint.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                }
            }
        }

        private static void Listen()
        {
            NetworkStream stream = client.GetStream();

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
    }
}
