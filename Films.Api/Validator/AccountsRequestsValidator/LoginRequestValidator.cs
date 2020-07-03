﻿using System.Linq;
using Films.Api.Requests.AccountsRequests;
using FluentValidation;

namespace Films.Api.Validator.AccountsRequestsValidator
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(u => u.UserName).NotNull().NotEmpty()
                .WithMessage("UserName is required");
            RuleFor(u => u.UserName).Must(u => u.Any(char.IsLetterOrDigit))
                .WithMessage("UserName should contain only letters abd digits");
            RuleFor(u => u.UserName).MinimumLength(6)
                .WithMessage("UserName should be at least 6 letters");
            RuleFor(u => u.UserName).MaximumLength(50)
                .WithMessage("UserName should be maximum 50 letters");

            RuleFor(u => u.Password).MinimumLength(6)
                .WithMessage("Password should be at least 6 letters");
            RuleFor(u => u.Password).MaximumLength(50)
                .WithMessage("Password should be maximum 50 letters");
        }
    }
}