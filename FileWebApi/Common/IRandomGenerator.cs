namespace FileWebApi.Common
{
    /// <summary>
    /// Abstract random function.
    /// </summary>
    public interface IRandomGenerator
    {
        /// <summary>
        /// The Next.
        /// </summary>
        /// <param name="min">The min<see cref="int"/>.</param>
        /// <param name="max">The max<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        int Next(int min, int max);
    }
}
