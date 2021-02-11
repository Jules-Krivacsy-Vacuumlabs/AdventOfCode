using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    internal static class A7
    {
        internal static void Main()
        {
            string target = "shiny gold";
            string[] text = System.IO.File.ReadAllLines(@"Input\input7.txt");
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
            //dark olive bags contain 3 faded blue bags, 4 dotted black bags.
            foreach (string line in text)
            {
                //dark olive
                string container = line.Split(" bags contain ")[0];
                //3 faded blue bags, 4 dotted black bags.
                string part = line.Split(" bags contain ")[1];

                if (!part.Equals("no other bags."))
                {
                    List<string> contains = new List<string>();
                    //3 faded blue bags
                    // 4 dotted black bags.
                    string[] parts = part.Split(", ");
                    foreach (string inside in parts)
                    {
                        string contain = "";
                        int spaceCounter = 0;
                        foreach (char c in inside)
                        {
                            if (Char.IsLetter(c))
                            {
                                contain = new StringBuilder(contain).Append(c).ToString();
                            }
                            else if (c == ' ')
                            {
                                spaceCounter++;
                                if (spaceCounter == 3)
                                {
                                    break;
                                }
                                else if (spaceCounter == 2)
                                {
                                    contain = new StringBuilder(contain).Append(c).ToString();
                                }
                            }
                        }
                        contains.Add(contain);
                    }

                    //build map
                    foreach (string item in contains)
                    {
                        if (map.ContainsKey(item))
                        {
                            if (!map[item].Contains(container))
                                map[item].Add(container);
                        }
                        else
                        {
                            List<string> temp = new List<string>
                            {
                                container
                            };
                            map.Add(item, temp);
                        }
                    }
                }
            }

            //walking trough the graph

            List<string> reachable = new List<string>();
            Stack<string> remaining = new Stack<string>();
            remaining.Push(target);
            while (remaining.Count != 0)
            {
                string current = remaining.Pop();
                if (map.ContainsKey(current))
                {
                    foreach (string source in map[current])
                    {
                        if (!reachable.Contains(source))
                        {
                            reachable.Add(source);
                            remaining.Push(source);
                        }
                    }
                }
            }
            Console.WriteLine(reachable.Count);
        }
    }
}