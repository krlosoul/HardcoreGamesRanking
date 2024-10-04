namespace HardcoreGamesRanking.Business.Features.User.Validators
{
    using FluentValidation;
    using Business.Features.User.Queries;
    using Core.Messages;

    public class AuthenticateQueryValidator : AbstractValidator<AuthenticateQuery>
    {
        public AuthenticateQueryValidator()
        {
            RuleFor(entity => entity.Email)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .EmailAddress().WithMessage(ErrorMessage.EmailError)
                .WithName("Email");

            RuleFor(entity => entity.Password)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .WithName("Password");
        }
    }
}