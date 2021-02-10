using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode
{
    internal class A24
    {
        private static void Main()
        {
            string[] text = System.IO.File.ReadAllLines(@"Input\input24.txt");
            /*
            CENTRE IS 0,0
            se
            sw
            nw
            ne
            e
            w
            */
            Dictionary<Point, Boolean> floor = new Dictionary<Point, bool>();
            int numberOfBlacks = 0;
            foreach (string line in text)
            {
                string mod = line.Replace("nw", "1").Replace("ne", "2").Replace("sw", "3").Replace("se", "4");
                Point current = new Point(0, 0);
                for (int i = 0; i < mod.Length; i++)
                {
                    switch (mod[i])
                    {
                        case '1':
                            current.X--;
                            current.Y--;
                            break;

                        case '2':
                            current.X--;
                            current.Y++;
                            break;

                        case '3':
                            current.X++;
                            current.Y--;
                            break;

                        case '4':
                            current.X++;
                            current.Y++;
                            break;

                        case 'w':
                            current.Y -= 2;
                            break;

                        case 'e':
                            current.Y += 2;
                            break;

                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                }
                if (floor.ContainsKey(current))
                {
                    if (floor[current])
                    {
                        floor[current] = false;
                        numberOfBlacks--;
                    }
                    else
                    {
                        floor[current] = true;
                        numberOfBlacks++;
                    }
                }
                else
                {
                    floor.Add(current, true);
                    numberOfBlacks++;
                }
            }
            Console.WriteLine(numberOfBlacks);
        }

        private class Point : IEquatable<Point>
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }

            public bool Equals([AllowNull] Point other)
            {
                return other != null && other.X == this.X && other.Y == this.Y;
            }

            public override bool Equals(object obj)
            {
                Point other = (Point)obj;
                return other != null && other.X == this.X && other.Y == this.Y;
            }

            public override int GetHashCode()
            {
                return X * 100000 + Y;
            }
        }
    }
}