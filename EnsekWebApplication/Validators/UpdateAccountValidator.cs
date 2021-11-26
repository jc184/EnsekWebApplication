using Entities.DTOs;
using FluentValidation;

namespace EnsekWebApplication.Validators
{
    public class UpdateAccountValidator : AbstractValidator<AccountForUpdateDTO>
    {
        public UpdateAccountValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required.");
            RuleFor(x => x.FirstName).MinimumLength(3);
            RuleFor(x => x.FirstName).MaximumLength(20);
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required.");
            RuleFor(x => x.LastName).MinimumLength(3);
            RuleFor(x => x.LastName).MaximumLength(20);
            RuleFor(x => x.AccountId).NotNull().NotEmpty().WithMessage("AccountId is required.");
            RuleFor(x => x.AccountId).GreaterThan(0);
            RuleFor(x => x.AccountId).LessThan(10000);
        }
    }
}
