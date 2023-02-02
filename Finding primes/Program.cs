using System.Collections.Generic;

namespace Finding_primes
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Number of intervals:");
            int c = int.Parse(Console.ReadLine());

            var intervals = new int[c][];
            for (int i = 0; i < c; i++)
            {
                Console.WriteLine("Two integers input:");
                intervals[i] = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            }

            foreach (var interv in intervals)
            {
                int result = 0;
                bool isPrime = true;
                int m = interv[0];
                int n = interv[1];

                if ((1 <= m) || (m <= n) || (n <= 1000000000) || ((n - m) <= 100000))
                {
                    for (int j = m; j <= n; j++)
                    {
                        for (int k = 2; k < j; k++)
                        {
                            if (j % k == 0)
                                isPrime = false;
                        }

                        if (isPrime && (j > 1))
                            result++;
                        else 
                            isPrime = true;
                    }
                }
                else
                    Console.WriteLine("Wrong input");

                if (result > 0)
                    Console.WriteLine(result);
                else
                    Console.WriteLine("0");
            }
        }
    }
}