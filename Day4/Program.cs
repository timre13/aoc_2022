using System;
using System.IO;
using System.Linq;

namespace Day4
{
    class Range
    {
        public int From { get; private set; }
        public int To { get; private set; }

        public Range(in string range)
        {
            var fields = range.Split('-');
            From = int.Parse(fields[0]);
            To = int.Parse(fields[1]);
        }
    }

    class RangePair
    {
        public Range Range1 { get; private set; }
        public Range Range2 { get; private set; }

        public RangePair(in string line)
        {
            var fields = line.Split(',');
            Range1 = new Range(fields[0]);
            Range2 = new Range(fields[1]);
        }

        public bool IsOneInsideAnother()
        {
            return Range1.From >= Range2.From && Range1.To <= Range2.To
                || Range2.From >= Range1.From && Range2.To <= Range1.To;
        }

        public bool HasOverlap()
        {
            return Range1.From >= Range2.From && Range1.From <= Range2.To
                || Range1.To >= Range2.From && Range1.To <= Range2.To
                || Range2.From >= Range1.From && Range2.From <= Range1.To
                || Range2.To >= Range1.From && Range2.To <= Range1.To;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");

            var pairs = lines.Where(x => x.Length != 0)
                .Select(x => new RangePair(x)).ToArray();

            Console.WriteLine($"Answer #1: {pairs.Count(x => x.IsOneInsideAnother())}");
            Console.WriteLine($"Answer #2: {pairs.Count(x => x.HasOverlap())}");

            Console.ReadKey();
        }
    }
}
