using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiIntegratedWithDocker.Common;

public class ApiProblem
{
    public ApiProblem(string errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public string ErrorCode { get; private set; }

    public string ErrorMessage { get; private set; }
}

public class ApiProblemDetails : ProblemDetails
{
    public IEnumerable<ApiProblem>? Errors { get; set; }
}

public class ApiValidationProblemDetails : ApiProblemDetails
{
    public ApiValidationProblemDetails(IEnumerable<ApiProblem> errors, string path, int statusCode = StatusCodes.Status400BadRequest)
    {
        Status = statusCode;
        Type = "ValidationError";
        Title = "Validation Error.";
        Detail = "One or more validation errors occurred. Please, check errors for details.";
        Errors = errors;
        Instance = path;
    }
}