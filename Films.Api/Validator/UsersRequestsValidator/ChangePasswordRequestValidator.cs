using Films.Api.Requests.UserRequests;
using FluentValidation;

namespace Films.Api.Validator.UsersRequestsValidator
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(u => u.Id).NotNull()
                .WithMessage("Id is required");

            RuleFor(u => u.Email).EmailAddress()
                .WithMessage("Enter correct email");

            RuleFor(u => u.NewPassword).MinimumLength(6)
                .WithMessage("Password should be at least 6 letters");
            RuleFor(u => u.NewPassword).MaximumLength(50)
                .WithMessage("Password should be maximum 50 letters");

            RuleFor(u => u.OldPassword).MinimumLength(6)
                .WithMessage("Password should be at least 6 letters");
            RuleFor(u => u.OldPassword).MaximumLength(50)
                .WithMessage("Password should be maximum 50 letters");
        }
    }
}