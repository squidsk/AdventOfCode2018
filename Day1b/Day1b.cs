/*
 * Created by SharpDevelop.
 * User: Steve
 * Date: 12/11/2018
 * Time: 12:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day1b
{
    /// <summary>
    /// Description of Day1b.
    /// </summary>
    public class Day1b
    {
        public static void Main(string[] args)
        {
            StringReader reader = new StringReader(File.ReadAllText("frequency.txt"));
            Queue<int> q = new Queue<int>();
            HashSet<int> h = new HashSet<int>();
            bool notFound = true;
            int total = 0;
            int numRead;
            h.Add(total);
            while(reader.Peek() != -1 && notFound) {
                numRead = int.Parse(reader.ReadLine());
                total += numRead;

                if(h.Contains(total)) notFound = false;

                q.Enqueue(numRead);
                h.Add(total);
            }

            while(notFound) {
                foreach(int i in q) {
                    total += i;
                    if(h.Contains(total)) {
                        notFound = false;
                        break;
                    }
                    h.Add(total);
                }
            }
            Console.Out.WriteLine("First Duplicate: " + total);
            Console.ReadKey();
        }
    }
}
