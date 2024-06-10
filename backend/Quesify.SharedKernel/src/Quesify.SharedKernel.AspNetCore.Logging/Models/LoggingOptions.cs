﻿namespace Quesify.SharedKernel.AspNetCore.Logging.Models;

public class LoggingOptions
{
    public LoggingLevel LoggingLevel { get; set; }

    public bool IgnoreRequestHeader { get; set; }

    public bool IgnoreRequestBody { get; set; }

    public bool IgnoreResponseHeader { get; set; }

    public bool IgnoreResponseBody { get; set; }

    public LoggingOptions(
        LoggingLevel logLevel = LoggingLevel.Informational,
        bool ignoreRequestHeader = false,
        bool ignoreRequestBody = false,
        bool ignoreResponseHeader = false,
        bool ignoreResponseBody = false)
    {
        LoggingLevel = logLevel;
        IgnoreRequestHeader = ignoreRequestHeader;
        IgnoreRequestBody = ignoreRequestBody;
        IgnoreResponseHeader = ignoreResponseHeader;
        IgnoreResponseBody = ignoreResponseBody;
    }
}
