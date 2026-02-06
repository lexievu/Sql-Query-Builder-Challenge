namespace SqlQueryBuilderCSharp;

public record SqlColumn(string TableName, string ColumnName, string? ColumnAlias = null)
{
    public override string ToString()
    {
        if (string.IsNullOrEmpty(ColumnAlias))
            return $"{TableName}.{ColumnName}";
        else
            return $"{TableName}.{ColumnName} AS {ColumnAlias}";
    }
}