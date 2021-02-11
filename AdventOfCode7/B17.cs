using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode
{
    internal class B17
    {
        internal static void Main()
        {
            string[] text = System.IO.File.ReadAllLines(@"Input\input17.txt");
            /*
            CENTRE IS 0,0
            */
            HashSet<Point4D> lastSpace = new HashSet<Point4D>();
            int inputX = 0;
            foreach (string line in text)
            {
                int y = 0;
                foreach (char c in line)
                {
                    if (c == '#')
                        lastSpace.Add(new Point4D(inputX, y, 0, 0));
                    y++;
                }
                inputX++;
            }

            //GAME OF LIFE
            HashSet<Point4D> nextSpace = new HashSet<Point4D>();
            for (int time = 0; time <= 4; time++)
            {
                nextSpace = new HashSet<Point4D>();
                foreach (var point in lastSpace)
                {
                    //26 check around point until exactly 2 or 3 active (0,3,4,5,6 black) to remain active
                    int countAdjacentActives = -1;
                    for (int x = point.X - 1; x < point.X + 2; x++)
                    {
                        for (int y = point.Y - 1; y < point.Y + 2; y++)
                        {
                            for (int z = point.Z - 1; z < point.Z + 2; z++)
                            {
                                for (int w = point.W - 1; w < point.W + 2; w++)
                                {
                                    if (lastSpace.Contains(new Point4D(x, y, z, w)))
                                    {
                                        countAdjacentActives++;
                                    }
                                    else
                                    {
                                        //check all adjecent passives if they have exactly 3 active neightbors
                                        PassiveToActive(lastSpace, nextSpace, new Point4D(x, y, z, w));
                                    }
                                }
                            }
                        }
                    }
                    if ((countAdjacentActives == 2) || (countAdjacentActives == 3))
                    {
                        nextSpace.Add(point);
                    }
                }

                lastSpace = nextSpace;
            }
            Console.WriteLine("Day " + 5 + ": " + nextSpace.Count);
        }

        private static void PassiveToActive(HashSet<Point4D> lastSpace, HashSet<Point4D> nextSpace, Point4D passive)
        {
            int countAdjacentActives = 0;
            for (int x = passive.X - 1; x < passive.X + 2; x++)
            {
                for (int y = passive.Y - 1; y < passive.Y + 2; y++)
                {
                    for (int z = passive.Z - 1; z < passive.Z + 2; z++)
                    {
                        for (int w = passive.W - 1; w < passive.W + 2; w++)
                        {
                            if (lastSpace.Contains(new Point4D(x, y, z, w)))
                            {
                                countAdjacentActives++;
                                if (countAdjacentActives > 3)
                                    return;
                            }
                        }
                    }
                }
            }
            //exactly 3 active neighbor
            if (countAdjacentActives == 3)
            {
                nextSpace.Add(passive);
            }
        }

        private class Point4D : IEquatable<Point4D>
        {
            public Point4D(int x, int y, int z, int w)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public int W { get; set; }

            public override int GetHashCode()
            {
                return W * 111111 + Z * 2221 + Y * 31 + X;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Point4D);
            }

            public bool Equals([AllowNull] Point4D other)
            {
                if (other != null)
                    return (other.X, other.Y, other.Z, other.W).Equals((X, Y, Z, W));
                return false;
            }

            public override string ToString()
            {
                return X + " " + Y + " " + Z + " " + W;
            }
        }
    }
}