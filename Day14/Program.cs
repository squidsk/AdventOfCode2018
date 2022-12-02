/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/1/2022
 * Time: 11:41 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Day14
{
	class Program
	{
		public static void Main(string[] args)
		{
			List<int> scores = new List<int>();
			int elf1Recipe = 0;
			int elf2Recipe = 1;
			const int numInterations = 880751;
			int[] iResult= {8,8,0,7,5,1};
			int index = 0;
			int j = 0;
			scores.Add(3);
			scores.Add(7);
			/*scores.Add(8);
			scores.Add(8);
			scores.Add(0);
			scores.Add(7);
			scores.Add(5);
			scores.Add(1);*/
			printList(scores, elf1Recipe, elf2Recipe);
			
			for(int i=2; j < iResult.Length; i+=1){
				int newRecipee = scores[elf1Recipe] + scores[elf2Recipe];
				int tens = newRecipee/10;
				int ones = newRecipee%10;
				try{
				if(tens > 0) {
					scores.Add(tens);
					if(j < 6) {
						if(scores[i] == iResult[j]) {
							j+=1;
						} else {
							j = 0;
						}
					}
					i+=1;
				}
				scores.Add(ones);
				if(j < iResult.Length) {
					if(scores[i] == iResult[j]) {
						j+=1;
					} else {
						j = 0;
					}
				}
				if(j==iResult.Length){
					index = i - iResult.Length;
					Console.WriteLine("Index is at {0}", index);
				}
				elf1Recipe = (elf1Recipe + scores[elf1Recipe] + 1) % scores.Count;
				elf2Recipe = (elf2Recipe + scores[elf2Recipe] + 1) % scores.Count;
				//printList(scores, elf1Recipe, elf2Recipe);
				}catch(Exception e){
					Console.WriteLine(e);
					Console.Write("Press any key to continue . . . ");
					Console.ReadKey(true);
				}
			}
			//printList(scores, elf1Recipe, elf2Recipe);
			String result = "";
			for(int i=numInterations; i < numInterations + 10; i+=1) result += scores[i];
			
			
			Console.WriteLine("The scores of the next 10 recipes are: {0}.", result);
			Console.WriteLine("{0} first appears after {1} recipes.", numInterations, index);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		private static void printList(List<int> scores, int elf1, int elf2) {
			for(int i=0; i<scores.Count; i+=1){
				if(i==elf1) Console.Write("({0})",scores[i]);
				else if(i==elf2)Console.Write("[{0}]",scores[i]);
				else Console.Write(" {0} ",scores[i]);
			}
			Console.WriteLine();
		}
	}
}