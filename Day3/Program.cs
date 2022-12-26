using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Rucksack
    {
        public string Compart1 { get; private set; } = "";
        public string Compart2 { get; private set; } = "";
        public string Items
        {
            get
            {
                return Compart1 + Compart2;
            }
        }

        public Rucksack(in string items)
        {
            Compart1 = items.Substring(0, items.Length / 2);
            Compart2 = items.Substring(items.Length / 2, items.Length / 2);
            if (Compart1.Length != Compart2.Length)
                throw new Exception();
        }

        public string GetCommonItems()
        {
            var common = "";
            foreach (char c in Compart1)
            {
                if (Compart2.Contains(c) && !common.Contains(c))
                    common += c;
            }
            return common;
        }
    }

    class MainClass
    {
        public static int ItemToPrior(char item)
        {
            if (char.IsLower(item))
            {
                return item - 'a' + 1;
            }
            return item - 'A' + 27;
        }

        public static char GetCommonBetweenThree(
            in Rucksack rs1, in Rucksack rs2, in Rucksack rs3)
        {
            var set1 = new HashSet<char>(rs1.Items.ToCharArray());
            var set2 = new HashSet<char>(rs2.Items.ToCharArray());
            var set3 = new HashSet<char>(rs3.Items.ToCharArray());

            var inters = set1.Intersect(set2).ToHashSet();
            inters = inters.Intersect(set3).ToHashSet();

            if (inters.Count != 1)
            {
                throw new Exception();
            }

            return inters.First();
        }

        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var rucksacks = lines.Where(x => x.Length != 0).Select(x => new Rucksack(x)).ToArray();

            var priorSum = rucksacks.Select(x => ItemToPrior(x.GetCommonItems()[0])).Sum();
            Console.WriteLine($"Answer #1: {priorSum}");

            int groupCommonPriorSum = 0;
            for (int i = 0; i < rucksacks.Length / 3; i++)
            {
                char common = GetCommonBetweenThree(
                    rucksacks[i * 3 + 0],
                    rucksacks[i * 3 + 1],
                    rucksacks[i * 3 + 2]);

                groupCommonPriorSum += ItemToPrior(common);
            }
            Console.WriteLine($"Answer #2: {groupCommonPriorSum}");

            Console.ReadKey();
        }
    }
}
