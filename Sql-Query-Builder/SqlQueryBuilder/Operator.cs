namespace SqlQueryBuilder;

public enum Operator
{
    Equals,
    NotEquals,
    LessThan,
    LessThanOrEquals,
    GreaterThan,
    GreaterThanOrEquals,
}

public static class OperatorConversion
{
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
                throw new ArgumentException(nameof(Operator));
        }
    }
}