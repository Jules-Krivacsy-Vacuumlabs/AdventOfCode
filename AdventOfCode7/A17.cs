using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode
{
    internal class A17
    {
        internal static void Main()
        {
            string[] text = System.IO.File.ReadAllLines(@"Input\input17.txt");
            /*
            CENTRE IS 0,0
            */
            HashSet<Point3D> lastSpace = new HashSet<Point3D>();
            int inputX = 0;
            foreach (string line in text)
            {
                int y = 0;
                foreach (char c in line)
                {
                    if (c == '#')
                        lastSpace.Add(new Point3D(inputX, y, 0));
                    y++;
                }
                inputX++;
            }
            //GAME OF LIFE
            HashSet<Point3D> nextSpace = new HashSet<Point3D>();
            for (int time = 0; time <= 4; time++)
            {
                nextSpace = new HashSet<Point3D>();
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
                                if (lastSpace.Contains(new Point3D(x, y, z)))
                                {
                                    countAdjacentActives++;
                                }
                                else
                                {
                                    //check all adjecent passives if they have exactly 3 active neightbors
                                    PassiveToActive(lastSpace, nextSpace, new Point3D(x, y, z));
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

        private static void PassiveToActive(HashSet<Point3D> lastSpace, HashSet<Point3D> nextSpace, Point3D passive)
        {
            int countAdjacentActives = 0;
            for (int x = passive.X - 1; x < passive.X + 2; x++)
            {
                for (int y = passive.Y - 1; y < passive.Y + 2; y++)
                {
                    for (int z = passive.Z - 1; z < passive.Z + 2; z++)
                    {
                        if (lastSpace.Contains(new Point3D(x, y, z)))
                        {
                            countAdjacentActives++;
                            if (countAdjacentActives > 3)
                                return;
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

        private class Point3D : IEquatable<Point3D>
        {
            public Point3D(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

            public bool Equals([AllowNull] Point3D other)
            {
                return (other != null) && (other.X, other.Y, other.Z).Equals((X, Y, Z));
            }

            public override bool Equals(object obj)
            {
                Point3D other = (Point3D)obj;
                return (other != null) && (other.X, other.Y, other.Z).Equals((X, Y, Z));
            }

            public override int GetHashCode()
            {
                return X * 111111 + Y * 3333 + Z;
            }

            public override string ToString()
            {
                return X + " " + Y + " " + Z;
            }
        }
    }
}