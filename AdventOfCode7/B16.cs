using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal class B16
    {
        internal static void Main()
        {
            string[] text = System.IO.File.ReadAllLines(@"Input\input16.txt");

            Boolean[] valid = new Boolean[1000];
            int section = 0;
            List<Field> fields = new List<Field>();
            List<List<int>> tickets = new List<List<int>>();
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
                        Field field = new Field
                        {
                            Name = line.Split(": ")[0]
                        };
                        string part = line.Split(": ")[1];
                        string[] parts = part.Split(" or ");
                        field.StartA = Convert.ToInt32(parts[0].Split('-')[0]);
                        field.EndA = Convert.ToInt32(parts[0].Split('-')[1]);
                        field.StartB = Convert.ToInt32(parts[1].Split('-')[0]);
                        field.EndB = Convert.ToInt32(parts[1].Split('-')[1]);
                        for (int i = field.StartA; i <= field.EndA; i++)
                        {
                            valid[i] = true;
                        }
                        for (int i = field.StartB; i <= field.EndB; i++)
                        {
                            valid[i] = true;
                        }
                        fields.Add(field);
                    }
                    else if (section == 1)
                    {
                        if (line != "your ticket:")
                        {
                            List<int> ticket = new List<int>();
                            foreach (string str in line.Split(','))
                            {
                                int num = Convert.ToInt32(str);
                                ticket.Add(num);
                            }
                            tickets.Add(ticket);
                        }
                    }
                    else if (section == 2 && line != "nearby tickets:")
                    {
                        List<int> ticket = new List<int>();
                        Boolean ticketErrorFound = false;
                        foreach (string str in line.Split(','))
                        {
                            int num = Convert.ToInt32(str);
                            if (!valid[num])
                            {
                                ticketErrorFound = true;
                            }
                            ticket.Add(num);
                        }
                        if (!ticketErrorFound)
                        {
                            tickets.Add(ticket);
                        }
                    }
                }
            }

            foreach (var item in fields)
            {
                item.Invalidity = new Boolean[fields.Count];
            }

            Stack<Field> foundStack = new Stack<Field>();
            foreach (var field in fields)
            {
                for (int invalidIndex = 0; invalidIndex < field.Invalidity.Length; invalidIndex++)
                {
                    if (!field.Invalidity[invalidIndex])
                    {
                        foreach (var ticket in tickets)
                        {
                            if (!(((field.StartA <= ticket[invalidIndex]) && (field.EndA >= ticket[invalidIndex]))
                                || ((field.StartB <= ticket[invalidIndex]) && (field.EndB >= ticket[invalidIndex]))))
                            {
                                field.Invalidity[invalidIndex] = true;
                                break;
                            }
                        }
                    }
                }
                if (field.CountValidPositions() == 1)
                {
                    foundStack.Push(field);
                }
            }
            while (foundStack.Count != 0)
            {
                Field field = foundStack.Pop();
                int indexOfFound = field.FirstValidPositionIndex();
                foreach (var tempField in fields)
                {
                    if ((!tempField.Invalidity[indexOfFound]) && (tempField != field))
                    {
                        tempField.Invalidity[indexOfFound] = true;
                        if (tempField.CountValidPositions() == 1)
                        {
                            foundStack.Push(tempField);
                        }
                    }
                }
            }
            long ret = tickets[0][fields[0].FirstValidPositionIndex()];
            for (int i = 1; i < 6; i++)
            {
                ret *= tickets[0][fields[i].FirstValidPositionIndex()];
            }
            Console.WriteLine(ret);
        }

        private class Field
        {
            public int CountValidPositions()
            {
                return Invalidity.Count(x => !x);
            }

            public int FirstValidPositionIndex()
            {
                int i = 0;
                foreach (var item in Invalidity)
                {
                    if (!item)
                    {
                        return i;
                    }
                    i++;
                }
                return -1;
            }

            public string Name { get; set; }
            public int StartA { get; set; }
            public int EndA { get; set; }
            public int StartB { get; set; }
            public int EndB { get; set; }
            public bool[] Invalidity { get; set; }
        }
    }
}