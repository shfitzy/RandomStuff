using System;

namespace Scratchpad.CoinFlipMadness
{
    public class Coin
    {
        private readonly string name;
        private readonly int value;
        private readonly double odds;

        private double rating = 0;

        public Coin(string name, int value, double odds)
        {
            this.name = name;
            this.value = value;
            this.odds = odds;
        }

        public double CalculateRating(int flips, int targetScore)
        {
            int targetNumHeads = CalculateTargetNumHeads(flips, targetScore);
            if (targetNumHeads > flips)
            {
                rating = 0;
            }
            else
            {
                rating = SmotMathCache.GetBinomialCoefficientSummation(flips, targetNumHeads, odds);
            }

            return rating;
        }

        private int CalculateTargetNumHeads(int flips, int targetScore)
        {
            int netGain = (int) Math.Round(targetScore * 1.0 / value, MidpointRounding.ToPositiveInfinity);
            return (int) Math.Round((flips * 1.0 / 2) + (netGain * 1.0 / 2), MidpointRounding.ToPositiveInfinity);
        }

        public int Flip(Random r)
        {
            return (r.NextDouble() < odds) ? value : (value * -1);
        }

        public string GetName()
        {
            return name;
        }

        public int GetValue()
        {
            return value;
        }

        public double GetOdds()
        {
            return odds;
        }

        public double GetRating()
        {
            return rating;
        }
    }
}
