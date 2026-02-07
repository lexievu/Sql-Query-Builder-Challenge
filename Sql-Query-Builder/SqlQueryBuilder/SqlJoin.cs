namespace SqlQueryBuilder;

public record SqlJoin(string Table, SqlColumn Left, SqlColumn Right, JointType JointType)
{
    public override string ToString()
    {
        return $"{JointType.ToSqlString()} JOIN {Table} ON {Left.TableName}.{Left.ColumnName} = {Right.TableName}.{Right.ColumnName}";
    }
}