using FluentValidation.TestHelper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Models.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.IntegrationTests.Validators
{
    public class RestaurantQueryValidatorTests
    {
        public static IEnumerable<object[]> GetSampleValidData()
        {
            var list = new List<RestaurantQuery>()
            {
                new RestaurantQuery()
                {
                    pageNumber = 1,
                    pageSize = 10
                },
                new RestaurantQuery()
                {
                    pageNumber = 2,
                    pageSize = 15
                },
                new RestaurantQuery()
                {
                    pageNumber = 22,
                    pageSize = 5,
                    SortBy = nameof(Restaurant.Name)
                },
                new RestaurantQuery()
                {
                    pageNumber = 12,
                    pageSize = 15,
                    SortBy = nameof(Restaurant.Category)
                }
            };

            return list.Select(x => new object[] { x });
        }

        public static IEnumerable<object[]> GetSampleInvalidData()
        {
            var list = new List<RestaurantQuery>()
            {
                new RestaurantQuery()
                {
                    pageNumber = 0,
                    pageSize = 10
                },
                new RestaurantQuery()
                {
                    pageNumber = 2,
                    pageSize = 13
                },
                new RestaurantQuery()
                {
                    pageNumber = 22,
                    pageSize = 5,
                    SortBy = nameof(Restaurant.ContactEmail)
                },
                new RestaurantQuery()
                {
                    pageNumber = 12,
                    pageSize = 15,
                    SortBy = nameof(Restaurant.ContactNumber)
                }
            };

            return list.Select(x => new object[] { x });
        }

        [Theory]
        [MemberData(nameof(GetSampleValidData))]
        public void Validate_ForCorrectModel_ReturnsSuccess(RestaurantQuery model)
        {
            //arrange
            var validator = new RestaurantQueryValidator();

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetSampleInvalidData))]
        public void Validate_ForIncorrectModel_ReturnsFailure(RestaurantQuery model)
        {
            //arrange
            var validator = new RestaurantQueryValidator();

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldHaveAnyValidationError();
        }
    }
}
