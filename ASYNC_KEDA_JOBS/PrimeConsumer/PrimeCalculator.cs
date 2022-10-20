using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeConsumer
{
    internal class PrimeCalculator
    {
        public int GetLargestPrimeUpTo(int upTo)
        {
            int max = 2;
            for (int i = 2; i < upTo; i++)
            {
                bool prime = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        prime = false;
                    }
                }
                if (prime == true && i > max)
                {
                    max = i;
                }
            }
            return max;
        }
    }
}
