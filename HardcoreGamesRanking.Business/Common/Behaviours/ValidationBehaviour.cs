namespace HardcoreGamesRanking.Business.Common.Behaviours
{
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;
    using ValidationException = Exceptions.ValidationException;

    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                ValidationContext<TRequest> context = new(request);
                ValidationResult[] validationResults = await Task.WhenAll(
                    _validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken)));
                List<ValidationFailure> failures = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList();
                if (failures.Any())
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}