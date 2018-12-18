/*
 * Created by SharpDevelop.
 * User: Steve
 * Date: 12/18/2018
 * Time: 12:13 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day6b
{
    struct Point {
        internal int X;
        internal int Y;
        public Point(int x, int y) { X = x; Y = y;}
    }
    
    class Program
    {
        private const int BOARD_SIZE = 380;
        private const int MAX_DISTANCE = 10000;
        
        public static void Main(string[] args)
        {
            StringReader reader = new StringReader(File.ReadAllText("input.txt"));
            int[,] board = new int[BOARD_SIZE,BOARD_SIZE];
            HashSet<Point> h = new HashSet<Point>();
            
            while(reader.Peek() != -1) {
                string line = reader.ReadLine();
                string[] sLine = line.Split(',');
                h.Add(new Point(int.Parse(sLine[1]), int.Parse(sLine[0])));
            }
            
            calcDistance(board, h);

            
            int maxArea = countClosestPoints(board);
            


            Console.WriteLine("The area containing all locations which have a total distance to all given coordinates of less than {0} is {1}.", MAX_DISTANCE, maxArea);
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static int countClosestPoints(int[,] board) {
            int count = 0;
            for(int i = 0; i < BOARD_SIZE; i += 1) {
                for(int j = 0; j < BOARD_SIZE; j += 1) {
                    if(board[i, j] < MAX_DISTANCE) count += 1;
                }
            }
            return count;
        }
        
        static int calcManhattanDistance(int col1, int row1, int col2, int row2) {
            return Math.Abs(col1 - col2) + Math.Abs(row1 - row2);
        }
        
        static void calcDistance(int[,] board, HashSet<Point> points) {
            for(int i = 0; i < BOARD_SIZE; i += 1) {
                for(int j = 0; j < BOARD_SIZE; j += 1) {
                    foreach(Point p in points) {
                        int distance = calcManhattanDistance(p.Y, p.X, i, j);
                        board[i, j] += distance;
                    }
                }
            }
        }
    }
}