namespace HardcoreGamesRanking.Business.Common.Exceptions
{
    using FluentValidation.Results;

    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation errors have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}