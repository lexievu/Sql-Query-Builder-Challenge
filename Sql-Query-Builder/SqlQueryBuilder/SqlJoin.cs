namespace SqlQueryBuilder;

/// <summary>
/// Represents a SQL JOIN clause between two tables.
/// </summary>
/// <param name="Table">The name of the table being joined.</param>
/// <param name="Left">The column from the primary (left) table to join on.</param>
/// <param name="Right">The column from the joining (right) table to match against.</param>
/// <param name="JointType">The type of join to perform (e.g., Inner, Left Outer).</param>
public record SqlJoin(string Table, SqlColumn Left, SqlColumn Right, JointType JointType)
{
    /// <summary>
    /// Formats the join as a valid T-SQL string (e.g. "INNER JOIN Table ON A.Id = B.Id").
    /// </summary>
    public override string ToString()
    {
        return $"{JointType.ToSqlString()} JOIN {Table} ON {Left.TableName}.{Left.ColumnName} = {Right.TableName}.{Right.ColumnName}";
    }
}