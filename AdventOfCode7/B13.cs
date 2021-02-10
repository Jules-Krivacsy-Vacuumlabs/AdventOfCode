using System;
using System.Linq;

namespace AdventOfCode
{
    internal static class B13
    {
        private static void Main()
        {
            string[] text = System.IO.File.ReadAllLines(@"Input\input13.txt");

            var x = text[1].Split(',')
                    .Select((item, index) => (item, index))
                    .Where(t => !t.item.Equals("x"))
                    .Select(t => (long.Parse(t.item), t.index))
                    .OrderByDescending(t => t.Item1)
                    .ToList();

            var timestamp = x.First().Item1 - x.First().index;
            var period = x.First().Item1;
            for (int busIndex = 1; busIndex <= x.Count; busIndex++)
            {
                while (x.Take(busIndex)
                    .Any(t => (timestamp + t.index) % t.Item1 != 0))
                {
                    timestamp += period;
                }

                period = x.Take(busIndex)
                          .Select(t => t.Item1)
                          .Aggregate(LCM);
            }

            var busses = text[1].Split(',')
                    .Select((item, index) => (item, index))
                    .Where(t => !t.item.Equals("x"))
                    .Select(t => (long.Parse(t.item), t.index))
                    .OrderByDescending(t => t.Item1)
                    .ToList();

            var periods = new int[busses.Count];

            Console.WriteLine(periods);
        }

        private static long LCM(long a, long b)
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