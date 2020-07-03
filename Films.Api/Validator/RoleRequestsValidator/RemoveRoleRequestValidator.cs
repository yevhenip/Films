using System.Linq;
using Films.Api.Requests.RoleRequests;
using FluentValidation;

namespace Films.Api.Validator.RoleRequestsValidator
{
    public class RemoveRoleRequestValidator : AbstractValidator<RemoveRoleRequest>
    {
        public RemoveRoleRequestValidator()
        {
            RuleFor(u => u.UserName).Must(u => u.Any(char.IsLetterOrDigit))
                .WithMessage("UserName should contain only letters abd digits");
            RuleFor(u => u.UserName).MinimumLength(6)
                .WithMessage("UserName should be at least 6 letters");
            RuleFor(u => u.UserName).MaximumLength(50)
                .WithMessage("UserName should be maximum 50 letters");
            
            RuleFor(r => r.RoleName).NotEmpty().NotNull()
                .WithMessage("Role is required");
        }
    }
}