namespace HardcoreGamesRanking.Business.Features.User.Validators
{
    using Business.Features.User.Commands;
    using Core.Messages;
    using FluentValidation;

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
		public CreateUserCommandValidator()
		{
            RuleFor(entity => entity.Username)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .WithName("Username");

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