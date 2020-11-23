using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TicTacToeServer
{
    class TicTacToe
    {
        private static char[] map = new char[9];
        private static char[] standartMap = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static char currentChar = 'X';
        private static int phase = 0;
        private static int localPhase = 0;
        private static string currentPlayer = "Player 1";
        private static bool drawFlag = false;

        static void MainA(string[] args)
        {
            while (true)
            {
                localPhase = 0;
                standartMap.CopyTo(map, 0);
                MainCycle();
            }
        }

        private static void MainCycle()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("PLayer 1: X");
                Console.WriteLine("PLayer 2: O");
                Console.WriteLine();
                Console.WriteLine($"{currentPlayer}'s turn");
                Console.WriteLine();
                ShowBoard();
                Console.WriteLine();

                Console.Write("Enter cell number: ");
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (map.Contains(key.KeyChar))
                    {
                        map[int.Parse(key.KeyChar.ToString()) - 1] = currentChar;
                        break;
                    }
                    Console.Write("\b \b");
                }
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
                Console.WriteLine($"\n\n\n {currentPlayer} win!");
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
                currentPlayer = "Player 1";
            }
            else
            {
                currentChar = 'O';
                currentPlayer = "Player 2";
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

        private static void ShowBoard()
        {
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {map[0]}  |  {map[1]}  |  {map[2]}");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {map[3]}  |  {map[4]}  |  {map[5]}");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {map[6]}  |  {map[7]}  |  {map[8]}");
            Console.WriteLine("     |     |      ");
        }
    }
}
