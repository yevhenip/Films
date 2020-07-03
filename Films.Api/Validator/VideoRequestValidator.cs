using Films.Api.Requests;
using FluentValidation;

namespace Films.Api.Validator
{
    public class VideoRequestValidator : AbstractValidator<VideoRequest>
    {
        public VideoRequestValidator()
        {
            RuleFor(vr => vr.Title).NotEmpty()
                .WithMessage("Title is required");
            RuleFor(vr => vr.Title).MaximumLength(100)
                .WithMessage("Title must be less than 100");
            RuleFor(vr => vr.Description).MaximumLength(500)
                .WithMessage("Description must be less than 500");
            RuleFor(vr => vr.AuthorId).GreaterThan(0).NotNull()
                .WithMessage("Author is Required");
            RuleFor(vr => vr.Price).InclusiveBetween(0, 5000)
                .WithMessage("Price must be greater than 0 and less than 5000");
            RuleFor(vr => vr.GenresId).NotEmpty().NotNull()
                .WithMessage("Genres is required.");
            RuleForEach(vr => vr.GenresId).NotNull()
                .WithMessage("Genre {CollectionIndex} is required.");
        }
    }
}