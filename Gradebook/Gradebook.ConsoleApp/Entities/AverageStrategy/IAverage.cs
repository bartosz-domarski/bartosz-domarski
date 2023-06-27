namespace Gradebook.ConsoleApp.Entities.AverageStrategy
{
    public interface IAverage
    {
        float Calculate(IEnumerable<float> grades, IEnumerable<int> weights);
    }
}
