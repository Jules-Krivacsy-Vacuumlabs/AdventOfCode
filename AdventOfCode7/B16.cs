﻿using System.Linq;
using System;
using System.Collections.Generic;

namespace AdventOfCode7
{
    class B16
    {
        static void Main(string[] args)
        {
            //string[] text = System.IO.File.ReadAllLines(@"C:\Users\JulesWin10\Desktop\AdventOfCode\input16btest.txt");
            string[] text = System.IO.File.ReadAllLines(@"C:\Users\JulesWin10\Desktop\AdventOfCode\input16.txt");

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
                        Field field = new Field();
                        field.Name = line.Split(": ")[0];
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
                    else if (section == 2)
                    {
                        if (line != "nearby tickets:")
                        {
                            List<int> ticket = new List<int>();
                            Boolean ticketErrorFound = false;
                            foreach (string str in line.Split(','))
                            {
                                int num = Convert.ToInt32(str);
                                if (valid[num] == false)
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
                    if ((!tempField.Invalidity[indexOfFound])&&(tempField != field))
                    {
                        tempField.Invalidity[indexOfFound] = true;
                        if (tempField.CountValidPositions() == 1)
                        {
                            foundStack.Push(tempField);
                        }
                    }
                }
            }
            foreach(var field in fields)
            {
                Console.WriteLine(field.Name+" "+field.FirstValidPositionIndex()+" Is it okey: "+field.CountValidPositions());
            }
            long ret = tickets[0][fields[0].FirstValidPositionIndex()];
            for(int i = 1; i < 6; i++)
            {
                ret*= tickets[0][fields[i].FirstValidPositionIndex()];
            }
            Console.WriteLine(ret);
        }

        class Field
        {
            string name;
            int startA;
            int endA;
            int startB;
            int endB;
            Boolean[] invalid;
            public int CountValidPositions()
            {
                return invalid.Count(x => x == false);
            }
            public int FirstValidPositionIndex()
            {
                int i = 0;
                foreach(var item in invalid)
                {
                    if (!item)
                    {
                        return i;
                    }
                    i++;
                }
                return -1;
            }
            public string Name { get => name; set => name = value; }
            public int StartA { get => startA; set => startA = value; }
            public int EndA { get => endA; set => endA = value; }
            public int StartB { get => startB; set => startB = value; }
            public int EndB { get => endB; set => endB = value; }
            public bool[] Invalidity { get => invalid; set => invalid = value; }
        }
    }
}
