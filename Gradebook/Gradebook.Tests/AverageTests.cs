using Gradebook.ConsoleApp.Entities.AverageStrategy;
using Xunit;

namespace Gradebook.Tests
{
    public class AverageTests
    {
        [Fact]
        public void ArithmeticAverage_Calculate_ChecksIfAverageIsCalculatedCorrectly()
        {
            //arrange
            var grades = new List<float>() { 1, 2, 3, 4, 5 };
            var arithmeticAverage = new Average(new ArithmeticAverage());

            //act
            var result = arithmeticAverage.Calculate(grades, new List<int>());

            //assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void WeightedAverage_Calculate_ChecksIfAverageIsCalculatedCorrectly()
        {
            //arrange
            var grades = new List<float>() { 1, 2, 3, 4 };
            var weights = new List<int>() { 1, 2, 3, 4 };
            var weightedAverage = new Average(new WeightedAverage());

            //act
            var result = weightedAverage.Calculate(grades, weights);

            //assert
            Assert.Equal(3, result);
        }
    }
}
