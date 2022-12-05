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
using System.Linq;

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
        internal char lastTrack;
        TurnDirection nextTurn;
        internal bool hasCrashed;
        
        
        public Point(char symbol, int x, int y) {
            //if(dX != 0 && dY != 0) throw new ArgumentException("Arguments dX and dY cannot both be non-zero!");
            this.x = x;
            this.y = y;
            this.symbol = symbol;
            if(symbol == 'v' || symbol == '^') lastTrack = '|';
            else lastTrack = '-';
            nextTurn = TurnDirection.Left;
            hasCrashed = false;
        }
        
        public void move(char nextTrack) {
            if(symbol == '^') {
                x = x - 1;
                if(nextTrack == '\\') symbol = '<';
                else if(nextTrack == '/') symbol = '>';
                else if(nextTrack == '+') {
                    if(nextTurn == TurnDirection.Left) {
                        nextTurn = TurnDirection.Straight;
                        symbol = '<';
                    } else if (nextTurn == TurnDirection.Right) {
                        nextTurn = TurnDirection.Left;
                        symbol = '>';
                    } else {
                        nextTurn = TurnDirection.Right;
                    }
                }
            } else if (symbol == 'v') {
                x = x + 1;
                if(nextTrack == '\\') symbol = '>';
                else if(nextTrack == '/') symbol = '<';
                else if(nextTrack == '+') {
                    if(nextTurn == TurnDirection.Left) {
                        nextTurn = TurnDirection.Straight;
                        symbol = '>';
                    } else if (nextTurn == TurnDirection.Right) {
                        nextTurn = TurnDirection.Left;
                        symbol = '<';
                    } else {
                        nextTurn = TurnDirection.Right;
                    }
                }
            } else if(symbol == '<') {
                y = y - 1;
                if(nextTrack == '\\') symbol = '^';
                else if(nextTrack == '/') symbol = 'v';
                else if(nextTrack == '+') {
                    if(nextTurn == TurnDirection.Left) {
                        nextTurn = TurnDirection.Straight;
                        symbol = 'v';
                    } else if (nextTurn == TurnDirection.Right) {
                        nextTurn = TurnDirection.Left;
                        symbol = '^';
                    } else {
                        nextTurn = TurnDirection.Right;
                    }
                }
            } else { //if(symbol == '>') {
                y = y + 1;
                if(nextTrack == '\\') symbol = 'v';
                else if(nextTrack == '/') symbol = '^';
                else if(nextTrack == '+') {
                    if(nextTurn == TurnDirection.Left) {
                        nextTurn = TurnDirection.Straight;
                        symbol = '^';
                    } else if (nextTurn == TurnDirection.Right) {
                        nextTurn = TurnDirection.Left;
                        symbol = 'v';
                    } else {
                        nextTurn = TurnDirection.Right;
                    }
                }
            }
            lastTrack = nextTrack;
        }
        
        public int distanceTo(Point p) {
            return Math.Abs(x - p.x) + Math.Abs(y - p.y);
        }
        
    }
    
    class PointComparer : IComparer<Point> {
        public int Compare(Point p1, Point p2) {
            if(p1 == default(Point) && p2 == default(Point)) return 0;
            if(p1 == default(Point)) return -1;
            if(p2 == default(Point)) return 1;
            if(p1.x == p2.x && p1.y == p2.y) return 0;
            if(p1.x < p2.x || (p1.x == p2.x && p1.y < p2.y)) return -1;
            return 1;
        }
    }

    class Program
    {
        static int maxWidth;
        
        public static void Main(string[] args)
        {
            //Part1("test.txt");
            //Part1("input.txt");
            Part2("test4.txt");
            Part2("input.txt");
            
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static void Part1(string filename)
        {
            SortedSet<Point> sortedPoints = new SortedSet<Point>(new PointComparer());
            var lines = File.ReadAllLines(filename);
            
            char[,] display = new char[lines.Length, lines[0].Length];
            
            for(int i = 0; i<lines.Length; i +=1) {
                for(int j=0; j < lines[0].Length; j += 1) {
                    display[i,j] = lines[i][j];
                    if(isTrain(display[i,j])) {
                        sortedPoints.Add(new Point(display[i,j], i, j));
                    }
                }
            }
            displayTracks(display);
            int turns = 1;
            bool crash = false;
            
            while(!crash){
                SortedSet<Point> tempPoints = new SortedSet<Point>(new PointComparer());
                foreach(Point p in sortedPoints) {
                    char nextChar = getNextSymbol(display, p);
                    int currentX = p.x;
                    int currentY = p.y;
                    char lastTrack = p.lastTrack;
                    p.move(nextChar);
                    display[currentX, currentY] = lastTrack;
                    if(isTrain(nextChar)) {
                        display[p.x,p.y] = 'X';
                        displayTracks(display);
                        Console.WriteLine("On turn {2}, trains crashed at location X:{0}, Y:{1}", p.x,p.y, turns);
                        crash = true;
                    } else {
                        display[p.x,p.y] = p.symbol;
                    }
                    //displayTracks(display);
                    if(crash) break;
                    tempPoints.Add(p);
                }
                sortedPoints = tempPoints;
                turns += 1;
            }
        }

        static void Part2(string filename)
        {
            SortedSet<Point> sortedPoints = new SortedSet<Point>(new PointComparer());
            var lines = File.ReadAllLines(filename);
            
            char[,] display = new char[lines.Length, lines[0].Length];
            maxWidth = lines[0].Length;
            
            for(int i = 0; i<lines.Length; i +=1) {
                for(int j=0; j < lines[0].Length; j += 1) {
                    display[i,j] = lines[i][j];
                    if(isTrain(display[i,j])) {
                        sortedPoints.Add(new Point(display[i,j], i, j));
                    }
                }
            }
            displayTracks(display);
            int turns = 1;
            bool crash = false;
            
            while(sortedPoints.Count > 1){
                SortedSet<Point> tempPoints = new SortedSet<Point>(new PointComparer());
                HashSet<Point> crashPoints = new HashSet<Point>();
                foreach(Point p in sortedPoints) {
                    crash = false;
                    if(!crashPoints.Any(m => m.x == p.x && m.y == p.y)) {
                        char nextChar = getNextSymbol(display, p);
                        int currentX = p.x;
                        int currentY = p.y;
                        char lastTrack = p.lastTrack;
                        p.move(nextChar);
                        display[currentX, currentY] = lastTrack;
                        if(isTrain(nextChar)) {
                            display[p.x,p.y] = sortedPoints.First(m => m != p && m.x == p.x && m.y == p.y).lastTrack; //getReplacementTrack(p, display);
                            //Console.WriteLine("On turn {2}, trains crashed at location X:{0}, Y:{1}", p.x,p.y, turns);
                            crash = true;
                        } else {
                            display[p.x,p.y] = p.symbol;
                        }
                        //displayTracks(display);
                        if(crash){
                            crashPoints.Add(p);
                            tempPoints.RemoveWhere(m => m.x == p.x && m.y == p.y);
                        } else {
                            tempPoints.Add(p);
                        }
                    } else {
                        crashPoints.RemoveWhere(m => m.x == p.x && m.y == p.y);
                    }
                }
                //Console.WriteLine("End of Turn {0}", turns);
                //displayTracks(display);
                sortedPoints = tempPoints;
                turns += 1;
            }
            displayTracks(display);
            Console.WriteLine("On turn {2}, there is one train left at location X:{0}, Y:{1}", sortedPoints.First().x,sortedPoints.First().y, turns);
        }

        static bool isTrackLeft(Point p, char[,] display) {
            return p.y > 0 && isTrack(display[p.x, p.y-1]);
        }
        
        static bool isTrackRight(Point p, char[,] display) {
            return p.y < maxWidth -1 && isTrack(display[p.x, p.y+1]);
        }
        
        static bool isTrackAbove(Point p, char[,] display) {
            return p.x > 0 && isTrack(display[p.x-1, p.y]);
        }
        
        static char getReplacementTrack(Point p, char[,] display) {
            char track;
            if(isTrackLeft(p, display)) {
                if(isTrackAbove(p,display)) {
                    if(isTrackRight(p,display)) {
                        track = '+';
                    } else {
                        track = '/';
                    }
                } else if(isTrackRight(p, display)) {
                    track = '-';
                } else {
                    track = '\\';
                }
            } else {
                if(isTrackAbove(p,display)) {
                    if(isTrackRight(p,display)) {
                        track = '\\';
                    } else {
                        track = '|';
                    }
                } else {
                    track = '/';
                }
            }
            return track;
        }
        
        
        static char getNextSymbol(char[,] display, Point p)
        {
            switch(p.symbol) {
                    case 'v': return display[p.x + 1, p.y];
                    case '^': return display[p.x - 1, p.y];
                    case '<': return display[p.x, p.y - 1];
                    case '>': return display[p.x, p.y + 1];
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
            Console.WriteLine();
        }
        
        static bool isTrain(char ch) {
            return ch == '^' || ch == 'v' || ch == '>' || ch == '<';
        }
        
        static bool isTrack(char ch) {
            return ch == '|' || ch == '-' || ch == '+' || ch == '\\' || ch == '/';
        }
    }
}