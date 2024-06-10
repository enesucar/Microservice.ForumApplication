﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Quesify.SharedKernel.AspNetCore.HttpProblemDetails;
using Quesify.SharedKernel.Validation;

namespace Quesify.SharedKernel.AspNetCore.Filters;

public class ValidationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var validationFailuresResult = new List<ValidationFailureResult>();
        foreach (var key in context.ModelState.Keys)
        {
            var errors = context.ModelState[key]!.Errors;
            if (errors.Any())
            {
                var validationFailureResult = new ValidationFailureResult() { PropertyName = key };
                foreach (var error in errors)
                {
                    validationFailureResult.ErrorMessages.Add(error.ErrorMessage);
                }
                validationFailuresResult.Add(validationFailureResult);
            }
        }

        context.Result =
            new BadRequestObjectResult(
                new ValidationFailureProblemDetails(
                    ErrorMessages.ValidationFailureErrorMessage,
                    context.HttpContext.Request.Path,
                    validationFailuresResult
                )
            );
    }
}
