/*
 * Created by SharpDevelop.
 * User: Steve
 * Date: 12/11/2018
 * Time: 12:52 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day2
{
    class Program
    {
        private const int NUM_LETTERS = 26;
        public static void Main(string[] args)
        {
            StringReader reader = new StringReader(File.ReadAllText("input.txt"));
            HashSet<char> alreadyProcessed = new HashSet<char>();
            int twoLetterCount = 0;
            int threeLetterCount = 0;
            char[] line = new char[NUM_LETTERS];
            
            while(reader.Peek() != -1) {
                reader.ReadLine().CopyTo(0, line, 0, NUM_LETTERS);
                bool twoLetters = false;
                bool threeLetters = false;
                for(int i = 0; i < NUM_LETTERS; i += 1) {
                    if(alreadyProcessed.Contains(line[i])) continue;
                    alreadyProcessed.Add(line[i]);
                    int letterCount = 1;
                    for(int j = i+1; j < NUM_LETTERS; j += 1){
                        if(line[i] == line[j]) letterCount += 1;
                    }
                    twoLetters |= letterCount == 2;
                    threeLetters |= letterCount == 3;
                }
                if(twoLetters) twoLetterCount += 1;
                if(threeLetters) threeLetterCount += 1;
                alreadyProcessed.Clear();
            }

            Console.WriteLine("Checksum: " + twoLetterCount*threeLetterCount);
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}