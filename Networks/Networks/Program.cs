using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace Networks
{
    class Program
    {
        static string userName;
        private const string host = "192.168.1.69";
        private const int port = 8888;
        //static TcpClient client;
        static NetworkStream stream;

        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            try
            {
                Console.WriteLine("SuukaBlyat");

                client.Connect(host, port);
                
                Console.WriteLine("Connected to server");
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    byte[] data = Encoding.Unicode.GetBytes(key.KeyChar.ToString());
                    //Console.Clear();
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Chlen");
                }

                client.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        //static void Main(string[] args)
        //{
        //    Console.Write("Enter your name: ");
        //    userName = Console.ReadLine();
        //    client = new TcpClient();

        //    try
        //    {
        //        client.Connect(host, port);
        //        stream = client.GetStream();

        //        string message = userName;
        //        byte[] data = Encoding.Unicode.GetBytes(message);
        //        stream.Write(data, 0, data.Length);

        //        Thread recieveThread = new Thread(new ThreadStart(RecieveMessage));
        //        recieveThread.Start();
        //        Console.WriteLine($"Welcome, {userName}");
        //        SendMessage();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        Disconnect();
        //    }
        //}

        private static void SendMessage()
        {
            Console.WriteLine("Enter your message: ");

            while (true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }

        private static void RecieveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (stream.DataAvailable);

                    Console.WriteLine(builder);
                }
                catch
                {
                    Console.WriteLine("Connection lost");
                    Console.ReadLine();
                    //Disconnect();
                }
            }
        }

        //private static void Disconnect()
        //{
        //    if (stream != null)
        //    {
        //        stream.Close();
        //    }
        //    if (client != null)
        //    {
        //        client.Close();
        //    }

        //    Environment.Exit(0);
        //}
    }
}
