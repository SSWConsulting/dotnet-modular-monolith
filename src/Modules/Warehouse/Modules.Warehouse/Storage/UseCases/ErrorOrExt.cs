using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Modules.Warehouse.Storage.UseCases;

public static class ErrorOrExt
{
    public static IResult Problem(this IErrorOr error)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(error.IsError, false);

        if (error.Errors is null)
            return Problem(Error.Unexpected());

        if (error.Errors.All(e => e.Type == ErrorType.Validation))
            return ValidationProblem(error);

        return Problem(error.Errors[0]);
    }

    private static ProblemHttpResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return TypedResults.Problem(statusCode: statusCode, title: error.Description);
    }

    private static ValidationProblem ValidationProblem(IErrorOr error)
    {
        var errors = new Dictionary<string, string[]>();
        foreach (var e in error.Errors!)
            errors.Add(e.Code, [e.Description]);

        return TypedResults.ValidationProblem(errors, title: "One or more validation errors occurred.");
    }
}