/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/21/2018
 * Time: 10:48 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day9
{
	class Program
	{
		const int MAX_MARBLE = 7130700;
		const int NUM_PLAYERS = 439;
		
		public static void Main(string[] args)
		{
			List<int> marbleList = new List<int>();
			long[] playerScores = new long[NUM_PLAYERS];
			int currentPlayer = 2;
			int currentPosition = 1;
			marbleList.Add(0);
			marbleList.Add(1);
			
			for(int currentMarble = 2; currentMarble <= MAX_MARBLE; currentMarble += 1, currentPlayer = (currentPlayer + 1) % NUM_PLAYERS) {
				if(currentMarble % 23 != 0) {
					currentPosition = (currentPosition + 2) % marbleList.Count;
					if(currentPosition == 0) currentPosition = marbleList.Count;

					marbleList.Insert(currentPosition, currentMarble);
				} else {
					currentPosition = (currentPosition - 7 + marbleList.Count) % marbleList.Count;
					int removedMarble = marbleList[currentPosition];
					marbleList.RemoveAt(currentPosition);
					playerScores[currentPlayer] += currentMarble + removedMarble;
				}
			}
			long winner = playerScores.Max();
			Console.WriteLine("The winning elf's score is: {0}", winner);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}