namespace HardcoreGamesRanking.Business.Features.Rating.Validators
{
    using FluentValidation;
    using Business.Features.Rating.Queries;
    using Core.Messages;

    public class GetRankingValidator : AbstractValidator<GetRankingQuery>
    {
        public GetRankingValidator()
        {
            RuleFor(entity => entity.Top)
                .Must(Top => Top == null || Top > 0).WithMessage(ErrorMessage.GreaterThanZero)
                .WithName("Top");
        }
    }
}