namespace HardcoreGamesRanking.Business.Features.VideoGame.Validators
{
    using FluentValidation;
    using Business.Features.VideoGame.Commands;
    using Core.Messages;

    public class DeleteVideoGameValidator : AbstractValidator<DeleteVideoGameCommand>
    {
        public DeleteVideoGameValidator()
        {
            RuleFor(entity => entity.IdVideoGame)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .Must(IdVideoGame => IdVideoGame > 0).WithMessage(ErrorMessage.GreaterThanZero)
                .WithName("IdVideoGame");
        }
    }
}