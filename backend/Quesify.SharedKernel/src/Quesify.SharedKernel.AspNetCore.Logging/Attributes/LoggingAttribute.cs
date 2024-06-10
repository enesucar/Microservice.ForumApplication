﻿using Quesify.SharedKernel.AspNetCore.Logging.Models;

namespace Quesify.SharedKernel.AspNetCore.Logging.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LoggingAttribute : Attribute
{
    public LoggingLevel? LoggingLevel { get; set; }

    public bool? IgnoreRequestHeader { get; set; }

    public bool? IgnoreRequestBody { get; set; }

    public bool? IgnoreResponseHeader { get; set; }

    public bool? IgnoreResponseBody { get; set; }

    public LoggingAttribute(
        LoggingLevel? logLevel = null,
        bool? ignoreRequestHeader = null,
        bool? ignoreRequestBody = null,
        bool? ignoreResponseHeader = null,
        bool? ignoreResponseBody = null)
    {
        LoggingLevel = logLevel;
        IgnoreRequestHeader = ignoreRequestHeader;
        IgnoreRequestBody = ignoreRequestBody;
        IgnoreResponseHeader = ignoreResponseHeader;
        IgnoreResponseBody = ignoreResponseBody;
    }
}
