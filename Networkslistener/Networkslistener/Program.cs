using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Linq;

namespace TicTacToeServer
{
    class Program
    {
        private static TcpListener listener;
        private static TcpClient client;

        //Fix this
        private static Client cli1;
        private static Client cli2;

        private static char[] map = new char[9];
        private static char[] standartMap = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static char currentChar = 'X';
        private static int phase = 0;
        private static int localPhase = 0;
        private static Client currentPlayer;
        private static bool drawFlag = false;

        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 8888);
                listener.Start();
                Console.WriteLine("Server waiting for the connections");

                //now only for 2 players
                client = listener.AcceptTcpClient();
                cli1 = new Client(client, "Player1");
                Thread listhenThread = new Thread(new ThreadStart(cli1.Process));
                listhenThread.Start();
                Console.WriteLine(client.Client.RemoteEndPoint.ToString());

                client = listener.AcceptTcpClient();
                cli2 = new Client(client, "Player2");
                listhenThread = new Thread(new ThreadStart(cli2.Process));
                listhenThread.Start();
                Console.WriteLine(client.Client.RemoteEndPoint.ToString());

                currentPlayer = cli1;
                //for (int i = 0; i < 2; i++)
                //{
                //    client = listener.AcceptTcpClient();
                //    Client newClient = new Client(client);
                //    Thread listhenThread = new Thread(new ThreadStart(newClient.Process));
                //    listhenThread.Start();
                //    Console.WriteLine(client.Client.RemoteEndPoint.ToString());
                //}

                //tic
                //tac
                //toe
                while (true)
                {
                    localPhase = 0;
                    standartMap.CopyTo(map, 0);
                    MainCycle();
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

        private static void Broadcast(string message)
        {
            cli1.SendInformation(message);
            cli2.SendInformation(message);
        }

        private static void MainCycle()
        {
            do
            {
                Console.Clear();
                StringBuilder builder = new StringBuilder();
                builder.Append("PLayer 1: X\n");
                builder.Append("PLayer 2: O\n");
                builder.Append($"{currentPlayer.name}'s turn\n");
                string message = ShowBoard().ToString();
                builder.Append(message);
                Console.WriteLine(message);
                builder.Append("\n");

                Broadcast(builder.ToString());

                Console.Write("Enter cell number: ");
                //while (true)
                //{
                //    ConsoleKeyInfo key = Console.ReadKey();
                //    if (map.Contains(key.KeyChar))
                //    {
                //        map[int.Parse(key.KeyChar.ToString()) - 1] = currentChar;
                //        break;
                //    }
                //    Console.Write("\b \b");
                //}
                char ch;
                do
                {
                    ch = currentPlayer.GetResponse();
                } while (!map.Contains(ch));
                map[int.Parse(ch.ToString()) - 1] = currentChar;

                changePhase();
            } while (CheckWin());

            Console.Clear();
            Console.WriteLine("PLayer 1: X");
            Console.WriteLine("PLayer 2: O");
            Console.WriteLine();
            ShowBoard();
            if (!drawFlag)
            {
                changePhase();
                Console.WriteLine($"\n\n\n {currentPlayer.name} win!");
                changePhase();
            }
            else
            {
                Console.WriteLine("Draw!");
            }
            Console.ReadLine();
        }

        private static void changePhase()
        {
            phase++;
            localPhase++;
            if (phase % 2 == 0)
            {
                currentChar = 'X';
                currentPlayer = cli1;
            }
            else
            {
                currentChar = 'O';
                currentPlayer = cli2;
            }
        }

        private static bool CheckWin()
        {
            if (localPhase == 9)
            {
                drawFlag = true;
                return false;
            }
            //pizdec blyat
            int i = 0;
            int j = 3;
            int k = 6;
            for (int x = 0; x < 3; x++)
            {
                if (map[i] == map[j] && map[j] == map[k])
                {
                    return false;
                }
                i++;
                j++;
                k++;
            }

            i = 0;
            j = 1;
            k = 2;
            for (int x = 0; x < 3; x++)
            {
                if (map[i] == map[j] && map[j] == map[k])
                {
                    return false;
                }
                i += 3;
                j += 3;
                k += 3;
            }

            if (map[0] == map[4] && map[4] == map[8])
            {
                return false;
            }
            if (map[2] == map[4] && map[4] == map[6])
            {
                return false;
            }

            return true;
        }

        private static StringBuilder ShowBoard()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("     |     |      \n");
            builder.Append($"  {map[0]}  |  {map[1]}  |  {map[2]}\n");
            builder.Append("_____|_____|_____ \n");
            builder.Append("     |     |      \n");
            builder.Append($"  {map[3]}  |  {map[4]}  |  {map[5]}\n");
            builder.Append("_____|_____|_____ \n");
            builder.Append("     |     |      \n");
            builder.Append($"  {map[6]}  |  {map[7]}  |  {map[8]}\n");
            builder.Append("     |     |      \n");

            return builder;
        }
    }
}
