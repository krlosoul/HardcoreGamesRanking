namespace HardcoreGamesRanking.Business.Features.VideoGame.Validators
{
    using FluentValidation;
    using Business.Features.VideoGame.Queries;
    using Core.Messages;

    public class GetAllVideoGameFilterValidator : AbstractValidator<GetAllVideoGameFilterQuery>
    {
        public GetAllVideoGameFilterValidator()
        {
            RuleFor(entity => entity.PageNumber)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .Must(id => id > 0).WithMessage(ErrorMessage.GreaterThanZero)
                .WithName("PageNumber");

            RuleFor(entity => entity.PageSize)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .Must(id => id > 0).WithMessage(ErrorMessage.GreaterThanZero)
                .WithName("PageSize");
        }
    }
}