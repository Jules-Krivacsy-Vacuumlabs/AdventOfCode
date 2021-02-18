using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    internal static class A18
    {
        internal static void Main()
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
                    StringBuilder currentNumberSB = new StringBuilder("");
                    string lastOperator = "";
                    string rest = "";
                    foreach (char c in parts[i])
                    {
                        if (rest == "")
                        {
                            if (Char.IsDigit(c))
                            {
                                currentNumberSB.Append(c);
                            }
                            else if (c == '+' || c == '*' || c == ')')
                            {
                                if (lastNum == "")
                                {
                                    lastNum = currentNumberSB.ToString();
                                }
                                else
                                {
                                    if (lastOperator == "+")
                                    {
                                        lastNum = (Convert.ToInt64(lastNum) + Convert.ToInt64(currentNumberSB.ToString())).ToString();
                                    }
                                    else if (lastOperator == "*")
                                    {
                                        lastNum = (Convert.ToInt64(lastNum) * Convert.ToInt64(currentNumberSB.ToString())).ToString();
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
                                //currentNum = "";
                                currentNumberSB.Clear();
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
                        if (currentNumberSB.Length != 0)
                        {
                            if (lastOperator == "+")
                            {
                                lastNum = (Convert.ToInt64(lastNum) + Convert.ToInt64(currentNumberSB.ToString())).ToString();
                            }
                            else if (lastOperator == "*")
                            {
                                lastNum = (Convert.ToInt64(lastNum) * Convert.ToInt64(currentNumberSB.ToString())).ToString();
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
