namespace FileWebApi.Common
{
    using System;

    /// <summary>
    /// Defines the <see cref="RandomGenerator" />.
    /// </summary>
    public class RandomGenerator : IRandomGenerator
    {
        /// <summary>
        /// Defines the _random.
        /// </summary>
        private Random _random = new Random();

        /// <summary>
        /// The Next.
        /// </summary>
        /// <param name="min">The min<see cref="int"/>.</param>
        /// <param name="max">The max<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int Next(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
