/*
 * Created by SharpDevelop.
 * User: skalmar
 * Date: 12/20/2018
 * Time: 10:10 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Day7b
{
	class Node : IComparable<Node> {
		internal char data;
		internal SortedSet<Node> children;
		internal int timeToComplete;
		internal int timeWorked;
		
		public Node(char data, int timeToComplete) { 
			this.data = data;
			this.timeToComplete = timeToComplete;
			timeWorked = 0;
			children = new SortedSet<Node>();
		}
		
		public bool isADescendant(Node n) {
			bool isDescendant = false;
			foreach (var child in children) {
				isDescendant |= child.data == n.data || child.isADescendant(n);
			}
			return isDescendant;
		}
		
		public bool finishedWork() { return timeWorked == timeToComplete; }

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
		const int NUM_WORKERS = 5;
		const int BASE_TIME = 60;
		const int NUM_TASKS = 26;
		
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
            		n1 = new Node(firstChar, BASE_TIME + (firstChar - 'A' + 1));
            		nodes.Add(firstChar, n1);
            		heads.Add(n1);
            	}
            	if(nodes.ContainsKey(secondChar)) {
            		n2 = nodes[secondChar];
            	} else {
            		n2 = new Node(secondChar, BASE_TIME + (secondChar - 'A' + 1));
            		nodes.Add(secondChar, n2);
            	}
           		heads.Remove(n2);
            	n1.children.Add(n2);
            }
			
        	SortedSet<Node> output = new SortedSet<Node>(heads);
        	Node[] workers = new Node[NUM_WORKERS];
        	string taskOrder = "";
        	int count = 0;
        	int time = -1;
        	while(count < NUM_TASKS) {
        		time += 1;
        		workers.ForEach(n => n.timeWorked += 1);
        		for(int i = 0; i < NUM_WORKERS; i += 1) {
        			if(workers[i] == null) {
	    				if(output.Count > 0) {
	    					workers[i] = output.Min;
	    					output.Remove(output.Min);
	    					Console.WriteLine("Task {0} starts at time {1}", workers[i].data, time);
	    				}        				
        			} else if(workers[i].finishedWork()) {
        				count += 1;
        				Node n = workers[i];
    					workers[i] = null;
        				taskOrder += n.data;
        				Console.WriteLine("Task {0} completed at time {1}", n.data, time);
        				foreach(var node in n.children) {
		        			bool isAnotherNodesChild = false;
		        			foreach(var node2 in workers) {
		        				isAnotherNodesChild |= node2 != null && node != node2 && node2.isADescendant(node);
		        			}
		        			foreach (var node2 in n.children) {
		        				isAnotherNodesChild |= node != node2 && node2.isADescendant(node);
		        			}
		        			foreach (var node2 in output) {
								isAnotherNodesChild |= node2.isADescendant(node);
		        			}
		        			if(!isAnotherNodesChild) output.Add(node);
		        		}
	    				if(output.Count > 0) {
        					for(int j = 0; j < NUM_WORKERS; j+= 1) {
        						if(workers[j] == null && output.Count > 0) {
			    					workers[j] = output.Min;
			    					output.Remove(output.Min);
		    						Console.WriteLine("Task {0} starts at time {1}", workers[j].data, time);
        						}
        					}
    					}
        			}
        		}
        	}
        	Console.WriteLine("{0} with a total length of {1}.", taskOrder, count);            	
        	Console.WriteLine("It took {0} units of time to complete all tasks.", time);
           	Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
	}
	
	static class LinqExtensions {
		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action) {
		    foreach(T item in enumeration)
		    {
		    	if(item != null) {
		        	action(item);
		    	}
		    }
		}

	}
}