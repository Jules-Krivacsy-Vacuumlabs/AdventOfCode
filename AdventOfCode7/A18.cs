using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal class A18
    {
        private static void Main()
        {
            //Changing comment
            string[] text = System.IO.File.ReadAllLines(@"Input\input18.txt");
            //string[] text = System.IO.File.ReadAllLines(@"Input\input18atest.txt");

            long ret = 0;

            foreach (string line in text)
            {
                List<string> parts = line.Split('(').ToList();
                int i = 0;
                while ((parts.Count != 0) && (i < parts.Count))
                {
                    string lastNum = "";
                    string currentNum = "";
                    string lastOperator = "";
                    string rest = "";
                    foreach (char c in parts[i])
                    {
                        if (rest == "")
                        {
                            if (Char.IsDigit(c))
                            {
                                currentNum += c;
                            }
                            else if (c == '+' || c == '*' || c == ')')
                            {
                                if (lastNum == "")
                                {
                                    lastNum = currentNum;
                                }
                                else
                                {
                                    if (lastOperator == "+")
                                    {
                                        lastNum = (Convert.ToInt64(lastNum) + Convert.ToInt64(currentNum)).ToString();
                                    }
                                    else if (lastOperator == "*")
                                    {
                                        lastNum = (Convert.ToInt64(lastNum) * Convert.ToInt64(currentNum)).ToString();
                                    }
                                }
                                if (c == ')')
                                {
                                    lastOperator = "";
                                    rest += " ";
                                }
                                else
                                {
                                    lastOperator = c.ToString();
                                }
                                currentNum = "";
                            }
                        }
                        else
                        {
                            rest += c;
                        }
                    }
                    if (rest == "")
                    {
                        if (currentNum != "")
                        {
                            if (lastOperator == "+")
                            {
                                lastNum = (Convert.ToInt64(lastNum) + Convert.ToInt64(currentNum)).ToString();
                            }
                            else if (lastOperator == "*")
                            {
                                lastNum = (Convert.ToInt64(lastNum) * Convert.ToInt64(currentNum)).ToString();
                            }
                            currentNum = "";
                            lastOperator = "";
                            if (parts.Count == 1)
                            {
                                ret += Convert.ToInt64(lastNum);
                                //Console.WriteLine(lastNum);
                                break;
                            }
                        }
                        parts[i] = lastNum + lastOperator + rest;
                        i++;
                    }
                    else
                    {
                        parts[i - 1] = parts[i - 1] + lastNum + lastOperator + rest;
                        parts.RemoveAt(i);
                        i--;
                    }
                }
            }
            Console.WriteLine(ret);
        }
    }
}