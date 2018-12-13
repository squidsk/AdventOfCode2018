/*
 * Created by SharpDevelop.
 * User: Steve
 * Date: 12/11/2018
 * Time: 1:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Day3
{
    class Program
    {
        private const int BOARD_SIZE = 1000;
        public static void Main(string[] args)
        {
            StringReader reader = new StringReader(File.ReadAllText("input.txt"));
            int[,] board = new int[BOARD_SIZE, BOARD_SIZE];
            HashSet<int> noOverlap = new HashSet<int>();
            int numOverlaps = 0;
            int lineNumber = 1;
            
            while(reader.Peek() != -1) {
                string line = reader.ReadLine();
                lineNumber += 1;
                var match = Regex.Match(line, @"\#(\d+) \@ (\d+)\,(\d+)\: (\d+)x(\d+)");
                var id = int.Parse(match.Groups[1].Value);
                var left = int.Parse(match.Groups[2].Value);
                var top = int.Parse(match.Groups[3].Value);
                var width = int.Parse(match.Groups[4].Value);
                var height = int.Parse(match.Groups[5].Value);
                
                //Console.WriteLine("ID: {0}, Left: {1}, Top: {2}, Width: {3}, Height: {4}", id, left, top, width, height);
                
                noOverlap.Add(id);
                
                for(int i = top; i< top + height; i += 1) {
                    for(int j = left; j < left + width; j += 1) {
                        if(board[i, j] == 0) {
                            board[i, j] = id;
                        } else if(board[i, j] > 0) {
                            noOverlap.Remove(id);
                            noOverlap.Remove(board[i, j]);
                            board[i, j] = -1;
                            numOverlaps += 1;
                        } else {
                            noOverlap.Remove(id);
                        }
                    }
                }
            }
            
            Console.WriteLine("Number of Overlaps: {0}", numOverlaps);
            foreach(int i in noOverlap) {
                Console.WriteLine("Item: {0}", i);
            }
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}