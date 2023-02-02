namespace Common_Digit
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Integer input:");
            int n = int.Parse(Console.ReadLine());
            if ((n >= 2) && (n <= 20))
            {
                Console.WriteLine("Array input");
                string[] array = Console.ReadLine().Split(' ');
                if (array.Length == n)
                {
                    string input = string.Join("", array);
                    int num = input.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First();
                    Console.WriteLine(num);
                }
                else
                    Console.WriteLine("Wrong array length");
            }
            else
                Console.WriteLine("Wrong input");
        }
    }
}