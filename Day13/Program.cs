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
		internal char symbol;
		public Point(char symbol, int x, int y) {
			if(dX != 0 && dY != 0) throw new ArgumentException("Arguments dX and dY cannot both be non-zero!");
			this.x = x;
			this.y = y;
			this.symbol = symbol;
		}
		
		public void move(char nextSymbol) {
			x += dX;
			y += dY;
		}
		
		public int distanceTo(Point p) {
			return Math.Abs(x - p.x) + Math.Abs(y - p.y);
		}
		
		public char getSymbol() {
			return symbol;
		}
		
		public int getX() { return x; }
		public int getY() { return y; }
	}

	class Program
	{
		public static void Main(string[] args)
		{
			HashSet<Point> points = new HashSet<Point>();
			var lines = File.ReadAllLines("test.txt");
			int numTurns
			
			char[,] display = new char[lines.Length, lines[0].Length];
			
			for(int i = 0; i<lines.Length; i +=1) {
				for(int j=0; j < lines[0].Length; j += 1) {
					display[i,j] = lines[i][j];
					if(isTrain(display[i,j])) {
						points.Add(new Point(display[i,j], i, j));
					}
				}
			}
			displayTracks(display);
			int turns = 1;
			foreach(Point p in points) {
				char nextChar = getNextSymbol(display, p);
				if(isTrain(nextChar)) {
					Console.WriteLine();
				}
			}
			
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}

		static char getNextSymbol(char[,] display, Point p)
		{
			switch(p.getSymbol) {
					case 'v': return display[p.getX() + 1, p.getY()];
					case '^': return display[p.getX() - 1, p.getY()];
					case '<': return display[p.getX(), p.getY() - 1];
					case '>': return display[p.getX(), p.getY() + 1];
			}
		}
		
		static void displayTracks(char[,] display){
			for(int i = 0; i < display.GetLength(0); i += 1) {
				for(int j = 0; j < display.GetLength(1); j += 1) {
					Console.Write(display[i,j]);
				}
				Console.WriteLine();
			}
		}
		
		static bool isTrain(char ch) {
			return ch == '^' || ch == 'v' || ch == '>' || ch == '<';
		}
	}
}