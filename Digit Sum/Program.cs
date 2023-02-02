namespace Digit_Sum
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Size of array input");
            int size = int.Parse(Console.ReadLine());
            int[] sums = new int[size];

            Console.WriteLine("Array input");
            int[] array = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            for (int i = 0; i < array.Length; i++)
            {
                int sum = 0;
                for (int n = array[i]; n > 0; sum += n % 10, n /= 10);
                sums[i] = sum;
            }

            Console.WriteLine(Array.IndexOf(sums, sums.Max()));
        }
    }
}