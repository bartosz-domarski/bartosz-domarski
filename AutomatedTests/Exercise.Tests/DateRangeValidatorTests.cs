using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Tests
{
    public class DateRangeValidatorTests
    {
        List<DateRange> _dateRanges = new List<DateRange>()
        {
            new DateRange( new DateTime(2022, 1, 8), new DateTime(2022, 1, 14)),
            new DateRange( new DateTime(2022, 1, 22), new DateTime(2022, 1, 23)),
            new DateRange( new DateTime(2021, 12, 31), new DateTime(2022, 1, 22)),
            new DateRange( new DateTime(2022, 1, 5), new DateTime(2022, 1, 14))
        };

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ValidateOverlapping_ForGivenDateRange_ReturnsTrueValue(int index)
        {
            //arrange
            DateRange dateRange = _dateRanges[index];

            List<DateRange> dateRanges = new List<DateRange> 
            { 
                new DateRange( new DateTime(2022, 1, 1), new DateTime(2022, 1, 7)),
                new DateRange( new DateTime(2022, 1, 15), new DateTime(2022, 1, 21))
            };

            Validator validator = new Validator();

            //act
            bool result = validator.ValidateOverlapping(dateRanges, dateRange);

            //assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3 )]
        public void ValidateOverlapping_ForGivenDateRange_ReturnsFalseValue(int index)
        {
            //arrange
            DateRange dateRange = _dateRanges[index];

            List<DateRange> dateRanges = new List<DateRange>
            {
                new DateRange( new DateTime(2022, 1, 1), new DateTime(2022, 1, 7)),
                new DateRange( new DateTime(2022, 1, 15), new DateTime(2022, 1, 21))
            };

            Validator validator = new Validator();

            //act
            bool result = validator.ValidateOverlapping(dateRanges, dateRange);

            //assert
            result.Should().BeFalse();
        }
    }
}
