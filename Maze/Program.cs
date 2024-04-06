using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace Maze
{
    class MazeProgram
    {
        static int width = 25;
        static int height = 25;
        static string[,] mazePlane = new string[width, height];

        static Stack<int> prevX = new Stack<int>();
        static Stack<int> prevY = new Stack<int>();

        static int[] testedDirections = new int[4];
        static int x = 0;
        static int y = 0;
        static int moves = 0;

        static StreamWriter writer = new StreamWriter(@"C:\Users\julian.thunellaxnas\Documents\Maze\StackMethod.bin");


        static void Main(string[] args)
        {
            generateMaze();
            printMaze();

            /*for (int i = 0; i < 300; i++)
            {
                Console.WriteLine(i);
                generateMaze();
                generatePath();
                for (int j = 0; j < 6; j++)
                {
                    threeway();
                }
                //printMaze();
                writeMaze();
            }
            writer.Close();
            */
        }
        static void generateMaze()
        {
            Boolean chess = true;
            Boolean flip = true;
            //wall
            for (int i = 0; i < height; i++)
            {
                flip = true;
                for (int j = 0; j < width; j++)
                {
                    if (chess)
                    {
                        if (flip)
                        {
                            mazePlane[j, i] = "O";
                        }
                        else
                        {
                            mazePlane[j, i] = "X";
                        }
                        flip = !flip;
                    }
                    else
                    {
                        mazePlane[j, i] = "X";
                    }
                }
                chess = !chess;
            }
        }
        static void generatePath()
        {
            Boolean run = true;
            while (run)
            {
                if (checkComplete() == true)
                {
                    run = false;
                }
                else
                {
                    stackAlgorithm();
                    //huntAndKill();
                }
            }

        }
        static void stackAlgorithm()
        {
            bool moved = false;
            int direction = 0;
            int nonMove = 0;
            mazePlane[x, y] = "P";
            Boolean startMove = false;
            if (checkComplete() == true)
            {
                moved = true;
            }
            while (moved == false)
            {
                while (startMove == false)
                {
                    direction = randomNumber(1, 5);
                    if (testedDirections.Contains(direction))
                    {
                    }
                    else
                    {
                        startMove = true;
                    }

                }

                //right
                if (x < width - 1 && direction == 1 && mazePlane[x + 2, y] == "O")
                {
                    removeWall('R');
                    prevX.Push(x);
                    prevY.Push(y);
                    x = x + 2;
                    Array.Clear(testedDirections, 0, testedDirections.Length);
                    moved = true;
                }
                //left
                if (x > 0 && direction == 2 && mazePlane[x - 2, y] == "O")
                {
                    removeWall('L');
                    prevX.Push(x);
                    prevY.Push(y);
                    x = x - 2;
                    Array.Clear(testedDirections, 0, testedDirections.Length);
                    moved = true;
                }
                //down
                if (y < height - 1 && direction == 3 && mazePlane[x, y + 2] == "O")
                {
                    removeWall('D');
                    prevX.Push(x);
                    prevY.Push(y);
                    y = y + 2;
                    Array.Clear(testedDirections, 0, testedDirections.Length);
                    moved = true;
                }
                //up
                if (y > 0 && direction == 4 && mazePlane[x, y - 2] == "O")
                {
                    removeWall('U');
                    prevX.Push(x);
                    prevY.Push(y);
                    y = y - 2;
                    Array.Clear(testedDirections, 0, testedDirections.Length);

                    moved = true;

                }
                if (moved == false)
                {
                    testedDirections[nonMove] = direction;
                    nonMove++;
                    startMove = false;
                }
                if (nonMove > 3)
                {
                    nonMove = 0;
                    Array.Clear(testedDirections, 0, testedDirections.Length);
                    prevX.Pop();
                    prevY.Pop();
                    x = prevX.Peek();
                    y = prevY.Peek();
                }
            }
        }
        static void binaryTree()
        {
            int dir;
            for (int i = 0; i < height; i += 2)
            {
                for (int j = 0; j < width; j += 2)
                {
                    dir = randomNumber(1, 3);
                    Console.WriteLine(dir);
                    //west
                    if (dir == 1 && j > 0 || i == 0 && j > 0)
                    {
                        mazePlane[j - 1, i] = "P";

                    }
                    //north
                    else if (dir == 2 && i > 0 || j == 0 && i > 0)
                    {
                        mazePlane[j, i - 1] = "P";
                    }
                }
            }
        }
        static void huntAndKill()
        {
            bool moved = false;
            int direction = 0;
            int nonMove = 0;
            mazePlane[x, y] = "P";
            //Thread.Sleep(10);
            //Console.Clear();
            //printMaze();
            Boolean startMove = false;

            if (checkComplete() == true)
            {
                moved = true;
            }
            while (moved == false)
            {
                while (startMove == false)
                {
                    direction = randomNumber(1, 5);
                    if (testedDirections.Contains(direction))
                    {
                    }
                    else
                    {
                        startMove = true;
                    }

                }

                //right
                if (x < width - 1 && direction == 1 && mazePlane[x + 2, y] == "O")
                {
                    removeWall('R');
                    x = x + 2;
                    Array.Clear(testedDirections, 0, testedDirections.Length);
                    moved = true;
                }
                //left
                if (x > 0 && direction == 2 && mazePlane[x - 2, y] == "O")
                {
                    removeWall('L');
                    x = x - 2;
                    Array.Clear(testedDirections, 0, testedDirections.Length);
                    moved = true;
                }
                //down
                if (y < height - 1 && direction == 3 && mazePlane[x, y + 2] == "O")
                {
                    removeWall('D');
                    y = y + 2;
                    Array.Clear(testedDirections, 0, testedDirections.Length);
                    moved = true;
                }
                //up
                if (y > 0 && direction == 4 && mazePlane[x, y - 2] == "O")
                {
                    removeWall('U');
                    y = y - 2;
                    Array.Clear(testedDirections, 0, testedDirections.Length);
                    moved = true;

                }
                if (moved == false)
                {
                    testedDirections[nonMove] = direction;
                    nonMove++;
                    startMove = false;
                }
                if (nonMove > 3)
                {
                    nonMove = 0;
                    Array.Clear(testedDirections, 0, testedDirections.Length);
                    //hunt mode
                    searchEmptyCell();
                    moved = true;
                }
            }
        }
        static void printMaze()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (mazePlane[j, i] == "O")
                    {
                        Console.Write("██");
                        //Console.Write("P"); 
                    }
                    else if (mazePlane[j, i] == "X")
                    {
                        Console.Write("  ");
                        //Console.Write("X"); 
                    }
                    else if (mazePlane[j, i] == "G")
                    {
                        Console.Write("GG");
                        //Console.Write("X"); 
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
        }
        static int randomNumber(int start, int end)
        {
            Random rnd = new Random();
            return rnd.Next(start, end);
        }
        static Boolean checkComplete()
        {
            int unvisitedFound = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (mazePlane[j, i] == "O")
                        unvisitedFound++;
                }
            }
            //Console.WriteLine("unvisited found:"+unvisitedFound);
            //printMaze();
            if (unvisitedFound == 0)
                return true;
            else
                return false;
        }
        static void removeWall(Char dir)
        {
            switch (dir)
            {
                case 'R':
                    if (mazePlane[x + 2, y] == "O")
                        mazePlane[x + 1, y] = "P";
                    break;
                case 'L':
                    if (mazePlane[x - 2, y] == "O")
                        mazePlane[x - 1, y] = "P";
                    break;
                case 'D':
                    if (mazePlane[x, y + 2] == "O")
                        mazePlane[x, y + 1] = "P";
                    break;
                case 'U':
                    if (mazePlane[x, y - 2] == "O")
                        mazePlane[x, y - 1] = "P";
                    break;
            }
        }
        static void searchEmptyCell()
        {
            int test;
            Boolean run = true;
            for (int i = 0; i < height; i+=2)
            {
                if (run)
                {
                    for (int j = 0; j < width; j += 2)
                    {
                        if (mazePlane[j, i] == "O")
                        {

                            if (checkDir(j,i))
                            {

                                run = false;
                                break;
                            }
                            else
                            {

                            }
                        }
                    }

                }
                else
                {
                    break;
                }
            }
        }
        static Boolean checkDir(int xPos, int yPos)
        {
            int direction = 0;
            int[] testedDir = new int[4];
            int test = 0;

            while (test < 4)
            {
                Boolean startMove = false;
                while (startMove == false)
                {
                    direction = randomNumber(1, 5);
                    if (testedDir.Contains(direction))
                    {
                        if (test == 4)
                        {
                            startMove = true;
                        }
                    }
                    else
                    {
                        startMove = true;
                    }

                }
                switch (direction)
                {
                    case 1:
                        if(xPos < width - 1 && mazePlane[xPos + 2, yPos] == "P")
                        {
                            x = xPos;
                            y = yPos;
                            mazePlane[x, y] = "P";
                            mazePlane[x + 1, y] = "P";
                            Array.Clear(testedDir, 0, testedDir.Length);
                            return true;
                            test = 5;
                        }
                        else
                        {
                            testedDir[test] = direction;
                            test++;
                        }
                        break;
                    case 2:
                        if (xPos > 0 && mazePlane[xPos - 2, yPos] == "P")
                        {
                            x = xPos;
                            y = yPos;
                            mazePlane[x, y] = "P";
                            mazePlane[x-1, y] = "P";
                            Array.Clear(testedDir, 0, testedDir.Length);
                            return true;
                            test = 5;
                        }
                        else
                        {
                            testedDir[test] = direction;
                            test++;
                        }
                        break;
                    case 3:
                        if (yPos < height - 1 && mazePlane[xPos, yPos + 2] == "P")
                        {
                            x = xPos;
                            y = yPos;
                            mazePlane[x, y] = "P";
                            mazePlane[x, y + 1] = "P";
                            Array.Clear(testedDir, 0, testedDir.Length);
                            return true;
                            test = 5;
                        }
                        else
                        {
                            testedDir[test] = direction;
                            test++;
                        }
                        break;
                    case 4:
                        if (yPos > 0 && mazePlane[xPos, yPos - 2] == "P")
                        {
                            x = xPos;
                            y = yPos;
                            mazePlane[x, y] = "P";
                            mazePlane[x, y - 1] = "P";
                            Array.Clear(testedDir, 0, testedDir.Length);
                            return true;
                            test = 5;
                        }
                        else
                        {
                            testedDir[test] = direction;
                            test++;
                        }
                        break;                
                }
            }
            return false;
            Array.Clear(testedDir, 0, testedDir.Length);
        }
        static int randomEvenNumber(int start, int end, Boolean even)
        {
            Boolean run = true;
            int number = 0;
            while (run)
            {
                number = randomNumber(start, end);

                if (even == true && number%2 == 0)
                {
                    run = false;
                }
                else if (even == false && number % 2 >0)
                {
                    run = false;
                }
                
            }
            return number;
        }
        static void threeway()
        {

            Boolean run = true;
            int x = 0;
            int y = 0;
            while (run)
            {
                x = randomEvenNumber(3, 21, true);
                y = randomEvenNumber(3, 21, false);

                if (mazePlane[x, y] == "P" )
                {
                    continue;
                }

                //y Check
                if (mazePlane[x, y - 1] == "P" && mazePlane[x, y - 2] == "P" && mazePlane[x, y + 1] == "P" && mazePlane[x, y + 2] == "P")
                {
                    //mazePlane[x, y] = "G";
                    run = false;
                    //Console.WriteLine("y found");
                }
                //x check
                if(mazePlane[x-1, y] == "P" && mazePlane[x-2, y] == "P" && mazePlane[x+1, y] == "P" && mazePlane[x+2, y] == "P")
                {
                    //mazePlane[x, y] = "G";
                    run = false;
                    //Console.WriteLine("x found");
                }
            }
            mazePlane[x, y] = "P";
            //Console.WriteLine(x);
            //Console.WriteLine(y);
        }
        static void writeMaze()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (mazePlane[j, i] == "P")
                    {
                        writer.Write("0");
                        //Console.Write("P"); 
                    }
                    else if (mazePlane[j, i] == "X")
                    {
                        writer.Write("1");
                        //Console.Write("X"); Vägg
                    }
                    else
                    {
                        writer.Write("1");
                    }
                }
            }
        }
    }
}
