namespace Exercise.Tests
{
    public class ListHelperTests
    {
        [Fact]
        public void FilterOddNumber_ForGivenListOfInt_ReturnsOnlyOddNumbers()
        {
            //arrange
            List<int> list = new List<int>() { 1, 2, 3, 444, 555, 23 };
            List<int> list2 = new List<int>() { 1, 3, 555, 23 };

            //act
            List<int> result = ListHelper.FilterOddNumber(list);

            //assert
            result.Should().Equal(list2);
        }
    }
}
