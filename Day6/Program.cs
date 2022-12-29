using System;
using System.IO;
using System.Collections.Generic;

namespace Day6
{
    class MainClass
    {
        public static int GetStartOfPacketMarkerPos(in string buffer)
        {
            for (int i = 0; i < buffer.Length - 4; ++i)
            {
                var set = new HashSet<char> {
                    buffer[i],
                    buffer[i + 1],
                    buffer[i + 2],
                    buffer[i + 3]
                };
                if (set.Count == 4)
                    return i + 4;
            }
            throw new ArgumentException();
        }

        public static int GetStartOfMessageMarkerPos(in string buffer)
        {
            for (int i = 0; i < buffer.Length - 14; ++i)
            {
                var set = new HashSet<char> { };
                for (int j = 0; j < 14; ++j)
                {
                    set.Add(buffer[i + j]);
                }
                if (set.Count == 14)
                    return i + 14;
            }
            throw new ArgumentException();
        }

        public static void Main(string[] args)
        {
            var buffer = File.ReadAllText("input.txt").TrimEnd();

            Console.WriteLine($"Answer #1: { GetStartOfPacketMarkerPos(buffer) }");
            Console.WriteLine($"Answer #2: { GetStartOfMessageMarkerPos(buffer) }");

            Console.ReadKey();
        }
    }
}
