using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    internal static class B15
    {
        private static void Main()
        {
            Dictionary<int, Point> mem;

            mem = new Dictionary<int, Point>
            {
                { 1, new Point(1) },
                { 0, new Point(2) },
                { 15, new Point(3) },
                { 2, new Point(4) },
                { 10, new Point(5) },
                { 13, new Point(6) }
            };

            int turn = 7;
            int last = 13;
            while (true)
            {
                int answer;
                if (mem.ContainsKey(last))
                {
                    answer = mem[last].Answer(turn);
                }
                else
                {
                    answer = 0;
                }
                if (mem.ContainsKey(answer))
                {
                    mem[answer].Recurrence(turn);
                }
                else
                {
                    mem.Add(answer, new Point(turn));
                }

                if (turn == (30000000))
                {
                    Console.WriteLine(turn + " " + answer);
                    break;
                }
                last = answer;
                turn++;
            }
        }

        private class Point
        {
            private int _first;
            private int _second;

            public Point(int v)
            {
                this._first = v;
            }

            public int Answer(int turn)
            {
                return turn - 1 - _first;
            }

            internal void Recurrence(int turn)
            {
                if (_second != 0)
                {
                    _first = _second;
                }
                _second = turn;
            }
        }
    }
}