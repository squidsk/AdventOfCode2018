/*
 * Created by SharpDevelop.
 * User: Steve
 * Date: 12/11/2018
 * Time: 1:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day2b
{
    class Program
    {
        private const int NUM_LETTERS = 26;
        public static void Main(string[] args)
        {
            StringReader reader = new StringReader(File.ReadAllText("input.txt"));
            List<string> input = new List<string>();
            char[] line = new char[NUM_LETTERS];
            char[] line2 = new char[NUM_LETTERS];
            int matchPos = 0;
            int matchedLine = 0;
            
            while(reader.Peek() != -1) {
                input.Add(reader.ReadLine());
            }
            for(int i = 0; i < input.Count; i += 1) {
                input[i].CopyTo(0, line, 0, NUM_LETTERS);
                for(int j = i+1; j < input.Count; j += 1) {
                    input[j].CopyTo(0, line2, 0, NUM_LETTERS);
                    int letterCount = 0;
                    for(int k = 0; k < NUM_LETTERS; k += 1){
                        if(line[k] != line2[k]) {
                            letterCount += 1;
                            matchPos = k;
                        }
                    }
                    if(letterCount == 1) {
                        matchedLine = i;
                        goto End;
                    }
                }
            }
        End:
            Console.WriteLine("Matched Boxes: " + input[matchedLine].Substring(0,matchPos) + input[matchedLine].Substring(matchPos+1));
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}