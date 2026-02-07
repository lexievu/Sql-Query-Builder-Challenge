namespace SqlQueryBuilder;

/// <summary>
/// Represents a database column reference, optionally with an alias.
/// </summary>
/// <param name="TableName">The name of the table containing the column.</param>
/// <param name="ColumnName">The name of the column.</param>
/// <param name="ColumnAlias">Optional alias for the column (e.g. "Event Name").</param>
public record SqlColumn(string TableName, string ColumnName, string? ColumnAlias = null)
{
    /// <summary>
    /// Returns the formatted column string (e.g. "Table.Column AS "Alias"").
    /// </summary>
    public override string ToString()
    {
        if (string.IsNullOrEmpty(ColumnAlias))
            return $"{TableName}.{ColumnName}";
        else
            return $"{TableName}.{ColumnName} AS \"{ColumnAlias}\"";
    }
}