using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Table.Dto.Restaurant;

namespace Table.Dto.Validation
{
    public class RestaurantValidator : AbstractValidator<RestaurantDto>
    {
        public RestaurantValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(dto => dto.City)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(dto => dto.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d{9}$")
                .WithMessage("Invalid phone number.");

            RuleFor(dto => dto.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(dto => dto.OpeningHour)
                .NotEmpty()
                .LessThan(dto => dto.ClosingHour);

            RuleFor(dto => dto.ClosingHour)
                .NotEmpty();
        }
    }
}
