/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/9/2019
 * Time: 4:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace Day13 {
	
	class Point {
		internal int x;
		internal int y;
		internal int dX;
		internal int dY;
		public Point(char symbol, int x, int y, int dX, int dY) {
			if(dX != 0 && dY != 0) throw new ArgumentException("Arguments dX and dY cannot both be non-zero!");
			this.x = x;
			this.y = y;
			this.dX = dX;
			this.dY = dY;
		}
		
		public void move(char nextSymbol) {
			x += dX;
			y += dY;
		}
		
		public int distanceTo(Point p) {
			return Math.Abs(x - p.x) + Math.Abs(y - p.y);
		}
		
		public char getSymbol() {
			if(dX == 1) return '>';
			if(dX == -1) return '<';
			return dY == 1 ? 'v' : '^';
		}
	}

	class Program
	{
		public static void Main(string[] args)
		{
			HashSet<Point> points = new HashSet<Point>();
			var lines = File.ReadAllLines("test.txt");
			
			char[,] display = new char[lines.Length, lines[0].Length];
			
			for(int i = 0; i<lines.Length; i +=1) {
				for(int j=0; j < lines[0].Length; j += 1) {
					display[i,j] = lines[i][j];
				}
			}

			for(int i = 0; i < display.GetLength(0); i += 1) {
				for(int j = 0; j < display.GetLength(1); j += 1) {
					Console.Write(display[i,j]);
				}
				Console.WriteLine();
			}
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}