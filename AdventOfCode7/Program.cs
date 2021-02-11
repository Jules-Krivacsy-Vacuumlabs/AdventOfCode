using System;

namespace AdventOfCode
{
    internal static class Program
    {
        private static void Main()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Which code do you want to run? (letter+number)");
                Console.WriteLine("7 13 15 16 17 18 24 exit");
                var input = Console.ReadLine();
                exit = TaskCaller(input);
            }
        }

        private static bool TaskCaller(string input)
        {
            input = input.ToUpper();
            switch (input)
            {
                case "EXIT":
                    return true;

                case "A7":
                    A7.Main();
                    break;

                case "B7":
                    B7.Main();
                    break;

                case "A13":
                    A13.Main();
                    break;

                case "B13":
                    new B13().AoCB13();
                    break;

                case "A15":
                    Console.WriteLine("Sorry, in the hurry I overwrited A15 with B15. Try to create it, using B15.");
                    break;

                case "B15":
                    B15.Main();
                    break;

                case "A16":
                    A16.Main();
                    break;

                case "B16":
                    B16.Main();
                    break;

                case "A17":
                    A17.Main();
                    break;

                case "B17":
                    B17.Main();
                    break;

                case "A18":
                    A18.Main();
                    break;

                case "B18":
                    B18.Main();
                    break;

                case "A24":
                    A24.Main();
                    break;

                case "B24":
                    B24.Main();
                    break;

                default:
                    Console.WriteLine("Invalid input!");
                    break;
            }
            Console.WriteLine();
            return false;
        }
    }
}