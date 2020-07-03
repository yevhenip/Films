using Films.Api.Requests;
using FluentValidation;

namespace Films.Api.Validator
{
    public class AuthorRequestValidator: AbstractValidator<AuthorRequest>
    {
        public AuthorRequestValidator()
        {
            RuleFor(a => a.FullName).NotNull().NotEmpty()
                .WithMessage("Author name is required");
            RuleFor(a => a.FullName).MaximumLength(50)
                .WithMessage("Author name should be less than 50");
        }
    }
}