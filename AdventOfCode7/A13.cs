using System;

namespace AdventOfCode
{
    internal static class A13
    {
        private static void Main()
        {
            string[] text = System.IO.File.ReadAllLines(@"Input\input13.txt");

            int earliestT = Convert.ToInt32(text[0]);//939
            int busCounter = 0;
            (int id, int earliest)[] busses = new (int id, int earliest)[text[1].Split(',').Length];
            int absolutEarliest = int.MaxValue;
            int ret = 0;
            foreach (string busId in text[1].Split(','))
            {
                if (busId != "x")
                {
                    busses[busCounter].id = Convert.ToInt32(busId);
                    busses[busCounter].earliest = ((earliestT / busses[busCounter].id) + (earliestT % busses[busCounter].id == 0 ? 0 : 1)) * busses[busCounter].id;
                    if (busses[busCounter].earliest < absolutEarliest)
                    {
                        absolutEarliest = busses[busCounter].earliest;
                        ret = (busses[busCounter].earliest - earliestT) * busses[busCounter].id;
                    }
                }

                busCounter++;
            }
            Console.WriteLine(ret);
        }
    }
}