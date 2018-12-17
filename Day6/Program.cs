/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/17/2018
 * Time: 10:01 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day6
{
    
    struct BoardSpot {
        internal int distance;
        internal int pointNumber;
    }
    class Program
    {
        private const int BOARD_SIZE = 10;
        private const int NO_POINT = -1;
        
        public static void Main(string[] args)
        {
            StringReader reader = new StringReader(File.ReadAllText("input.txt"));
            BoardSpot[,] board = new BoardSpot[BOARD_SIZE,BOARD_SIZE];
            int pointCounter = 0;
            
            for(int i = 0; i < BOARD_SIZE; i += 1) {
                for(int j = 0; j < BOARD_SIZE; j += 1) {
                    board[i,j].distance = BOARD_SIZE;
                    board[i,j].pointNumber = NO_POINT;
                }
            }
            
            while(reader.Peek() != -1) {
                string line = reader.ReadLine();
                string[] sLine = line.Split(',');
                
                calcDistance(board, int.Parse(sLine[0]), int.Parse(sLine[1]), pointCounter);
                pointCounter += 1;
            }

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static int calcManhattanDistance(int col1, int row1, int col2, int row2) {
            return Math.Abs(col1 - col2) + Math.Abs(row1, row2);
        }
        
        static void calcDistance(BoardSpot[,] board, int col, int row, int pointCounter) {
            for(int i = 0; i < BOARD_SIZE; i += 1) {
                for(int j = 0; j < BOARD_SIZE; j += 1) {
                    int distance = calcManhattanDistance(col, row, i, j);
                    if(board[i,j].distance > distance) {
                        board[i,j].distance = distance;
                        board[i,j].pointNumber = pointCounter;
                    }
                }
            }
        }
    }
}