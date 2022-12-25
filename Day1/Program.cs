using System;
using System.IO;
using System.Collections.Generic;

namespace Day1
{
    class Elf
    {
        public int Calories { get; private set; } = 0;

        public void AddCalories(int value)
        {
            Calories += value;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var elves = new List<Elf> { new Elf() };
            for (int i = 0; i < lines.Length - 1; ++i)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    elves.Add(new Elf());
                }
                else
                {
                    elves[elves.Count - 1].AddCalories(Convert.ToInt32(lines[i]));
                }
            }

            elves.Sort((x, y) => Math.Sign(y.Calories - x.Calories));

            Console.WriteLine($"Answer #1: { elves[0].Calories }");

            Console.WriteLine($"Answer #2: " +
                $"{ elves[0].Calories + elves[1].Calories + elves[2].Calories }");

            Console.ReadKey();
        }
    }
}
