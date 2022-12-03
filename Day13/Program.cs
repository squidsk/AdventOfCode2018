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
    
    enum TurnDirection {
        Left,
        Straight,
        Right
    }
    
    class Point {
        internal int x;
        internal int y;
        internal char symbol;
        TurnDirection nextTurn;
        
        
        public Point(char symbol, int x, int y) {
            //if(dX != 0 && dY != 0) throw new ArgumentException("Arguments dX and dY cannot both be non-zero!");
            this.x = x;
            this.y = y;
            this.symbol = symbol;
            nextTurn = TurnDirection.Left;
        }
        
        public void move(char nextSymbol) {
            if(symbol == '^') {
                x = x - 1;
                if(nextSymbol == '\'') symbol = '<';
                else if(nextSymbol == '/') symbol = '>';
            } else if (symbol == 'v') {
                x = x + 1;
                if(nextSymbol == '\'') symbol = '>';
                else if(nextSymbol == '/') symbol = '<';
            } else if(symbol == '<') {
                y = y - 1;
                if(nextSymbol == '\'') symbol = '^';
                else if(nextSymbol == '/') symbol = 'v';
            } else { //if(symbol == '>') {
                y = y + 1;
                if(nextSymbol == '\'') symbol = 'v';
                else if(nextSymbol == '/') symbol = '^';
            }
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
    
    class PointComparer : IComparer<Point> {
        public int Compare(Point x, Point y) {
            if(x == default(Point) && y == default(Point)) return 0;
            if(x == default(Point)) return -1;
            if(y == default(Point)) return 1;
            if(x.getX() == y.getX() && x.getY() == y.getY()) return 0;
            if(x.getX() < y.getX() || (x.getX() == y.getX() && x.getY() < y.getY())) return -1;
            return 1;
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            SortedSet<Point> sortedPoints = new SortedSet<Point>(new PointComparer());
            HashSet<Point> points = new HashSet<Point>();
            var lines = File.ReadAllLines("test.txt");
            int numTurns = 0;
            
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
                int currentX = p.getX();
                int currentY = p.getY();
                char replacementChar = getReplacementTrack(currentX, currentY);
                p.move(nextChar);
                if(isTrain(nextChar)) {
                    Console.WriteLine("Train crash at location X:{0}, Y:{1}", p.getX(),p.getY());
                } else {
                    
                }
            }
            
            
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static char getReplacementTrack(int x, int y)
        {
            if(isTrackAbove(x, y) && isTrackBelow(x,y) && isTrackLeft(x,y) && isTrackRight(x,y)) return '+';
            
        }

        static char getNextSymbol(char[,] display, Point p)
        {
            switch(p.getSymbol()) {
                    case 'v': return display[p.getX() + 1, p.getY()];
                    case '^': return display[p.getX() - 1, p.getY()];
                    case '<': return display[p.getX(), p.getY() - 1];
                    case '>': return display[p.getX(), p.getY() + 1];
                    default: return '*';
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