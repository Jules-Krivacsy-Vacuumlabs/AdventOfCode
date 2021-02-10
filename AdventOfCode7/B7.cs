﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    internal class B7
    {
        private class InsideOfABag
        {
            public string name;
            public int number;

            public InsideOfABag(string nameInside, int numberInside)
            {
                name = nameInside;
                number = numberInside;
            }
        }

        private static void Main()
        {
            string target = "shiny gold";
            string[] text = System.IO.File.ReadAllLines(@"Input\input7.txt");
            Dictionary<string, List<InsideOfABag>> map = new Dictionary<string, List<InsideOfABag>>();
            //dark olive bags contain 3 faded blue bags, 4 dotted black bags.
            foreach (string line in text)
            {
                //dark olive
                string container = line.Split(" bags contain ")[0];
                //3 faded blue bags, 4 dotted black bags.
                string part = line.Split(" bags contain ")[1];

                if (!part.Equals("no other bags."))
                {
                    List<InsideOfABag> contains = new List<InsideOfABag>();
                    //3 faded blue bags
                    // 4 dotted black bags.
                    string[] parts = part.Split(", ");
                    foreach (string inside in parts)
                    {
                        string nameInside = "";
                        int numberInside = Convert.ToInt32(inside.Split(' ')[0]);
                        int spaceCounter = 0;
                        foreach (char c in inside)
                        {
                            if (Char.IsLetter(c))
                            {
                                StringBuilder sb = new StringBuilder(nameInside);
                                nameInside = sb.Append(c).ToString();
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
                                    StringBuilder sb = new StringBuilder(nameInside);
                                    nameInside = sb.Append(c).ToString();
                                }
                            }
                        }
                        contains.Add(new InsideOfABag(nameInside, numberInside));
                    }

                    //build map
                    List<InsideOfABag> temp = new List<InsideOfABag>();
                    foreach (InsideOfABag item in contains)
                    {
                        temp.Add(item);
                    }
                    map.Add(container, temp);
                }
            }

            //walking trough the graph

            int ret = LevelDownCalculator(target, map) - 1;

            Console.WriteLine(ret);
        }

        private static int LevelDownCalculator(string target, Dictionary<string, List<InsideOfABag>> map)
        {
            int ret = 0;
            if (map.ContainsKey(target))
            {
                foreach (InsideOfABag bag in map[target])
                {
                    int howManyInside = LevelDownCalculator(bag.name, map);
                    if (howManyInside > 0)
                    {
                        ret += bag.number * howManyInside;
                    }
                    else
                    {
                        ret += bag.number;
                    }
                }
                return ret + 1;
            }
            return 0;
        }
    }
}