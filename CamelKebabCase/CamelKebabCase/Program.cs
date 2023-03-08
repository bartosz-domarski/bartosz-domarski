using System.Text;

namespace CamelKebabCase
{
    class Program
    {
        public static string ConvertKebabToCamelCase(string input)
        {
            var newString = new StringBuilder();
            int charIdxToSkip = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (charIdxToSkip == i && charIdxToSkip != 0)
                {
                    continue;
                }
                else if (input[i] == '-')
                {
                    newString.Append(char.ToUpper(input[i + 1]));
                    charIdxToSkip = i + 1;
                }
                else
                {
                    newString.Append(input[i]);
                }
            }
            return newString.ToString();
        }

        public static string ConvertCamelToKebabCase(string input)
        {
            var newString = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsUpper(c))
                {
                    newString.Append("-");
                    newString.Append(char.ToLower(c));
                }
                else
                {
                    newString.Append(c);
                }
            }
            return newString.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("1 Convert kebab case to camel case");
            Console.WriteLine("2 Convert camel case to kebab case");
            Console.WriteLine("x Exit the program");

            Console.WriteLine("Input operation number");
            string input = Console.ReadLine();

            while (true)
            {
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Input variable name");
                        Console.WriteLine(ConvertKebabToCamelCase(Console.ReadLine()));
                        break;
                    case "2":
                        Console.WriteLine("Input variable name");
                        Console.WriteLine(ConvertCamelToKebabCase(Console.ReadLine()));
                        break;
                    case "x":
                        return;
                }
                Console.WriteLine("Input operation number");
                input = Console.ReadLine();
            }
        }
    }
}