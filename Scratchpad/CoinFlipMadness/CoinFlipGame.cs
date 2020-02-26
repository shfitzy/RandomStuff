using System;

namespace Scratchpad.CoinFlipMadness
{
    public class CoinFlipGame
    {
        readonly Random r = new Random();

        static void Main(string[] args)
        {
            CoinFlipGame game = new CoinFlipGame();

            int numFlips = 100;
            SmotMathCache.GenerateBinomialCoeffCache(numFlips);

            for (int i = 0; i <= 100; i++)
            {
                Coin coinA = new Coin("A", 1, i * 0.01);
                Coin coinB = new Coin("B", 2, 0.5);

                game.RunSimulations(numFlips, 1000, coinA, coinB);
            }
        }

        public void RunSimulations(int numFlips, int numSimulations, Coin coinA, Coin coinB)
        {
            int startingScore = 0;
            int targetScore = 1;

            int wins = 0, losses = 0;

            for (int i = 0; i < numSimulations; i++)
            {
                if (PlayGame(coinA, coinB, numFlips, startingScore, targetScore))
                {
                    wins++;
                }
                else
                {
                    losses++;
                }
            }

            //Console.WriteLine("Wins: " + wins);
            //Console.WriteLine("Losses: " + losses);
            Console.WriteLine("Coin A {0} - Win percentage: {1}", coinA.GetOdds().ToString("P02"), ((1.0 * wins) / (wins + losses)).ToString("P02"));
        }

        public bool PlayGame(Coin coinA, Coin coinB, int numFlips, int currentScore, int targetScore)
        {
            for (int flipsRemaining = numFlips; flipsRemaining > 0; flipsRemaining--)
            {
                int currentTargetScore = targetScore - currentScore;

                coinA.CalculateRating(flipsRemaining, currentTargetScore);
                coinB.CalculateRating(flipsRemaining, currentTargetScore);

                Coin coinToFlip = ChooseCoinToFlip(coinA, coinB);
                currentScore += coinToFlip.Flip(r);
            }

            return currentScore >= targetScore;
        }

        private Coin ChooseCoinToFlip(Coin coinA, Coin coinB)
        {
            if (coinA.GetRating() > coinB.GetRating())
            {
                return coinA;
            }
            else if (coinA.GetRating() < coinB.GetRating())
            {
                return coinB;
            }
            else if (coinA.GetValue() < coinB.GetValue()) 
            {
                return coinA;
            }
            else
            {
                return coinB;
            }
        }
    }
}
