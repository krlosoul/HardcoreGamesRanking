namespace HardcoreGamesRanking.Business.Features.VideoGame.Validators
{
    using FluentValidation;
    using Business.Features.VideoGame.Queries;
    using Core.Messages;

    public class GetVideoGameByIdValidator : AbstractValidator<GetVideoGameByIdQuery>
    {
        public GetVideoGameByIdValidator()
        {
            RuleFor(entity => entity.IdVideoGame)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .Must(id => id > 0).WithMessage(ErrorMessage.GreaterThanZero)
                .WithName("IdVideoGame");
        }
    }
}