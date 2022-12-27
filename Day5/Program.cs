using System;
using System.IO;
using System.Collections.Generic;

namespace Day5
{
    class MainClass
    {
        public static Stack<char>[] ParseStacks(in string[] lines)
        {
            int stackCount;
            int stackIdLineI = 0;
            {
                while (!lines[stackIdLineI].StartsWith(" 1 "))
                {
                    ++stackIdLineI;
                }
                string stackIdLine = lines[stackIdLineI].Trim();
                stackCount = int.Parse(stackIdLine.Substring(stackIdLine.LastIndexOf(' ')));
            }
            Console.WriteLine($"Stack count: { stackCount }");

            var stacks = new Stack<char>[stackCount];
            for (int i = 0; i < stackCount; ++i)
            {
                stacks[i] = new Stack<char>();
            }

            for (int lineI = stackIdLineI - 1; lineI >= 0; --lineI)
            {
                var line = lines[lineI];

                for (int i = 0; i < stackCount; ++i)
                {
                    char item = line[i * 4 + 1];
                    if (item == ' ')
                        continue;
                    Console.WriteLine($"Stack #{ i + 1 }: { item }");
                    stacks[i].Push(item);
                }
                Console.WriteLine();
            }

            return stacks;
        }

        public class Movement
        {
            public int Count { get; private set; }
            public int From { get; private set; }
            public int To { get; private set; }

            public Movement(in string line)
            {
                var fields = line.Split(' ');
                Count = int.Parse(fields[1]);
                From = int.Parse(fields[3]);
                To = int.Parse(fields[5]);
            }
        }

        public static Movement[] ParseMovements(in string[] lines)
        {
            int lineI = 0;
            while (!string.IsNullOrEmpty(lines[lineI]))
            {
                ++lineI;
            }
            ++lineI;

            var movements = new List<Movement>();
            for (; lineI < lines.Length; ++lineI)
            {
                var line = lines[lineI];
                if (!string.IsNullOrEmpty(line))
                {
                    movements.Add(new Movement(line));
                }
            }
            return movements.ToArray();
        }

        public static void ExecMovements(
            Stack<char>[] stacks, Movement[] movements)
        {
            foreach (var mov in movements)
            {
                for (int i = 0; i < mov.Count; ++i)
                {
                    var value = stacks[mov.From - 1].Pop();
                    stacks[mov.To - 1].Push(value);
                }
            }
        }

        public static void ExecMovements2(
            Stack<char>[] stacks, Movement[] movements)
        {
            foreach (var mov in movements)
            {
                var values = new Stack<char>();
                for (int i = 0; i < mov.Count; ++i)
                {
                    var value = stacks[mov.From - 1].Pop();
                    values.Push(value);
                }
                for (int i = 0; i < mov.Count; ++i)
                {
                    var value = values.Pop();
                    stacks[mov.To - 1].Push(value);
                }
            }
        }

        public static string GetTopCrates(Stack<char>[] stacks)
        {
            string output = "";
            foreach (var stack in stacks)
            {
                output += stack.Peek();
            }
            return output;
        }

        public static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");

            var movements = ParseMovements(lines);

            var stacks1 = ParseStacks(lines);
            ExecMovements(stacks1, movements);
            Console.WriteLine($"Answer #1: { GetTopCrates(stacks1) }");

            var stacks2 = ParseStacks(lines);
            ExecMovements2(stacks2, movements);
            Console.WriteLine($"Answer #2: { GetTopCrates(stacks2) }");

            Console.ReadKey();
        }
    }
}
