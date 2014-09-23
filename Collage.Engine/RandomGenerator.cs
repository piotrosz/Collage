namespace Collage.Engine
{
    using System;

    internal class RandomGenerator : IRandomGenerator
    {
        private readonly Random random;

        public RandomGenerator()
        {
            this.random = new Random();
        }

        public int Next(int min, int max)
        {
            return this.random.Next(min, max);
        }
    }
}
