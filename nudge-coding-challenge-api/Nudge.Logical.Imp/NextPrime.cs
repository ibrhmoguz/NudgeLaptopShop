using System;

namespace Nudge.Logical.Imp
{
    public class NextPrime
    {
        public int NextPrimeFinder(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Please enter natural number!");
            }

            if (number <= 3)
            {
                return number <= 2 ? 2 : 3;
            }

            var isPrime = false;
            while (!isPrime)
            {
                // If number is even, go next number
                if (number % 2 == 0)
                {
                    number += 1;
                }

                // try to divide with odd numbers
                var isDivided = false;
                for (var i = 3; i < number; i += 2)
                {
                    if (number % i == 0)
                    {
                        isDivided = true;
                        number += 1;
                        break;
                    }
                }

                if (!isDivided)
                {
                    isPrime = true;
                }
            }

            return number;
        }
    }
}
