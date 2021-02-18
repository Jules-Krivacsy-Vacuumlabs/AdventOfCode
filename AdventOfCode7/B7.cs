using System;
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

        internal static void Main()
        {
            string target = "shiny gold";
            string[] text = System.IO.File.ReadAllLines(@"Input\input7.txt");
            Dictionary<string, List<InsideOfABag>> map = new Dictionary<string, List<InsideOfABag>>();
            //dark olive bags contain 3 faded blue bags, 4 dotted black bags.
            foreach (string line in text)
            {
                //dark olive
                var splits = line.Split(" bags contain ");
                string container = splits[0];
                //3 faded blue bags, 4 dotted black bags.
                string part = splits[1];

                if (!part.Equals("no other bags."))
                {
                    List<InsideOfABag> contains = new List<InsideOfABag>();
                    //3 faded blue bags
                    // 4 dotted black bags.
                    string[] parts = part.Split(", ");
                    foreach (string inside in parts)
                    {
                        string nameInside = "";
                        StringBuilder sb = new StringBuilder(nameInside);
                        int numberInside = Convert.ToInt32(inside.Split(' ')[0]);
                        int spaceCounter = 0;
                        foreach (char c in inside)
                        {
                            if (Char.IsLetter(c))
                            {

                                sb.Append(c);
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
                                    sb.Append(c);
                                }
                            }
                        }
                        contains.Add(new InsideOfABag(sb.ToString(), numberInside));
                    }

                    //build map
                    map.Add(container, contains);
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
