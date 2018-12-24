/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/23/2018
 * Time: 5:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day11
{
	class Program
	{
		const int SERIAL_NUMBER = 6548;
		const int SIZE = 300;
	
		public static void Main(string[] args)
		{
			int[,] board = new int[SIZE,SIZE];
			
			for(int i = 0; i < SIZE; i += 1) {
				for(int j = 0; j < SIZE; j += 1) {
					int rackID = (i+1) + 10;
					int powerLevel = (j+1) * rackID;
					powerLevel += SERIAL_NUMBER;
					powerLevel *= rackID;
					board[i, j] = ((powerLevel / 100) % 10) - 5;
				}
			}
			
			int maxX=0, maxY=0;
			int maxPower = -1;
			
			for(int i = 0; i < SIZE - 3; i += 1) {
				for(int j = 0; j < SIZE - 3; j += 1) {
					int power = 0;
					for(int k = 0; k < 3; k += 1) {
						for(int l = 0; l < 3; l += 1) {
							power += board[i+k, j+l];
						}
					}

					if(power > maxPower) {
						maxPower = power;
						maxX = i;
						maxY = j;
					}
				}
			}
			
			Console.WriteLine("The power level of position {0},{1} is: {2}", maxX+1, maxY+1, maxPower);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}