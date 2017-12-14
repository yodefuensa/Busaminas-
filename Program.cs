using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscaminas
{
    class Program
    {
        static void Main(String[] Args)
        {

            //tablero de juego
            int[,] tablero = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tablero[i, j] = 0;
                }
            }

            //tablero visual
            Boolean[,] tableroVisual = new Boolean[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tableroVisual[i, j] = false;
                }
            }

            ponerMinas(tablero);
            int x = 0;
            int y = 0;

            while (true)
            {
                dibujar(tablero, tableroVisual, x, y);
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        x--;
                        break;
                    case ConsoleKey.DownArrow:
                        x++;
                        break;
                    case ConsoleKey.RightArrow:
                        y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        y--;
                        break;
                    case ConsoleKey.Enter:
                        marcar(x, y, tableroVisual, tablero);
                        break;
                }
                ganas(9, 9, tableroVisual);
            }
        }



        static void dibujar(int[,] casillas, Boolean[,] pulsado, int posX, int posY)
        {
            Console.Clear();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == posX && j == posY)
                        Console.BackgroundColor = ConsoleColor.Green;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;
                    if (pulsado[i, j] == false)
                    {
                        Console.Write("| - ");
                    }
                    else
                    {

                        if (casillas[i, j] < 0)
                        {
                            Console.Write("GAME OVER!");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                        Console.Write("| " + casillas[i, j] + " ");
                    }

                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }


        //mina igual string =X, 
        static void ponerMinas(int[,] casillas)
        {
            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                int numberX = rnd.Next(0, 9);
                int numberY = rnd.Next(0, 9);
                if (!(casillas[numberX, numberY] < 0))
                    casillas[numberX, numberY] = -8;
                if (!(casillas[numberX, numberY] > 0))
                {
                    if (numberX == 0)
                    {
                        if (!(numberY == 0))
                            casillas[numberX, numberY - 1]++;
                        casillas[numberX, numberY + 1]++;
                        if (!(numberY == 0))
                            casillas[numberX + 1, numberY - 1]++;
                        casillas[numberX + 1, numberY]++;
                        casillas[numberX + 1, numberY + 1]++;
                    }
                    else if (numberX == 9)
                    {
                        if (!(numberY == 0))
                            casillas[numberX - 1, numberY - 1]++;
                        casillas[numberX - 1, numberY]++;
                        casillas[numberX - 1, numberY + 1]++;
                        if (!(numberY == 0))
                            casillas[numberX, numberY - 1]++;
                        casillas[numberX, numberY + 1]++;
                    }
                    else if (numberY == 0)
                    {
                        casillas[numberX - 1, numberY]++;
                        casillas[numberX - 1, numberY + 1]++;
                        if (!(numberX == 9))
                        {
                            casillas[numberX + 1, numberY]++;
                            casillas[numberX + 1, numberY + 1]++;
                        }
                        casillas[numberX, numberY + 1]++;
                    }
                    else if (numberY == 9)
                    {
                        if (!(numberX == 9))
                        {
                            casillas[numberX + 1, numberY - 1]++;
                            casillas[numberX + 1, numberY]++;
                        }
                        casillas[numberX, numberY - 1]++;
                        casillas[numberX - 1, numberY - 1]++;
                        casillas[numberX - 1, numberY]++;
                    }
                    else
                    {
                        casillas[numberX - 1, numberY - 1]++;
                        casillas[numberX - 1, numberY]++;
                        casillas[numberX - 1, numberY + 1]++;
                        casillas[numberX, numberY - 1]++;
                        casillas[numberX, numberY + 1]++;
                        casillas[numberX + 1, numberY - 1]++;
                        casillas[numberX + 1, numberY]++;
                        casillas[numberX + 1, numberY + 1]++;
                    }
                }

            }

        }

        static void marcar(int x, int y, Boolean[,] tab, int[,] tabInt)
        {
            if (x >= 0 && x <= 9 && y >= 0 && y <= 9)
            {
                if (tab[x, y] == false)
                {
                    tab[x, y] = true;
                    if (tabInt[x, y] == 0)
                    {
                        marcar(x - 1, y  -1, tab, tabInt);
                        marcar(x - 1, y, tab, tabInt);
                        marcar(x - 1, y +1, tab, tabInt);
                       

                        marcar(x , y -1, tab, tabInt);
                        marcar(x , y +1, tab, tabInt);

                        marcar(x + 1, y, tab, tabInt);
                        marcar(x + 1, y + 1, tab, tabInt);
                        marcar(x + 1, y - 1, tab, tabInt);

                    }

                }

            }

        }


        //pasamos x e y proque en un futuro se podria cambaiar el tamaño del buscaminas por ahora pasamos 9
        static void ganas(int x, int y, Boolean[,] tab)
        {
            int counter = 0;
            for (int i = 0; i <= x; i++)
            {
                for (int j = 0; j <= y; j++)
                {
                    if (tab[i, j] == false)
                        counter++;
                }
            }
            if (counter == 10)
            {
                Console.Clear();
                Console.WriteLine("HAS GANADO");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

    }

}
