﻿
using System;
using System.Collections.Generic;

namespace AdventOfCode7
{
    class A16
    {
        static void Main(string[] args)
        {
            //string[] text = System.IO.File.ReadAllLines(@"C:\Users\JulesWin10\Desktop\AdventOfCode\input16atest.txt");
            string[] text = System.IO.File.ReadAllLines(@"C:\Users\JulesWin10\Desktop\AdventOfCode\input16.txt");

            int[] valid = new int[1000];
            int section = 0;
            int ticketScanningErrorRate =0;
            foreach (string line in text)
            {
                if (line == "")
                {
                    section++;
                }
                else
                {
                    if (section == 0)
                    {
                        string part = line.Split(": ")[1];
                        string[] parts = part.Split(" or ");
                        int a1 = Convert.ToInt32(parts[0].Split('-')[0]);
                        int a2 = Convert.ToInt32(parts[0].Split('-')[1]);
                        int b1 = Convert.ToInt32(parts[1].Split('-')[0]);
                        int b2 = Convert.ToInt32(parts[1].Split('-')[1]);
                        for (int i = a1; i <= a2; i++)
                        {
                            valid[i]++;
                        }
                        for (int i = b1; i <= b2; i++)
                        {
                            valid[i]++;
                        }
                    }
                    else if (section == 2)
                    {
                        if(line!= "nearby tickets:")
                        {
                            foreach(string str in line.Split(','))
                            {
                                int num = Convert.ToInt32(str);
                                if (valid[num] == 0)
                                {
                                    ticketScanningErrorRate += num;
                                }
                            }
                        }
                    }
                }

            }


            Console.WriteLine(ticketScanningErrorRate);



        }
    }
}