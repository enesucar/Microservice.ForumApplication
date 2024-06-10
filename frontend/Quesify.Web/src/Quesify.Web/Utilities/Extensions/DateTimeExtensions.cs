namespace System;

public static class DateTimeExtensions
{
    public static DateTimeOffset ToDatetimeOffsetFromUtc(this DateTime date)
    {
        return new DateTimeOffset(DateTime.SpecifyKind(date, DateTimeKind.Utc));
    }
}
