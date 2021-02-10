using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    internal static class A18
    {
        private static void Main()
        {
            //Changing comment
            string[] text = System.IO.File.ReadAllLines(@"Input\input18.txt");

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
                                currentNum = new StringBuilder(currentNum).Append(c).ToString();
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
                                    rest = new StringBuilder(rest).Append(" ").ToString();
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
                            StringBuilder sb = new StringBuilder(rest);
                            rest = sb.Append(c).ToString();
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
                            lastOperator = "";
                            if (parts.Count == 1)
                            {
                                ret += Convert.ToInt64(lastNum);
                                break;
                            }
                        }
                        parts[i] = lastNum + lastOperator + rest;
                        i++;
                    }
                    else
                    {
                        parts[i - 1] = new StringBuilder(parts[i - 1]).Append(lastNum)
                                                                      .Append(lastOperator)
                                                                      .Append(rest)
                                                                      .ToString();
                        parts.RemoveAt(i);
                        i--;
                    }
                }
            }
            Console.WriteLine(ret);
        }
    }
}