using ContactAPI.Entities;
using FluentValidation;

namespace ContactAPI.Models.Validators
{
    public class CreateContactDtoValidator : AbstractValidator<CreateContactDto>
    {
        public CreateContactDtoValidator(ContactDbContext dbContext)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.Category)
                .NotEmpty();

            RuleFor(x => x.Password).NotEmpty().WithMessage("Your password cannot be empty")
                   .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                   .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                   .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                   .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                   .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                   .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");

            RuleFor(x => new { x.Category, x.Subcategory }).Custom((value, context) =>
            {
                var subcategories = Contact.SubcategoriesAvailable.FirstOrDefault(s => s.Key == value.Category.ToLower());

                if (subcategories.Key is not null && !subcategories.Value.Contains(value.Subcategory.ToLower()))
                    context.AddFailure("Subcategory", $"For category: {value.Category} available subcategories are: " +
                        $"[{string.Join(", ", Contact.SubcategoriesAvailable[value.Category])}]");
            });

            RuleFor(x => x.Subcategory)
                .Empty()
                .When(x => !Contact.SubcategoriesAvailable.Keys.Contains(x.Category.ToLower()))
                .WithMessage(x => $"For categories other then: " +
                    $"[{string.Join(", ", Contact.SubcategoriesAvailable.Keys)}] subcategory must be empty");

            RuleFor(x => x.Email)
                .EmailAddress()
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Contacts.Any(c => c.Email == value);

                    if (emailInUse)
                        context.AddFailure("Email", "That email is already added");
                });
        }
    }
}