using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Day12 {
    class Pot {
        public string curState;
        public string nextState;
        public int generationsOff;

        public Pot(string curState) {
            this.curState = curState;
            nextState = ".";
            generationsOff = 0;
        }

        public void SetNextState(string state) {
            generationsOff = state.Equals(".") ? generationsOff + 1 : 0;
        }
    }
    
    
    internal class Program {
        private const string STATE = @"#....##.#.#.####..#.######..##.#.########..#...##...##...##.#.#...######.###....#...##..#.#....##.##";
        private const string TSTATE = @"#..#.#..##......###...###";
        private const string GROWTH_REGEX = @"^([\.#]{5}) => ([\.#])$";
        private const long NUM_GENERATIONS = 50000000000;
        
        public static void Main(string[] args) {
            Dictionary<string, string> rules = new Dictionary<string, string>();
            Dictionary<long, Pot> pots = new Dictionary<long, Pot>();
            StringReader reader = new StringReader(File.ReadAllText("input.txt"));
			
            while(reader.Peek() != -1) {
                var match = Regex.Match(reader.ReadLine(), GROWTH_REGEX);
                rules.Add(match.Groups[1].Value, match.Groups[2].Value);
            }
            long maxPot = 0;
            long minPossiblePot = -2;
            long minPot = 0;
            foreach(char ch in STATE) {
                pots.Add(maxPot, new Pot(ch.ToString()));
                maxPot += 1;
            }

            for(int i = 1; i <= 4; i += 1) {
                pots.Add(-i, new Pot("."));
                pots.Add(maxPot + (i-1), new Pot("."));
            }

            long maxPossiblePot = maxPot + 2;

            PrintCurrentState(pots, minPot, maxPot);
            for(long i = 0; i < NUM_GENERATIONS; i += 1) {
                for(long j = minPossiblePot; j < maxPossiblePot; j += 1) {
                    String pattern = GetPattern(j, pots);
                    if(rules.ContainsKey(pattern)) {
                        pots[j].nextState = rules[pattern];
                        if(pots[j].nextState.Equals("#"))
                            UpdateForNewPots(j, pots, ref minPot, ref maxPot);
                    } else {
                        pots[j].nextState = ".";
                    }
                }
                UpdateStateOf(pots, minPot, maxPot);

                maxPossiblePot = maxPot + 2;
                minPossiblePot = minPot - 2;
                
                
            }

            PrintCurrentState(pots, minPot, maxPot);
            long sum = 0;
            for(long i = minPot; i < maxPot; i += 1) {
                if(pots[i].curState.Equals("#")) {
                    sum += i;
                }
            }
            Console.WriteLine("The number of plant-containing pots after 20th generation is {0}.", sum);
        }

        private static long UpdateForNewPots(long potNum, Dictionary<long, Pot> pots, ref long minPot, ref long maxPot) {
            if(potNum < minPot) {
                pots.Add(potNum - 4, new Pot("."));
                if(potNum == minPot - 2) pots.Add(potNum - 3, new Pot("."));
                minPot = potNum;
            } else if(potNum >= maxPot) {
                pots.Add(potNum + 4, new Pot("."));
                if(potNum == maxPot + 1) pots.Add(potNum + 3, new Pot("."));
                maxPot = potNum + 1;
            }

            return minPot;
        }

        private static void UpdateStateOf(Dictionary<long, Pot> pots, long minPot, long maxPot) {
            for(long j = minPot; j < maxPot; j += 1) {
                pots[j].curState = pots[j].nextState;
            }
        }

        private static void PrintCurrentState(Dictionary<long, Pot> pots, long minPot, long maxPot) {
            Console.WriteLine("Min pot: {0}, Max pot: {1}",minPot, maxPot-1);
            for(long i = minPot; i < maxPot; i += 1) {
                Console.Write(pots[i].curState);
            }

            Console.WriteLine();
        }

        private static string GetPattern(long pot, Dictionary<long, Pot> pots) {
            String pattern = "";
            for(long i = pot - 2; i <= pot + 2; i += 1) {
                pattern += pots[i].curState;
            }

            return pattern;
        }
    }
}