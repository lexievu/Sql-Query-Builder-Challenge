namespace SqlQueryBuilder;

/// <summary>
/// Specifies the comparison operator used in a WHERE clause.
/// </summary>
public enum Operator
{
    /// <summary>
    /// Equality operator (=).
    /// </summary>
    Equals,

    /// <summary>
    /// Inequality operator (!=).
    /// </summary>
    NotEquals,

    /// <summary>
    /// Less than operator (&lt;).
    /// </summary>
    LessThan,

    /// <summary>
    /// Less than or equal to operator (&lt;=).
    /// </summary>
    LessThanOrEquals,

    /// <summary>
    /// Greater than operator (&gt;).
    /// </summary>
    GreaterThan,

    /// <summary>
    /// Greater than or equal to operator (&gt;=).
    /// </summary>
    GreaterThanOrEquals,
}

/// <summary>
/// Extension methods for converting Operator enum values to SQL string symbols.
/// </summary>
public static class OperatorConversion
{
    /// <summary>
    /// Converts the enum value to its SQL symbol (e.g. "=" or "&gt;=").
    /// </summary>
    public static string ToSqlString(this Operator op)
    {
        switch (op)
        {
            case Operator.Equals:
                return "=";
            case Operator.NotEquals:
                return "!=";
            case Operator.LessThan:
                return "<";
            case Operator.LessThanOrEquals:
                return "<=";
            case Operator.GreaterThan:
                return ">";
            case Operator.GreaterThanOrEquals:
                return ">=";
            default:
                throw new ArgumentException(nameof(op));
        }
    }
}