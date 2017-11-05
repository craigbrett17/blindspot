using System.Collections.Generic;

namespace Toore.Shuffling
{
    public static class ShuffleExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> elements, IShuffle shuffleAlgorithm)
        {
            var shuffledSet = shuffleAlgorithm.Shuffle(elements);
            return shuffledSet;
        }
    }
}