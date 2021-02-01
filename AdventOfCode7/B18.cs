using System;

namespace AdventOfCode7
{
    internal class B18
    {
        private static string Solver(string input)
        {
            long ret = -1;
            var nonMulti = input.Split('*');
            foreach (var itemWithAdd in nonMulti)
            {
                var nonAdd = itemWithAdd.Split('+');
                if (nonAdd.Length > 0)
                {
                    long added = Convert.ToInt64(nonAdd[0]);
                    foreach (var item in nonAdd[1..])
                    {
                        added += Convert.ToInt64(item);
                    }
                    if (ret < 0)
                    {
                        ret = added;
                    }
                    else
                    {
                        ret *= added;
                    }
                }
            }
            return ret.ToString();
        }

        private static void Main()
        {
            string[] text = System.IO.File.ReadAllLines(@"C:\Users\JulesWin10\Desktop\AdventOfCode\input18.txt");
            //string[] text = System.IO.File.ReadAllLines(@"C:\Users\JulesWin10\Desktop\AdventOfCode\input18atest.txt");

            long ret = 0;

            foreach (string line in text)
            {
                string spaceless = line.Replace(" ", "");
                //Search and split
                //search for the innermost
                int openingBracket0 = spaceless.IndexOf('(');
                while (spaceless.Contains(")"))
                {
                    int openingBracket1 = spaceless[(openingBracket0 + 1)..].IndexOf('(');
                    int closingBracket = spaceless[(openingBracket0 + 1)..].IndexOf(')');
                    //we openning a new one
                    if ((openingBracket1 >= 0) && (openingBracket1 < closingBracket))
                    {
                        openingBracket0 = openingBracket1 + openingBracket0 + 1;
                    }
                    //we closing the previous one
                    else
                    {
                        spaceless = spaceless.Substring(0, openingBracket0) + Solver(spaceless[(openingBracket0 + 1)..(closingBracket + openingBracket0 + 1)]) + spaceless[(closingBracket + openingBracket0 + 2)..];
                        openingBracket0 = spaceless.IndexOf('(');
                    }
                }
                spaceless = Solver(spaceless);
                Console.WriteLine(spaceless);
                ret += Convert.ToInt64(spaceless);
            }
            Console.WriteLine(ret);
        }
    }
}