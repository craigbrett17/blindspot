using System;

namespace Toore.Shuffling
{
    public interface IRandomWrapper
    {
        int Next(int minValue, int maxValue);
    }

    public class RandomWrapper : IRandomWrapper 
    {
        private readonly Random _random = new Random();

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}