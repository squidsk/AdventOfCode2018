/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/19/2018
 * Time: 4:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day7
{
	class Node : IComparable<Node> {
		internal char data;
		internal SortedSet<Node> children;
		
		public Node(char data) { 
			this.data = data;
			children = new SortedSet<Node>();
		}
		
		public bool isADescendant(Node n) {
			bool isDescendant = false;
			foreach (var child in children) {
				isDescendant |= child.data == n.data || child.isADescendant(n);
			}
			return isDescendant;
		}

		#region IComparable implementation

		public int CompareTo(Node other)
		{
			if(data == other.data) return 0;
			if(data < other.data) return -1;
			return 1;
		}

		#endregion
	}
	class Program
	{
		const string LINE_REGEX = @"Step ([A-Z]) must be finished before step ([A-Z]) can begin.";
		public static void Main(string[] args)
		{
            StringReader reader = new StringReader(File.ReadAllText("input.txt"));
            HashSet<Node> heads = new HashSet<Node>();
            Dictionary<char, Node> nodes = new Dictionary<char, Node>();
            
            while(reader.Peek() != -1) {
            	string line = reader.ReadLine();
            	var matches = Regex.Match(line, LINE_REGEX);
            	var firstChar = matches.Groups[1].Value[0];
            	var secondChar = matches.Groups[2].Value[0];
            	Node n1, n2;
            	
            	if(nodes.ContainsKey(firstChar)) {
            		n1 = nodes[firstChar];
            	} else {
            		n1 = new Node(firstChar);
            		nodes.Add(firstChar, n1);
            		heads.Add(n1);
            	}
            	if(nodes.ContainsKey(secondChar)) {
            		n2 = nodes[secondChar];
            	} else {
            		n2 = new Node(secondChar);
            		nodes.Add(secondChar, n2);
            	}
           		heads.Remove(n2);
            	n1.children.Add(n2);
            }
			
        	SortedSet<Node> output = new SortedSet<Node>(heads);
        	int count = 0;
        	while(output.Count > 0) {
        		Node n = output.Min;
        		output.Remove(n);
        		Console.Write(n.data);
        		count += 1;
        		foreach(var node in n.children) {
        			bool isAnotherNodesChild = false;
        			foreach (var node2 in n.children) {
        				isAnotherNodesChild |= node != node2 && node2.isADescendant(node);
        			}
        			foreach (var node2 in output) {
						//isAnotherNodesChild |= node2.children.Contains(node);
						isAnotherNodesChild |= node2.isADescendant(node);
        			}
        			if(!isAnotherNodesChild) output.Add(node);
        		}
        		//output.UnionWith(nodes[ch].children);
        	}
        	Console.WriteLine(" with a total length of {0}.", count);            	

           	Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}