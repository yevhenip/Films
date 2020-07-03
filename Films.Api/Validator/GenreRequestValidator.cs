using Films.Api.Requests;
using FluentValidation;

namespace Films.Api.Validator
{
    public class GenreRequestValidator: AbstractValidator<GenreRequest>
    {
        public GenreRequestValidator()
        {
            RuleFor(g => g.Name).NotNull().NotEmpty()
                .WithMessage("Genre name is required");
            RuleFor(g => g.Name).MaximumLength(50)
                .WithMessage("Genre name should be less than 50");
            RuleForEach(g => g.VideosId).NotNull()
                .WithMessage("Video cannot be null");
        }
    }
}