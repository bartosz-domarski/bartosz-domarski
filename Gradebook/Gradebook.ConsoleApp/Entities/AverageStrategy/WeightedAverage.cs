namespace Gradebook.ConsoleApp.Entities.AverageStrategy
{
    public class WeightedAverage : IAverage
    {
        public float Calculate(IEnumerable<float> grades, IEnumerable<int> weights)
        {
            var weightedSum = 0f;
            var totalWeight = 0;

            for (int i = 0; i < grades.Count(); i++)
            {
                var grade = grades.ElementAt(i);
                var weight = weights.ElementAt(i);

                weightedSum += grade * weight;
                totalWeight += weight;
            }

            return float.Parse((weightedSum / totalWeight).ToString("F2"));
        }
    }
}
