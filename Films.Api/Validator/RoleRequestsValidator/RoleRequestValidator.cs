using Films.Api.Requests.RoleRequests;
using FluentValidation;

namespace Films.Api.Validator.RoleRequestsValidator
{
    public class RoleRequestValidator : AbstractValidator<RoleRequest>
    {
        public RoleRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull()
                .WithMessage("Role is required");
        }
    }
}