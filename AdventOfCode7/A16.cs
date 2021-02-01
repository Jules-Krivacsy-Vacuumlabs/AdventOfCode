using System;

namespace AdventOfCode7
{
    internal class A16
    {
        private static void Main()
        {
            //string[] text = System.IO.File.ReadAllLines(@"Input\input16atest.txt");
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
                        if (line != "nearby tickets:")
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
            }

            Console.WriteLine(ticketScanningErrorRate);
        }
    }
}