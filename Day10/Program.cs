/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/22/2018
 * Time: 5:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day10
{
	class Point {
		internal int x;
		internal int y;
		internal int dX;
		internal int dY;
		public Point(int x, int y, int dX, int dY) {
			this.x = x;
			this.y = y;
			this.dX = dX;
			this.dY = dY;
		}
		
		public void move() {
			x += dX;
			y += dY;
		}
		
		public int distanceTo(Point p) {
			return Math.Abs(x - p.x) + Math.Abs(y - p.y);
		}
	}
	class Program
	{
		const string POINT_REGEX = @"position=< ?(-?\d+),  ?(-?\d+)> velocity=< ?(-?\d+),  ?(-?\d+)>";
		public static void Main(string[] args)
		{
			StringReader reader = new StringReader(File.ReadAllText("input.txt"));
			HashSet<Point> points = new HashSet<Point>();
			int minX=int.MaxValue, minY=int.MaxValue, maxX=int.MinValue, maxY=int.MinValue;
			
			while(reader.Peek() != -1) {
				var match = Regex.Match(reader.ReadLine(), POINT_REGEX);
				Point p = new Point(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
				points.Add(p);
			}
			int time;
			for(time = 1; true; time += 1) {
				points.ForEach(p => p.move());
				int maxDistance = -1;
				foreach(Point p1 in points) {
					int curMinDis = int.MaxValue;
					foreach(Point p2 in points) {
						if(p1 != p2) {
							if(curMinDis > p1.distanceTo(p2)) curMinDis = p1.distanceTo(p2);
						}
					}
					if(maxDistance < curMinDis) maxDistance = curMinDis;
				}
				if(maxDistance <= 2) break;
			}

			foreach(Point p in points) {
				if(p.x < minX) minX = p.x;
				else if(p.x > maxX) maxX = p.x;
				
				if(p.y < minY) minY = p.y;
				else if(p.y > maxY) maxY = p.y;
			}
			
			char[,] display = new char[Math.Abs(maxY - minY) + 1, Math.Abs(maxX - minX) + 1];
			for(int i = minX; i <= maxX; i += 1) {
				for(int j = minY; j <= maxY; j += 1) {
					display[j - minY, i - minX] = ' ';
				}
			}
			foreach(Point p in points) {
				display[p.y - minY, p.x - minX] = '#';
			}
			for(int i = 0; i < display.GetLength(0); i += 1) {
				for(int j = 0; j < display.GetLength(1); j += 1) {
					Console.Write(display[i,j]);
				}
				Console.WriteLine();
			}
			
			Console.WriteLine("The points are closest at time: {0}.", time);
			Console.WriteLine("Min X: {0}, Max X: {1}, Min Y: {2}, Max Y: {3}", minX, maxX, minY, maxY);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
	static class LinqExtensions {
		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action) {
		    foreach(T item in enumeration)
		    {
		    	if(item != null) {
		        	action(item);
		    	}
		    }
		}
	}
}