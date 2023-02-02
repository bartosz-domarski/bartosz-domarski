namespace Reverse_Array
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Array input:");
            var a = Console.ReadLine().Split(' ').ToArray().Reverse();
            
            Console.WriteLine(string.Join(' ', a));
        }
    }
}