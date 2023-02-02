namespace Palindrom
{
    class Program
    {
        public static string Method(string input)
        {
            if (input != string.Empty)
            {
                char[] arr = input.ToCharArray();
                arr = Array.FindAll<char>(arr, (l => char.IsLetter(l)));
                string inputNew = new string(arr);
                string inputNewReverse = new string(arr.Reverse().ToArray());

                if (inputNew == inputNewReverse)
                    return "YES";
                else
                    return "NO";
            }
            else
                return "Empty string";
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Input string:");
            string input = Console.ReadLine();
            Console.WriteLine(Method(input));
        }
    }
}