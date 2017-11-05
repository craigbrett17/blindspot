using System.Collections.Generic;

namespace Toore.Shuffling
{
    public interface IShuffle
    {
        IEnumerable<T> Shuffle<T>(IEnumerable<T> elements);
    }
}