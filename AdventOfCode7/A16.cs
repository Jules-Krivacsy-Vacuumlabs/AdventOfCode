using System;

namespace AdventOfCode
{
    internal static class A16
    {
        internal static void Main()
        {
            string[] text = System.IO.File.ReadAllLines(@"Input\input16.txt");

            int[] valid = new int[1000];
            int section = 0;
            int ticketScanningErrorRate = 0;
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
                        int startA = Convert.ToInt32(parts[0].Split('-')[0]);
                        int endA = Convert.ToInt32(parts[0].Split('-')[1]);
                        int startB = Convert.ToInt32(parts[1].Split('-')[0]);
                        int endB = Convert.ToInt32(parts[1].Split('-')[1]);
                        for (int i = startA; i <= endA; i++)
                        {
                            valid[i]++;
                        }
                        for (int i = startB; i <= endB; i++)
                        {
                            valid[i]++;
                        }
                    }
                    else if (section == 2 && line != "nearby tickets:")
                    {
                        foreach (string str in line.Split(','))
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

            Console.WriteLine(ticketScanningErrorRate);
        }
    }
}