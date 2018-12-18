/*
 * Created by SharpDevelop.
 * User: Steve
 * Date: 12/11/2018
 * Time: 12:07 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace Day1
{
    /// <summary>
    /// Description of Day1.
    /// </summary>
    public static class Day1
    {
        public static void Main(string[] args)
        {
            StringReader reader = new StringReader(File.ReadAllText("frequency.txt"));
            int total = 0;
            int numRead;
            while(reader.Peek() != -1) {
                numRead = int.Parse(reader.ReadLine());
                total += numRead;
                //Console.Out.WriteLine("Current number read: " + numRead + ", Running total: " + total);
            }
            Console.Out.WriteLine("Final Frequency: " + total);
            Console.ReadKey();
        }
    }
}
