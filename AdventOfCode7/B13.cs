using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class B13
    {
        public long AoCB13()
        {
            string[] text = System.IO.File.ReadAllLines(@"Input\input13.txt");

            var allBus = text[1].Split(",").ToArray();
            List<(long id, int offset)> busses = new List<(long id, int offset)>();
            for (int i = 0; i < allBus.Length; i++)
            {
                if (allBus[i] != "x")
                {
                    busses.Add((long.Parse(allBus[i]), i));
                }
            }
            busses.Sort();
            busses.Reverse();
            long ret = busses[0].id - busses[0].offset;
            List<long> periods = new List<long>
            {
                busses[0].id
            };
            for (int i = 0; i < busses.Count; i++)
            {
                bool found;
                do
                {
                    found = true;
                    for (int inn = 0; inn <= i; inn++)
                    {
                        if ((ret + busses[inn].offset) % busses[inn].id != 0)
                        {
                            found = false;
                            break;
                        }
                    }
                    if (!found)
                        ret += periods.Last();
                } while (!found);

                periods.Add(LCM(periods.Last(), busses[i].id));
            }

            Console.WriteLine(ret);
            return ret;
        }

        public long LCM(long a, long b)
        {
            long num1, num2;
            if (a > b)
            {
                num1 = a; num2 = b;
            }
            else
            {
                num1 = b; num2 = a;
            }

            for (int i = 1; i <= num2; i++)
            {
                if ((num1 * i) % num2 == 0)
                {
                    return i * num1;
                }
            }

            return num2;
        }
    }
}