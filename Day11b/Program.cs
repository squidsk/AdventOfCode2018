/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/23/2018
 * Time: 8:34 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Day11b
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
			int maxSize = -1;
			
			for(int size = 1; size <= SIZE; size += 1) {
				for(int i = 0; i < SIZE - (size-1); i += 1) {
					for(int j = 0; j < SIZE - (size-1); j += 1) {
						int power = 0;
						for(int k = 0; k < size; k += 1) {
							for(int l = 0; l < size; l += 1) {
								power += board[i+k, j+l];
							}
						}
	
						if(power > maxPower) {
							maxPower = power;
							maxX = i;
							maxY = j;
							maxSize = size;
						}
					}
				}
			}
			
			Console.WriteLine("The power level of position ({0}, {1}) with size {2} is: {3}", maxX+1, maxY+1, maxSize, maxPower);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}