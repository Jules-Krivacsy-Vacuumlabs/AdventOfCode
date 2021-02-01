using System;
using System.Collections.Generic;

namespace AdventOfCode7
{
    internal class B24
    {
        private static void Main()
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

            //GAME OF LIFE
            //we plan to store only blacks
            //remove whites from floor
            foreach (var key in floor.Keys)
            {
                if (!floor[key])
                {
                    floor.Remove(key);
                }
            }
            HashSet<Point> lastFloor = new HashSet<Point>(floor.Keys);
            for (int time = 0; time <= 100; time++)
            {
                HashSet<Point> nextFloor = new HashSet<Point>();
                foreach (var point in lastFloor)
                {
                    //6 check around point until find zero or more than 2 (0,3,4,5,6 black) turn to white
                    int countAdjacentBlacks =
                        (lastFloor.Contains(new Point(point.X, point.Y + 2)) ? 1 : 0)
                        + (lastFloor.Contains(new Point(point.X, point.Y - 2)) ? 1 : 0)
                        + (lastFloor.Contains(new Point(point.X - 1, point.Y - 1)) ? 1 : 0)
                        + (lastFloor.Contains(new Point(point.X - 1, point.Y + 1)) ? 1 : 0)
                        + (lastFloor.Contains(new Point(point.X + 1, point.Y - 1)) ? 1 : 0)
                        + (lastFloor.Contains(new Point(point.X + 1, point.Y + 1)) ? 1 : 0)
                        ;
                    //1,2 stay black next round
                    if ((countAdjacentBlacks == 1) || (countAdjacentBlacks == 2))
                    {
                        nextFloor.Add(point);
                    }

                    //check all adjecent whites if they have exactly 2 black neightbors
                    White2Black(lastFloor, nextFloor, point, 0, 2);
                    White2Black(lastFloor, nextFloor, point, 0, -2);
                    White2Black(lastFloor, nextFloor, point, -1, -1);
                    White2Black(lastFloor, nextFloor, point, -1, 1);
                    White2Black(lastFloor, nextFloor, point, 1, -1);
                    White2Black(lastFloor, nextFloor, point, 1, 1);
                }

                lastFloor = nextFloor;
                Console.WriteLine("Day " + (time + 1) + ": " + nextFloor.Count);
            }
        }

        private static void White2Black(HashSet<Point> lastFloor, HashSet<Point> nextFloor, Point point, int offsetX, int offsetY)
        {
            if (!lastFloor.Contains(new Point(point.X + offsetX, point.Y + offsetY)))
            {
                int countAdjacentBlacks =
                (lastFloor.Contains(new Point(point.X + offsetX, point.Y + offsetY + 2)) ? 1 : 0)
                + (lastFloor.Contains(new Point(point.X + offsetX, point.Y + offsetY - 2)) ? 1 : 0)
                + (lastFloor.Contains(new Point(point.X + offsetX - 1, point.Y + offsetY - 1)) ? 1 : 0)
                + (lastFloor.Contains(new Point(point.X + offsetX - 1, point.Y + offsetY + 1)) ? 1 : 0)
                + (lastFloor.Contains(new Point(point.X + offsetX + 1, point.Y + offsetY - 1)) ? 1 : 0)
                + (lastFloor.Contains(new Point(point.X + offsetX + 1, point.Y + offsetY + 1)) ? 1 : 0)
                ;
                //exactly 2 black neighbor
                if (countAdjacentBlacks == 2)
                {
                    nextFloor.Add(new Point(point.X + offsetX, point.Y + offsetY));
                }
            }
        }

        private class Point
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

            public override string ToString()
            {
                return X + " " + Y;
            }
        }
    }
}