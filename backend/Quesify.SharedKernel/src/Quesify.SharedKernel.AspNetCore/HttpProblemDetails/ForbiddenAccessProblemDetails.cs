﻿using Microsoft.AspNetCore.Mvc;

namespace Quesify.SharedKernel.AspNetCore.HttpProblemDetails;

public class ForbiddenAccessProblemDetails : ProblemDetails
{
    public ForbiddenAccessProblemDetails(string? detail, string? instance)
    {
        Title = "Forbidden Access";
        Status = StatusCodes.Status403Forbidden;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3";
        Detail = detail;
        Instance = instance;
    }
}
