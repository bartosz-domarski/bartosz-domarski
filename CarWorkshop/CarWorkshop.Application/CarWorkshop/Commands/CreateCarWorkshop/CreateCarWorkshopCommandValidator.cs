using CarWorkshop.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommandValidator : AbstractValidator<CreateCarWorkshopCommand>
    {
        public CreateCarWorkshopCommandValidator(ICarWorkshopRepository repository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(2, 20)
                .Custom((value, context) =>
                {
                    var carWorkshopIsExist = repository.GetByName(value).Result;

                    if (carWorkshopIsExist != null)
                    {
                        context.AddFailure("This name is already in use");
                    }
                });

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Length(8, 12);
        }
    }
}
