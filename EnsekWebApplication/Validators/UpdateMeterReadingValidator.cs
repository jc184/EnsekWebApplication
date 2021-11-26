using Entities.DTOs;
using FluentValidation;
using System;

namespace EnsekWebApplication.Validators
{
    public class UpdateMeterReadingValidator : AbstractValidator<MeterReadingsForCreationDTO>
    {
        public UpdateMeterReadingValidator()
        {
            RuleFor(x => x.MeterReadingDateTime).NotNull().NotEmpty().WithMessage("MeterReadingDateTime is required.");
            RuleFor(x => x.MeterReadingDateTime.Equals(DateTime.Now));
            RuleFor(x => x.MeterReadValue).NotNull().NotEmpty().WithMessage("MeterReadValue is required.");
            RuleFor(x => x.MeterReadValue.Length.Equals(5));
            RuleFor(x => x.MeterReadValue)
                .Custom((x, context) =>
                {
                    if ((!(int.TryParse(x, out int value)) || value < 10000))
                    {
                        context.AddFailure($"{x} is not a valid number or less than 10000");
                    }
                });
            RuleFor(x => x.AccountId).NotNull().NotEmpty().WithMessage("AccountId is required.");
            RuleFor(x => x.AccountId).GreaterThan(0);
            RuleFor(x => x.AccountId).LessThan(10000);
        }
    }
}
