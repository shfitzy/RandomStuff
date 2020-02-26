using System;
using System.Collections.Generic;

namespace Scratchpad.CoinFlipMadness
{
    class SmotMathCache
    {
        private static readonly Dictionary<string, double> binomialCoeffCache = new Dictionary<string, double>();

        public static void GenerateBinomialCoeffCache(int maxN)
        {
            for(int n = 1; n <= maxN; n++)
            {
                for(int k = 1; k <= n; k++)
                {
                    double coefficient = CalculateCoefficient(n, k);
                    binomialCoeffCache.Add("(" + n + "," + k + ")", coefficient);
                }
            }
        }

        public static double GetBinomialCoefficient(int n, int k)
        {
            string key = "(" + n + "," + k + ")";

            binomialCoeffCache.TryGetValue(key, out double value);
            return value;
        }

        public static double GetBinomialCoefficientSummation(int n, int k, double odds)
        {
            double totalValue = 0.0;

            for(int i = k; i <= n; i++)
            {
                totalValue += GetBinomialCoefficient(n, i) * Math.Pow(odds, i) * Math.Pow(1 - odds, n - i);
            }

            return totalValue;
        }

        private static double CalculateCoefficient(int n, int k)
        {
            double result = 1;

            for (int i = 1; i <= k; i++)
            {
                result *= n - (k - i);
                result /= i;
            }
            return result;
        }
    }
}
