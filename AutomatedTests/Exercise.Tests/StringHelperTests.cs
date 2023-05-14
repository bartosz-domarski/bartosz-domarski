namespace Exercise.Tests
{
    public class StringHelperTests
    {
        [Theory]
        [InlineData("ala ma kota", "kota ma ala")]
        [InlineData("to jest test", "test jest to")]
        public void ReverseWords_ForGivenSentence_ReturnsReversedSentence(string words, string wordsReversed)
        {
            //act
            var result = StringHelper.ReverseWords(words);

            //assert
            result.Should().Be(wordsReversed);
        }

        [Theory]
        [InlineData("ala")]
        [InlineData("AlA")]
        [InlineData("KAJAK")]
        public void IsPalindrome_ForGivenPalindrome_ReturnsTrueValue(string word)
        {
            //act
            var result = StringHelper.IsPalindrome(word);

            //assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("Ala")]
        [InlineData("AlAK")]
        [InlineData("Kajak")]
        public void IsPalindrome_ForGivenIncorrectPalindrome_ReturnsFalseValue(string word)
        {
            //act
            var result = StringHelper.IsPalindrome(word);

            //assert
            result.Should().BeFalse();
        }
    }
}