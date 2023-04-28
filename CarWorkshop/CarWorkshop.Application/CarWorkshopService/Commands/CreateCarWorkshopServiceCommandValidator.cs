using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopService.Commands
{
    public class CreateCarworkshopServiceCommandValidator : AbstractValidator<CreateCarWorkshopServiceCommand>
    {
        public CreateCarworkshopServiceCommandValidator()
        {
            RuleFor(c => c.Cost).NotEmpty().NotNull();
            RuleFor(c => c.Description).NotEmpty().NotNull();
            RuleFor(c => c.CarWorkshopEncodedName).NotEmpty().NotNull();
        }
    }
}
