namespace SqlQueryBuilder;

/// <summary>
/// Marker interface for any clause that can appear in a WHERE statement.
/// </summary>
public interface IWhere;

/// <summary>
/// Represents a single filter condition (e.g. Table.Id = 1).
/// </summary>
public record SqlWhere(SqlColumn Column, Operator Operator, object Value) : IWhere
{
    /// <summary>
    /// Formats the single condition as a valid SQL string, handling string quoting and date formatting.
    /// Example: "Table.Column = 'Value'"
    /// </summary>
    public override string ToString()
    {
        var val = SqlValueFormatter.Format(Value);
        return $"{Column.TableName}.{Column.ColumnName} {Operator.ToSqlString()} {val}";
    }
}

/// <summary>
/// Combines multiple conditions with the AND logical operator.
/// </summary>
public record And(IWhere[] Wheres) : IWhere
{
    /// <summary>
    /// Joins all inner conditions with " AND " and wraps the result in parentheses to ensure operator precedence.
    /// Example: "(A = 1 AND B = 2)"
    /// </summary>
    public override string ToString()
    {
        return "(" + string.Join(" AND ", Wheres) + ")";
    }
}

/// <summary>
/// Combines multiple conditions with the OR logical operator.
/// </summary>S
public record Or(IWhere[] Wheres) : IWhere
{
    /// <summary>
    /// Joins all inner conditions with " OR " and wraps the result in parentheses to ensure operator precedence.
    /// Example: "(A = 1 OR B = 2)"
    /// </summary>
    public override string ToString()
    {
        return "(" + string.Join(" OR ", Wheres) + ")";
    }
}