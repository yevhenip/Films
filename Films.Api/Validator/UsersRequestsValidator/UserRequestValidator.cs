using System.Linq;
using Films.Api.Requests.UserRequests;
using FluentValidation;

namespace Films.Api.Validator.UsersRequestsValidator
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(u => u.Id).NotNull()
                .WithMessage("Id is required");
            RuleFor(u => u.UserName).Must(u => u.Any(char.IsLetterOrDigit))
                .WithMessage("UserName should contain only letters abd digits");
            RuleFor(u => u.UserName).MinimumLength(6)
                .WithMessage("UserName should be at least 6 letters");
            RuleFor(u => u.UserName).MaximumLength(50)
                .WithMessage("UserName should be maximum 50 letters");
        }
    }
}