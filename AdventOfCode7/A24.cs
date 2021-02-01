using System;
using System.Collections.Generic;

namespace AdventOfCode7
{
    class A24
    {
        static void Main()
        {
            string[] text = System.IO.File.ReadAllLines(@"C:\Users\JulesWin10\Desktop\AdventOfCode\input24.txt");
            //string[] text = System.IO.File.ReadAllLines(@"C:\Users\JulesWin10\Desktop\AdventOfCode\input24atest.txt");
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

        class Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }


            public override int GetHashCode()
            {
                return X * 100000 + Y;
            }
            public override bool Equals(object obj)
            {
                return Equals(obj as Point);
            }

            public bool Equals(Point obj)
            {
                return obj != null && obj.X == this.X && obj.Y == this.Y;
            }
        }
    }
}
