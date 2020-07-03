using Films.Api.Requests.RoleRequests;
using FluentValidation;

namespace Films.Api.Validator.RoleRequestsValidator
{
    public class EditRoleRequestValidator : AbstractValidator<EditRoleRequest>
    {
        public EditRoleRequestValidator()
        {
            RuleFor(r => r.OldName).NotEmpty().NotNull()
                .WithMessage("Role is required");
            RuleFor(r => r.NewName).NotEmpty().NotNull()
                .WithMessage("Role is required");
        }
    }
}