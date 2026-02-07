namespace SqlQueryBuilder;

/// <summary>
/// Internal helper class responsible for formatting C# objects into valid SQL string literals.
/// Centralises rules for quoting strings, formatting dates, and handling boolean values.
/// </summary>
internal static class SqlValueFormatter
{
    /// <summary>
    /// Formats a raw object value into a SQL-safe string representation.
    /// </summary>
    /// <param name="value">The object value to format (e.g. string, int, DateTime, bool).</param>
    /// <returns>
    /// A string ready to be embedded in a SQL statement.
    /// <br/>Examples:
    /// <list type="bullet">
    /// <item><description>String: <c>'Event Date'</c></description></item>
    /// <item><description>DateTime: <c>'2026-01-01 12:00:00.000'</c></description></item>
    /// <item><description>Bool: <c>1</c> (for True) or <c>0</c> (for False)</description></item>
    /// </list>
    /// </returns>
    public static string Format(object value)
    {
        // Security Note: In production, use parameters (@p0).

        return value switch
        {
            string str => $"'{str}'",
            DateTime dt => $"'{dt.ToString(SqlConfiguration.DateTimeFormat)}'",
            bool b => b ? "1" : "0",
            _ => value.ToString() ?? "NULL"
        };
    }
}