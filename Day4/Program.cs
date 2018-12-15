/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/13/2018
 * Time: 2:47 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day4
{
    
    class Program
    {
        private const string DATE_REGEX = @"\[\d{4}\-(\d{2}\-\d{2} \d{2}:\d{2})\]";
        private const string NEW_GUARD_REGEX =  @"\[\d{4}\-(\d{2}\-\d{2}) \d{2}:\d{2}\] Guard \#(\d+) begins shift";
        private const string SLEEP_REGEX = @"\[\d{4}\-(\d{2}\-\d{2}) \d{2}:(\d{2})\] falls asleep";
        private const string WAKES_REGEX = @"\[\d{4}\-(\d{2}\-\d{2}) \d{2}:(\d{2})\] wakes up";
        private const int MINUTES_IN_HOUR = 60;
        
        
        public static void Main(string[] args)
        {
            StringReader reader = new StringReader(File.ReadAllText("input.txt"));
            Dictionary<string, int[]> records = new Dictionary<string, int[]>();
            Dictionary<string, int> totalTime = new Dictionary<string, int>();
            Dictionary<string, string> input = new Dictionary<string, string>();
            List<string> sortedKeys = new List<string>();
            
            string line;
            
            while(reader.Peek() != -1) {
                line = reader.ReadLine();
                var match = Regex.Match(line, DATE_REGEX);
                var date = match.Groups[1].Value;

                input.Add(date, line);
                sortedKeys.Add(date);
            }
            
            sortedKeys.Sort();

            for(int i = 0; i < sortedKeys.Count; ) {
                line = input[sortedKeys[i]];
                var match = Regex.Match(line, NEW_GUARD_REGEX);
                
                if(match.Success) {
                    int[] minutesSlept;
                    
                    var guard = match.Groups[2].Value;
                    if(records.ContainsKey(guard)) {
                        minutesSlept = records[guard];
                    } else {
                        minutesSlept = new int[MINUTES_IN_HOUR];
                        totalTime[guard] = 0;
                    }
                    
                    line = input[sortedKeys[++i]];
                    while(i < sortedKeys.Count && !Regex.IsMatch(line, NEW_GUARD_REGEX)) {
                        match = Regex.Match(line, SLEEP_REGEX);
                        int min1 = int.Parse(match.Groups[2].Value);
                        int min2;
                        line = input[sortedKeys[++i]];
                        match = Regex.Match(line, WAKES_REGEX);
                        min2 = int.Parse(match.Groups[2].Value);
                        
                        totalTime[guard] += (min2-min1);
                        for(int j = min1; j < min2; j += 1) {
                            minutesSlept[j] += 1;
                        }
                        
                        records[guard] = minutesSlept;
                        i+=1;
                        if(i < sortedKeys.Count) line = input[sortedKeys[i]];
                    }
                } 
            }

            int max = -1;
            string guardNum = null;
            
            foreach(String g in totalTime.Keys) {
                if(max < totalTime[g]){
                    guardNum = g;
                    max = totalTime[g];
                }
            }

            Console.WriteLine("Guard {0} slept for a total of {1} minutes.", guardNum, max);
            
            int maxMinute = -1;
            max = -1;
            int[] sleepArray = records[guardNum];
            for(int i = 0; i < MINUTES_IN_HOUR; i += 1) {
                if(max < sleepArray[i]) {
                    max = sleepArray[i];
                    maxMinute = i;
                }
            }
            
            Console.WriteLine("Guard {0} was asleep most during minute {1}.", guardNum, maxMinute);
            Console.WriteLine("The ID * minute = {0} * {1} = {2}.", int.Parse(guardNum), maxMinute, int.Parse(guardNum)*maxMinute);
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}