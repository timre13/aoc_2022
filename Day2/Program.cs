using System;
using System.IO;
using System.Linq;

namespace Day2
{
    class MainClass
    {
        public enum Outcome
        {
            Win,
            Draw,
            Lose,
        }

        public enum Shape
        {
            Rock,
            Paper,
            Scissors
        }

        public static Outcome GetOutcome(Shape first, Shape second)
        {
            Outcome[,] outcomes = {
                // Rock        | Paper      | Scissors
                { Outcome.Draw, Outcome.Win, Outcome.Lose }, // Rock
                { Outcome.Lose, Outcome.Draw, Outcome.Win }, // Paper
                { Outcome.Win, Outcome.Lose, Outcome.Draw }, // Scissors
            };
            return outcomes[(int)first, (int)second];
        }

        public static int OutcomeToScore(Outcome outcome)
        {
            switch (outcome)
            {
                case Outcome.Win: return 6;
                case Outcome.Draw: return 3;
                case Outcome.Lose: return 0;
            }
            throw new ArgumentOutOfRangeException();
        }

        public static int ShapeToScore(Shape shape)
        {
            switch (shape)
            {
                case Shape.Rock: return 1;
                case Shape.Paper: return 2;
                case Shape.Scissors: return 3;
            }
            throw new ArgumentOutOfRangeException();
        }

        public static Shape CharToShape(char c)
        {
            switch (c)
            {
                case 'A':
                case 'X':
                    return Shape.Rock;
                case 'B':
                case 'Y':
                    return Shape.Paper;
                case 'C':
                case 'Z':
                    return Shape.Scissors;
            }
            throw new ArgumentOutOfRangeException();
        }

        public static int CalcScoreForRound(Shape shape1, Shape shape2)
        {
            return ShapeToScore(shape2) + OutcomeToScore(GetOutcome(shape1, shape2));
        }

        public static Shape GetResponse(Shape shape, Outcome outcome)
        {
            Shape[,] responses = {
                // Win        | Draw      | Lose
                { Shape.Paper, Shape.Rock, Shape.Scissors }, // Rock
                { Shape.Scissors, Shape.Paper, Shape.Rock }, // Paper
                { Shape.Rock, Shape.Scissors, Shape.Paper }, // Scissors
            };
            return responses[(int)shape, (int)outcome];
        }

        public static Outcome CharToOutcome(char c)
        {
            switch (c)
            {
                case 'X': return Outcome.Lose;
                case 'Y': return Outcome.Draw;
                case 'Z': return Outcome.Win;
            }
            throw new ArgumentOutOfRangeException();
        }

        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var finalScore1 = lines
                .Where(x => x.Length != 0)
                .Select(x => CalcScoreForRound(CharToShape(x[0]), CharToShape(x[2])))
                .Sum();
            Console.WriteLine($"Answer #1: { finalScore1 }");

            var finalScore2 = lines
                .Where(y => y.Length != 0)
                .Select(y => CalcScoreForRound(
                    CharToShape(y[0]),
                    GetResponse(CharToShape(y[0]), CharToOutcome(y[2]))))
                .Sum();
            Console.WriteLine($"Answer #2: { finalScore2 }");

            Console.ReadKey();
        }
    }
}
