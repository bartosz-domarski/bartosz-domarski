namespace Gradebook.ConsoleApp.Entities.AverageStrategy
{
    public class ArithmeticAverage : IAverage
    {
        public float Calculate(IEnumerable<float> grades, IEnumerable<int> weights) =>
            float.Parse((grades.Sum() / grades.Count()).ToString("F2"));
    }
}
