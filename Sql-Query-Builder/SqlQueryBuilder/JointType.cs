namespace SqlQueryBuilder;

/// <summary>
/// Specifies the type of SQL JOIN operation to perform.
/// </summary>
public enum JointType
{
    /// <summary>
    /// Returns records that have matching values in both tables.
    /// </summary>
    Inner,

    /// <summary>
    /// Returns all records from the left table, and the matched records from the right table.
    /// </summary>
    LeftOuter,

    /// <summary>
    /// Returns all records from the right table, and the matched records from the left table.
    /// </summary>
    RightOuter,
    
    /// <summary>
    /// Returns all records when there is a match in either left or right table.
    /// </summary>
    FullOuter
}

/// <summary>
/// Extension methods for converting JointType enum values to SQL string literals.
/// </summary>
public static class JointTypeConversion
{
    /// <summary>
    /// Converts the enum value to its T-SQL string equivalent (e.g. "LEFT OUTER").
    /// </summary>
    public static string ToSqlString(this JointType jointType)
    {
        switch (jointType)
        {
            case JointType.Inner:
                return "INNER";
            case JointType.LeftOuter:
                return "LEFT OUTER";
            case JointType.RightOuter:
                return "RIGHT OUTER";
            case JointType.FullOuter:
                return "FULL OUTER";
            default:
                throw new ArgumentException(nameof(jointType));
        }
    }
}