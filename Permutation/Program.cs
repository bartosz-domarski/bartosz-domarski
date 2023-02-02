using Microsoft.VisualBasic;

namespace Permutation
{
    class Program
    {
        public static string Method(string input1, string input2)
        {
            string[] inputSplit1 = input1.Split(' ');
            string[] inputSplit2 = input2.Split(' ');
            int[] inputIntArray1 = inputSplit1.Select(int.Parse).ToArray();
            int[] inputIntArray2 = inputSplit2.Select(int.Parse).ToArray();

            if ((inputIntArray1.Length == 11) && (inputIntArray2.Length == 11))
            {
                foreach (int n in inputIntArray1)
                {
                    int numIndex = Array.IndexOf(inputIntArray2, n);
                    inputIntArray2 = inputIntArray2.Where((val, idx) => idx != numIndex).ToArray();
                }

                if (inputIntArray2.Length == 0)
                    return "YES";
                else
                    return "NO";
            }
            else
                return "Wrong format or no 11 integers provided";
        }

        public static void Main(string[] args) 
        {
            Console.WriteLine("Please input 11 integers separated by whitespace, twice:");
            string input1 = Console.ReadLine();
            string input2 = Console.ReadLine();
            Console.WriteLine(Method(input1, input2));
        }
    }
}