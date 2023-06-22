namespace Gradebook.Entities.AverageStrategy
{
    public class Average
    {
        private readonly IAverage _average;

        public Average(IAverage average)
        {
            _average = average;
        }

        public float Calculate(IEnumerable<float> grades, IEnumerable<int> weights) =>
            _average.Calculate(grades, weights);
    }
}
