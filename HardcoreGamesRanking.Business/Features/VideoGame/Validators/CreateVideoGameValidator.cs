namespace HardcoreGamesRanking.Business.Features.VideoGame.Validators
{
    using FluentValidation;
    using Business.Features.VideoGame.Commands;
    using Core.Messages;

    public class CreateVideoGameValidator : AbstractValidator<CreateVideoGameCommand>
    {
        public CreateVideoGameValidator()
        {
            RuleFor(entity => entity.Nombre)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .WithName("Nombre");

            RuleFor(entity => entity.IdCompania)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .Must(IdCompania => IdCompania > 0).WithMessage(ErrorMessage.GreaterThanZero)
                .WithName("IdCompania");

            RuleFor(entity => entity.AnioLanzamiento)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .Must(AnioLanzamiento => AnioLanzamiento > 0).WithMessage(ErrorMessage.GreaterThanZero)
                .WithName("AnioLanzamiento");

            RuleFor(entity => entity.Precio)
                .NotNull().WithMessage(ErrorMessage.NullError)
                .NotEmpty().WithMessage(ErrorMessage.EmptyError)
                .Must(Precio => Precio > 0).WithMessage(ErrorMessage.GreaterThanZero)
                .WithName("Precio");
        }
    }
}