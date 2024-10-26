using FluentValidation.Results;

namespace Ordering.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException() 
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failuress) : this()
    {
        Errors = failuress
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failure =>
                failure.Key, failuresGroup => failuresGroup.ToArray());
    }
}
