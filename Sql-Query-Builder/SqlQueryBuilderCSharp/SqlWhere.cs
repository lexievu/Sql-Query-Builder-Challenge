namespace SqlQueryBuilderCSharp;

public record SqlWhere(SqlColumn Column, Operator Operator, object Value) : IWhere
{
    public override string ToString()
    {
        var val = Value.ToString();
        // We would worry about SQL injection here if SQL query is executed
        if (Value is string)
            val = $"\"{Value}\"";
        else if (Value is DateTime dt) 
            val = $"'{dt:yyyy-MM-dd HH:mm:ss.mmm}'";
        return $"{Column.TableName}.{Column.ColumnName} {Operator.ToSqlString()} {val}";
    }
}

public interface IWhere;

public record And(IWhere[] Wheres) : IWhere
{
    public override string ToString()
    {
        return "(" + string.Join(" AND ", Wheres) + ")";
    }
}

public record Or(IWhere[] Wheres) : IWhere
{
    public override string ToString()
    {
        return "(" + string.Join(" OR ", Wheres) + ")";
    }
}