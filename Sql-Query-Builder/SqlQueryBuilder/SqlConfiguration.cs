namespace SqlQueryBuilder;

/// <summary>
/// Global configuration for SQL generation settings.
/// </summary>
public static class SqlConfiguration
{
    /// <summary>
    /// The format string used for DateTime values. Defaults to "yyyy-MM-dd HH:mm:ss.fff".
    /// </summary>
    public static string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fff";
}